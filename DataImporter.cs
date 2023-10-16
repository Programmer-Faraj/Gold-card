using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net.Sockets;
using System.IO;
using Microsoft.VisualBasic.ApplicationServices;
using System.Linq.Expressions;
using System.CodeDom.Compiler;
using System.Runtime.Intrinsics.X86;
using System.DirectoryServices.ActiveDirectory;
using System.Net.NetworkInformation;

namespace GoldCard
{
    internal class DataImporter
    {
        // Defining a method to import data from the customers database
        public static Tuple<List<Customer>,string> VerifyCustomerData(string receivedData)
        {
            var customers = new List<Customer>();

            try
            { 
            // Passing a path to our database file
            string connectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\moham\source\repos\BookTips\GoldCard\Kundregister.mdf; Integrated Security = True";

            // Create a query for making a request to retrive data from a database table
            string query = "SELECT AnvändarNr, Namn, Kommun FROM Kunder";
            // Creating an object for the database connection
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open(); // Open a channel to the customer database file

                    // Create an object to apply operation for reading the data from the database and using the dispose keyword ´using´ to close the connection and avoid any resource leakage, run out of context, and letting the connection timed out
                    using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Reading data from the database
                    SqlDataReader reader = command.ExecuteReader();

                        // Defining a loop to loop through the database 
                        while (reader.Read())
                        {
                                string userID = $"{reader.GetValue(0)}"; // Accessing the first element in the cutomer database table and storing it in a variable

                                var clientUserId = receivedData.Split("-")[0]; // After receiving customer & card data as numbers from the client program, we need to split these two numbers and use index (zero) to access the cutomer ID number and storing it in a variable
                                var idIsCorrect = userID.Equals(clientUserId); // Comparing the splited cutomer ID number with the customer ID number in the database table if both numbers equals each other and storing the boolean value true/false in a variable
                                // Using if condition if the customer ID number is true then we can continue executing the logic of retrieving the data from the customer database and if the value is false then the execution stops and it means the data is not equal
                                if (idIsCorrect)
                                {   // Accessing and retrieving the three fields using indices in every post row inside the customer database table, using the hash symbol between the fields to add a white space in order to seprate the customer ID and the city fields from the combined cutomer's first name and last name and storing these values in a variable
                                    var userData = $"{reader.GetString(0)}#{reader.GetString(1)}#{reader.GetString(2)}";
                                    string[] parts = userData.Split("#"); // Spliting the three fields (Customer ID), (Customer's first name & last name) and the (City) with a hash symbol and storing them in an array

                                    string id = parts[0]; // Accessing the first element inside the array using the first index [0] which's the (Customer ID) and store it in a variable
                                    string name = parts[1]; // Accessing the second element inside the array using the second index [1] which's the (Customer's first name & last name) and store it in a variable
                                    string city = parts[2]; // Accessing the third element inside the array using the third index [2] which's the (Customer's city) and store it in a variable

                                    Customer customerData = new Customer(id, name, city); // Instantiating an object for the cutomer class to pass the variables for the customer class constructor
                                    customers.Add(customerData); // Using the instantiated object to add the stored values in the variables in a list
                                }
                        }
                }
                connection.Close(); // Close the connection 
                }
        }
        catch (Exception e) { } // Using a genric exception handling to catch the following exceptions that could be thrown: ´SqlException´, ´InvalidOperationException´, ´IOException´, ´System.Data.SqlClient.SqlException´, ´System.Data.Common.DbException´

            return new Tuple<List<Customer>,string>(customers, receivedData); // Returning the first string received from the client program and the collection of cutomer data in the list
        }
        
        public static Tuple<List<Card>,string> VerifyCardData(string receivedData) // Defining a static method list type and using tuple to verify received card data with the database table
        {
            var cards = new List<Card>(); // Defining a list so we can save a collection of card data and then use the values in the list to compare with the received data from the client program
            // Using the exception handling
            try
            {   // Passing a path to our database file
                string connectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\moham\source\repos\BookTips\GoldCard\Kortregister.mdf; Integrated Security = True";
                string query = "SELECT KortNr, KortTyp FROM Kort"; // Create a query for making a request to retrive data from a database table
                using (SqlConnection connection = new SqlConnection(connectionString))  // Creating an object for the database connection
                {   
                    connection.Open(); // Open a channel to the card database file
                    using (SqlCommand command = new SqlCommand(query, connection)) // Create an object to apply operation for reading the data from the database and using the dispose keyword ´using´ to close the connection and avoid any resource leakage, run out of context, and letting the connection timed out
                    {
                        SqlDataReader reader = command.ExecuteReader(); // Using the ExecuteReader method of a SqlCommand object to execute the query and return a SqlDataReader object where we can use to read the results
                        while (reader.Read()) // Using the object to read the results of the query by iterating through the result row by row
                        {
                            string cardSerialN = $"{reader.GetString(0)}"; // Accessing and retrieving the first field in the card database table

                            var clientCard = receivedData.Split("-")[1]; // After receiving data as a customer ID & a card serial numbers from the client program, we need to split these two numbers and use the index (one) to access the card serial number and storing it in a variable
                            var cardIsCorrect = cardSerialN.Equals(clientCard); // Comparing the splited cutomer ID number with the customer ID number in the database table if both numbers equals each other and storing the boolean value true/false in a variable
                            // Using if condition if the customer ID number is true then we can continue executing the logic of retrieving the data from the customer database and if the value is false then the execution stops and it means the data is not equal
                            if (cardIsCorrect)
                            {   // Accessing and retrieving the three fields using indices in every post row inside the card database table and storing these values in a variable
                                var cardData = $"{reader.GetString(0)} {reader.GetString(1)}"; 
                                string[] parts = cardData.Split(" "); // Retrieving the two fields (Serial number), (Type) in the card database table, spliting them by adding a white space and storing them in an array

                                string serialNumber = parts[0]; // Accessing the first element in the array using the first index [0] which's the (Card serial number) and store it in a variable
                                string cardType = parts[1]; // Accessing the second element inside the array using the second index [1] which's the (Card type) and store it in a variable

                                Card card = cardType switch // instantiating an object using a switch expression for the sub-classes to display cards type to the user
                                {
                                    "Dunderkatt" => new Dunderkatt(serialNumber, cardType), // Using the first case to access the first sub-class which represents the first card type (Dunderkatt) so we are able to display these information to the customer when the received card serial number is correct
                                    "Kristallhäst" => new Kristallhäst(serialNumber, cardType), // Using the second case to access the second sub-class which represents the second card type (Kristallhäst) so we are able to display these information to the customer when the received card serial number is correct
                                    "Överpanda" => new Överpanda(serialNumber, cardType), // Using the third case to access third sub-class which represents the third card type (Överpanda) so we are able to display these information to the customer when the received card serial number is correct
                                    _ => new Eldtomat(serialNumber, cardType) // Here we are not using a name for the card type as a case, because this is the default type, but we need to access the fourth sub-class which represents the fourth card type (Eldtomat) so we are able to display these information to the customer when the received card serial number is correct
                                };
                                    cards.Add(card); // After passing the variables that store the values of card serial number and card type for each sub-class we need to add/save in a list so we can use the values in the list to compare with the received data from the client program
                            }
                        }
                    }
                    connection.Close(); // Close the connection 
                }
            }
            catch
            {
                 MessageBox.Show("An error occurs in Card data!"); // Using a genric exception handling to catch the following exceptions that could be thrown: ´SqlException´, ´InvalidOperationException´, ´IOException´, ´System.Data.SqlClient.SqlException´, ´System.Data.Common.DbException´
            }
            
            return new Tuple<List<Card>,string>(cards, receivedData); // Returning the first string received from the client program and the collection of card data in the list
        }
    }
}
