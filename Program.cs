using System;
using System.Data.SQLite;
using System.IO;

namespace Project
{
    class Program
    {
        static void Main(string[] args)
        {

            BusinessRules db = new BusinessRules();

            try
            {
                //open connection
                db.OpenConnection();
                
                //Selecting customer and contract details after changes 
                db.SelectAllDataFromCustomers();

                db.SelectCustomersAndContracts();

                db.FindAVGContractsPerCustomer();

                db.FindAvgContractLength();

                db.findContractLengthLeft("Sandra", "gas", 2019);

                db.FindAvgContractValue();


            }
            finally
            {
                //close connection 
                db.CloseConnection();
            }

            
            Console.ReadKey();
        }
    }
}
