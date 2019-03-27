using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    class BusinessRules: Database
    {
        //protected variables to use in methods for calculations
        protected int openContracts, w, x, y, z;
        protected double avg;
             
        public void FindAVGContractsPerCustomer()
        {
            //create two string queries to get values needed to calc average contract per customer
            String query = "SELECT count(customer_id) AS customerNum FROM customers";
            String query2 = "SELECT count(service) AS serviceCount FROM contracts JOIN customers on customerId = customers.customer_id GROUP BY customer_name";

            SQLiteCommand myCommand = new SQLiteCommand(query, myConn);
            SQLiteCommand myCommand2 = new SQLiteCommand(query2, myConn);

            SQLiteDataReader result = myCommand.ExecuteReader();
            SQLiteDataReader result2 = myCommand2.ExecuteReader();

            while (result.Read())
            {
                //convert result of customerNum to int and assign to x - this is the amount of customers in the database
                this.x = Convert.ToInt32(result["customerNum"]);
            }
            while (result2.Read())
            {
                //serviceCount is the amount of contracts per customer 
                this.y = Convert.ToInt32(result2["serviceCount"]);
                this.z += this.y;
            }
            this.avg = this.z / this.x;
            Console.WriteLine("Average Contract Per Customer: {0}", this.avg);
        }

        public void FindAmountOfOpenContracts()
        {
            String query = "SELECT count(contracts_id) AS OpenContracts FROM contracts WHERE Status='Open'";
            SQLiteCommand myCommand = new SQLiteCommand(query, myConn);
            SQLiteDataReader result2 = myCommand.ExecuteReader();
            
                while (result2.Read())
                {
                this.openContracts = Convert.ToInt32(result2["OpenContracts"]);
                Console.WriteLine("Amount of open contracts: {0}", this.openContracts);
            }
        }
        public void FindAvgContractLength()
        {
            String query = "SELECT startDate, endDate FROM contracts";
            String query2 = "SELECT count(contracts_id) AS ContractNum FROM contracts";

            SQLiteCommand myCommand = new SQLiteCommand(query, myConn);
            SQLiteCommand myCommand2 = new SQLiteCommand(query2, myConn);

            SQLiteDataReader result = myCommand.ExecuteReader();
            SQLiteDataReader result2 = myCommand2.ExecuteReader();
            //first set of results from the first string query 
            while (result.Read())
            {
                this.x = Convert.ToInt32(result["startDate"]);
                this.y = Convert.ToInt32(result["endDate"]);
                //calculating the legnth of each contract by taking the start date from the end date 
                this.w += (this.y - this.x);
               
            }
            while (result2.Read())
            {
                //number of contracts
                this.z = Convert.ToInt32(result2["ContractNum"]);
                
            }

            this.avg = (this.w / this.z);
            Console.WriteLine("Average contract length: {0}", this.avg);
        }

        //this method takes in the customers name, what service contract they want to check and the year that they input(current year)
        public void findContractLengthLeft(String name, String service, int year)
        {
            String query = "SELECT endDate FROM contracts JOIN customers on customerId = customers.customer_id WHERE customer_name = @customer_name AND service = @service";
            SQLiteCommand myCommand = new SQLiteCommand(query, myConn);
            myCommand.Parameters.AddWithValue("@customer_name", name);
            myCommand.Parameters.AddWithValue("@service", service);
            this.x = year;

            SQLiteDataReader result = myCommand.ExecuteReader();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    this.y = Convert.ToInt32(result["endDate"]);
                    this.w = (this.y - this.x);
                    Console.WriteLine("Years left on {0} contract for {1}: {2}", service, name, this.w);
                }
            }    
        }
        public void FindAvgContractValue()
        {
            String query = "SELECT count(contracts_id) AS count, sum(value) AS value FROM contracts JOIN customers ON customerId = customers.customer_id GROUP BY customer_name";
            SQLiteCommand myCommand = new SQLiteCommand(query, myConn);
            SQLiteDataReader result = myCommand.ExecuteReader();

            while (result.Read())
            {
                //value is the total value of all contracts per customer 
                this.x = Convert.ToInt32(result["value"]);
                //count is the total amount of contracts per customer     
                this.y = Convert.ToInt32(result["count"]);
                this.w = (this.x / this.y);
                Console.WriteLine("Average contract value per customer ${0}", this.w);
            }
        }
    
    }

}
