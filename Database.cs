using System.Data.SQLite;
using System.IO;
using System;
namespace Project
{
    class Database
    {
        //making an instance of SQLite connection
        public SQLiteConnection myConn;

        public Database()
        {
            //initialising the connection to SQLite
            myConn = new SQLiteConnection("Data Source=database.sqlite");
            if (!File.Exists("./database.sqlite"))
            {
                //creating the file originally if it doesn't already exist
                SQLiteConnection.CreateFile("database.sqlite");
                Console.Write("Database file created: database.sqlite");
            }

        }
        
        //method to open connection to database
        public void OpenConnection()
        {
            if(myConn.State != System.Data.ConnectionState.Open)
            {
                myConn.Open();
            }
        }
        //method to close connection to databse
        public void CloseConnection()
        {
            if(myConn.State != System.Data.ConnectionState.Closed)
            {
                myConn.Close();
            }
        }
        //Select all information from customers table
        public void SelectAllDataFromCustomers()
        {
            //creating a query 
            String query = "SELECT * FROM customers";
            //creating an instance of SQLiteCommand with my above query and db connection 
            SQLiteCommand myCommand = new SQLiteCommand(query, myConn);
            SQLiteDataReader result2 = myCommand.ExecuteReader();
            //loop to read through all data and console it out 
            if (result2.HasRows)
            {
                while (result2.Read())
                {
                    Console.WriteLine("ID: {0} - Name: {1} - Address: {2} - Age: {3}", result2["customer_id"], result2["customer_name"], result2["customer_address"], result2["customer_age"]);
                }
                result2.Close();
            }
        }
        //select all information from contracts table
        public void SelectAllDataFromContracts()
        {
            String query = "SELECT * FROM contracts";
            SQLiteCommand myCommand = new SQLiteCommand(query, myConn);
            SQLiteDataReader result2 = myCommand.ExecuteReader();
            if (result2.HasRows)
            {
                while (result2.Read())
                {
                    Console.WriteLine("ID: {0} - Start Year: {1} - Service: {2} - CustomerId: {3}", result2["contracts_id"], result2["startYear"], result2["service"], result2["customerId"]);
                }
                result2.Close();
            }

        }
        //Method for inserting new values into the customers table
        public void InsertIntoCustomersTable(int id, String name, String address, int age)
        {
            String query = "INSERT INTO customers ('customer_id', 'customer_name', 'customer_address', 'customer_age') VALUES (@customer_id, @customer_name, @customer_address, @customer_age)";
            SQLiteCommand myCommand = new SQLiteCommand(query, myConn);

            //this creates the values of each column with what is entered into the method in the main class
             myCommand.Parameters.AddWithValue("@customer_id", id);
             myCommand.Parameters.AddWithValue("@customer_name", name);
             myCommand.Parameters.AddWithValue("@customer_address", address);
             myCommand.Parameters.AddWithValue("@customer_age", age);

            var result = myCommand.ExecuteNonQuery();

            Console.WriteLine("Rows added: {0}", result);
        }
        //Method for inserting new values into the contracts table
        public void InsertIntoContractsTable(int startDate, String service, int customerId)
        {
            String query = "INSERT INTO contracts ('startYear', 'service', 'customerId') VALUES (@startYear, @service, @customerId)";
            SQLiteCommand myCommand = new SQLiteCommand(query, myConn);

            //myCommand.Parameters.AddWithValue("@id", id);
            myCommand.Parameters.AddWithValue("@startYear", startDate);
            myCommand.Parameters.AddWithValue("@service", service);
            myCommand.Parameters.AddWithValue("@customerId", customerId);

            var result = myCommand.ExecuteNonQuery();

            Console.WriteLine("Rows added: {0}", result);
        }
        //Method for updating customers name in the customers table
        public void UpdateNameInCustomersTable(String name, int id)
        {
            String query = ("UPDATE customers SET customer_name = @customer_name WHERE customer_id = @customer_id");
            SQLiteCommand myCommand = new SQLiteCommand(query, myConn);
            myCommand.Parameters.AddWithValue("@customer_name", name);
            myCommand.Parameters.AddWithValue("@customer_id", id);

            var result = myCommand.ExecuteNonQuery();
            Console.WriteLine("Details updated: {0}", result);
        }
        //Method for deleting a customer from the customers table given their name and id
        public void DeleteCustomerFromTable(String name, int id)
        {
            String query = ("Delete From customers WHERE customer_name = @customer_name AND customer_id = @customer_id");
            SQLiteCommand myCommand = new SQLiteCommand(query, myConn);
            myCommand.Parameters.AddWithValue("@customer_name", name);
            myCommand.Parameters.AddWithValue("@customer_id", id);

            var result = myCommand.ExecuteNonQuery();
            Console.WriteLine("Customer dropped: {0}", result);
        }
        //Method to delete a contract from the contracts table given the id
        public void DeleteContractFromTable(int id)
        {
            String query = ("Delete From contracts WHERE customer_id = @customer_id");
            SQLiteCommand myCommand = new SQLiteCommand(query, myConn);
            myCommand.Parameters.AddWithValue("@customer_id", id);
            var result = myCommand.ExecuteNonQuery();
            Console.WriteLine("Contract dropped: {0}", result);
        }
        //Method for joint selection of customers with contracts 
        public void SelectCustomersAndContracts()
        {
            String query = "SELECT customer_name, customer_address, startDate, service From contracts Join customers ON customerId=customers.customer_id";
            SQLiteCommand myCommand = new SQLiteCommand(query, myConn);
            SQLiteDataReader result2 = myCommand.ExecuteReader();
            if (result2.HasRows)
            {
                while (result2.Read())
                {
                    Console.WriteLine("Name: {0} - Address: {1} - Start Year: {2} - Service: {3}", result2["customer_name"], result2["customer_address"], result2["startDate"], result2["service"]);
                }

               
            }
        }
    }
}
