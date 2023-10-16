using Microsoft.VisualBasic.ApplicationServices;
using System.Data.SqlClient;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Drawing;
using System.Diagnostics.Eventing.Reader;

namespace GoldCard
{
    public partial class Form1 : Form
    {
        TcpListener listener; // Defining a listner object to listen to a client connection
        TcpClient client; // Defining a client object that contains the address for the host computer
        int port = 12345; // Using a port number to listen to the client connection

        List<DataImporter> customers = new List<DataImporter>(); // Defining a list to save the customers values 
        public Form1()
        {
            InitializeComponent();
        }

        public async void StartReceiving() // Defining an async method to accept an incoming client connections asynchronously from the client program
        {
            try // Applying exception handling if not getting a connection or our programs disconnects
            {
                client = await listener.AcceptTcpClientAsync(); // Using the the object to accept incoming client connections and the keyword await means this is an asynchronous operation that the method will paus and wait for a client connection
            }
            catch (Exception error) { MessageBox.Show(error.Message, Text); return; } // Catching the exception and displaying an error message instead of allowing the program to crash and display an error message to the user instead

            StartReading(client); // Invoking the method to read and wait for incoming byte data through the connection
        }

        public async void StartReading(TcpClient client) // Defining this method to start reading byte data sent by the client program
        {
            byte[] bytes = new byte[1024]; // Defining an array to save the incoming byte data from the client program when reading it

            int n = 0; // Decalaring a variable and initializing it to use it when storing data in the array that represents the beginning of the array 
            try // Handling 'System.IO.IOException' exception where can be thrown if there's a network error or the underlying stream is closed
            {
                n = await client.GetStream().ReadAsync(bytes, 0, bytes.Length); // Using asynchronous operation that reads data from the network stream associated with the client object and stores it in the bytes array
            }
            catch (Exception error) { MessageBox.Show(error.Message, Text); return; } // When an exception occurs it is cought by the `catch` block then we store the exception in the `error` variable so the program won't crash and display an error message to the user instead

            string receivedData = Encoding.Unicode.GetString(bytes, 0, n); // Converting the incoming byte data into string type and store them in a variable
            var verfiedClients = DataImporter.VerifyCustomerData(receivedData); // Invoking the static customer data method after verifying the accessed data from the database and storing the values in a variable 
                                                                                // SendToClient(string.Join(",", verfiedClients.Item1.Select(c => c.ID)));

            var result1 = DataImporter.VerifyCustomerData(receivedData); // Invoking the customer data method, passing the received data from the client and storing the data in a variable
            var verfiedCards = DataImporter.VerifyCardData(receivedData); // Invoking the static card data method after verifying the accessed data from the database, passing a variable that stores data received from the client and storing the verified data in a variable
            var result2 = DataImporter.VerifyCardData(receivedData); // Invoking the card data method, passing the received data from the client and storing the data in a variable

            var isCustomerDataCorrect = result1.Item1.Any(); // After storing the received verified customer data in this line we need to check if the string matches any customerID in the list
            var isCardDataCorrect = result2.Item1.Any(); // After storing the received verified card serial numbers data in this line we need to check if the string matches any card serial number in the list
            string message = string.Empty; // Here we need to declare a variable where we can use it to reference our variables using string interpolation, sending these values as messages and display these values to the user

            string user1; // Declaring a variable to store the value of first name in the customers list
            string user2; // Declaring a variable to store the value of last element which's the card type in the card list
            string user3; // Declaring a variable to store the value of the city name in the customers list
            string user4; // Declaring a variable to store the value of 

            if (isCustomerDataCorrect && isCardDataCorrect) // Using if condition to do comparession and setting both variables to true when receiving correct customer ID data and correct card serial number data
            {
                user1 = result1.Item1.First().Name; // Initializing the variable with the item1 in the list and accessing the name field
                user2 = result2.Item1.Last().Type; // Initializing the variable with item1 in the cards list and accessing the type field
                user3 = result1.Item1.Last().City; // Initializing the variable with the item1 in the customers list and accessing the city field
                message = $"Congratulations {user1}!" + // Initializing the variable message using the string interpolation, referencing the variables and send in a form of message to the client program
                          $"\n you have won this execusive gold card {user2}!" +
                          $"\n You can bring your gold card from a local store in {user3}!";
            }

            if (!isCustomerDataCorrect && isCardDataCorrect) // Using if condition to do a comparession and setting customer ID data to false and card data to true when receiving incorrect customer ID data and correct card serial number data
            {
                user4 = result1.Item2; // Declaring a variable to store the value of 
                user2 = result2.Item1.Last().Type; // Initializing the variable with item1 in the cards list and accessing the type field
                message = $"The user number you provided is not registered but you have won {user2}!" + // Initializing the variable message for the second block using the string interpolation,
                          $"\n control that the user ID is correct and try again!";                     // referencing only the card type variable and send in a form of message to the client program
            }

            if (isCustomerDataCorrect && !isCardDataCorrect) // Using if condition to do a comparession and setting customer ID data to true and card data to false when receiving incorrect customer ID data and correct card serial number data
            {
                user1 = result1.Item1.First().Name; // Initializing the variable with the item1 in the list and accessing the name field
                user4 = result2.Item2; // Declaring a variable to store the value of
                message = $"The user ID you provided is registered {user1} but you have not win a gold card!"; // Initializing the variable message for the second block using the string interpolation,
            }                                                                                                   // referencing only the card type variable and send in a form of message to the client program

            if (!isCustomerDataCorrect && !isCardDataCorrect) // Using if condition to do a comparession and setting both customer ID data to false and card data to false when receiving incorrect customer ID and correct card serial number
            {
                message = $"The user number you provided is not registered\n" + // Initializing the variable message in the fourth block using the string interpolation And send in a form of message to the client program
                          $"and unfortunately you have not win a gold card this time!";
            }
            SendToClient(message); // Invoking the send method and passing the variable as a parameter to send messages to the client program
            StartReading(client); // Invoking the read method to read the data sent from the client program 
        }

        public async void SendToClient(string message) // Defining a method and passing a parameter to send data to the client program
        {
            byte[] outData = Encoding.Unicode.GetBytes(message); // Defining an byte array to convert the string data into bytes and store them in the array
            // The expected exception that can occur in this line is a `System.IO.IOException` this exception can be thrown when an I/O error or a network-related errors for ex: when dissconnecting or timeouts
            try
            {
                await client.GetStream().WriteAsync(outData, 0, outData.Length); // Using asynchronous operation to write data from the ´outData´ byte array to the network stream associated with the client program
            }
            catch (Exception error) { MessageBox.Show(error.Message, Text); return; } // When an exception occurs it is cought by the `catch` block then we store the exception in the `error` variable so the program won't crash and display an error message to the user instead
        }

        private void startServer_btn(object sender, EventArgs e) // Adding an Event Handler button to start the server first to establish a connection between the client and the server programs 
        {
            try // The expected exception that can occur in this line is a `System.Net.Sockets.SocketException` to specifically handle network-related errors
            {
                listener = new TcpListener(IPAddress.Any, port); // Creating a new object to listen and accept any incoming connections from the client on any available network
                listener.Start(); // Starting the listner object and once started, listen to incoming connections on the specified IP address and port number
            }
            catch (SocketException error) { MessageBox.Show(error.Message, Text); return; } // When an exception occurs it is cought by the `catch` block then we store the exception in the `error` variable so the program won't crash and display an error message to the user instead
            StartReceiving(); // Invoking this method which to handle incoming connections from the client program if no exception occurs

            button1.BackColor = Color.Green; // Changing the color of the startServer button when clicking it
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}