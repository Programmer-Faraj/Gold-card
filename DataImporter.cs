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

namespace GoldCard
{
    internal class DataImporter
    {
        // Defining a method to import data from the customers database
        public static List<Customer> VerfiyCustomerData(string receivedData)
        {
            var customers = new List<Customer>();
            /////////////////////////////////////////////////////7
            try
            { 
            // Passing a path to our database file
            string connectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\moham\source\repos\BookTips\GoldCard\Kundregister.mdf; Integrated Security = True";

            // Create a query for making a request to retrive data from a database table
            string query = "SELECT AnvändarNr, Namn, Kommun FROM Kunder";
            // Creating an object for the database connection
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Invoking to open the databse connection
                connection.Open();

                // Create an object to apply operation for reading the data from the database
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Reading data from the database
                    SqlDataReader reader = command.ExecuteReader();

                    // Defining a loop to loop through the database 
                    
                        while (reader.Read())
                        {
                                string userID = $"{reader.GetValue(0)}";

                                var clientUserId = receivedData.Split("-")[0];
                                var idIsCorrect = userID.Equals(clientUserId);
                                //
                                if (idIsCorrect)
                                {
                                    var userData = $"{reader.GetString(0)} {reader.GetString(1)} {reader.GetString(2)}";
                                    string[] parts = userData.Split(" ");

                                    string id = parts[0];
                                    string name = parts[1];
                                    string municipality = parts[2];

                                    Customer customerData = new Customer(id, name, municipality);
                                    customers.Add(customerData);
                                }
                            /*var data1 = reader.GetValue(0).ToString();
                            var data2 = reader.GetValue(1).ToString();
                            var data3 = reader.GetValue(2).ToString();*/
                            /* string data2 = reader.GetString(1);
                             string data3 = reader.GetString(2);*/
                        }
                }
                connection.Close();
            }
        }
        catch (Exception e) { }

        //MessageBox.Show("An error occurs in customer data!");
    
            return customers;
        }
        
        public static List<Card> VerfiyCardData(string receivedData)
        {
            var cards = new List<Card>();   
            try
            {
                string connectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\moham\source\repos\BookTips\GoldCard\Kortregister.mdf; Integrated Security = True";

                string query = "SELECT KortNr, KortTyp FROM Kort";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            string cardSerialN = $"{reader.GetString(0)}";

                            var clientCard = receivedData.Split("-")[1];
                            var cardIsCorrect = cardSerialN.Equals(clientCard);

                            if (cardIsCorrect) 
                            {
                                var cardData = $"{reader.GetString(0)} {reader.GetString(1)}";

                                string[] parts = cardData.Split(" ");
                                
                                string serialNumber = parts[0];
                                string cardType = parts[1];

                                Card card = new Card(serialNumber, cardType);
                                cards.Add(card);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            
            catch
            {
                MessageBox.Show("An error occurs in Card data!");
            }
            
            return cards;
        }
    }
}
