using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenukaartSQL
{
    class Program
    {

        public static string ConnectionString = ("Network Address=ao.tryan.nl; user id=menukaart; password=vroemvroem;Initial catalog=Menukaart;");
        public static SqlConnection conn = new SqlConnection(ConnectionString);
        #region list
        private static List<string> Des = new List<string>();
        private static List<string> DesPri = new List<string>();
        private static List<string> Soep = new List<string>();
        private static List<string> SoepPri = new List<string>();



        #endregion
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Clear();
            Console.WriteLine("");

            Sql();

            Console.WriteLine("***");
            Console.WriteLine("***");
            Console.WriteLine("Deserts");
            Console.WriteLine("***");
            Console.WriteLine("***");

            // in deze console.writeline hoef je nu alleen nog maar de naam van de list mee tegeven
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(Soep[i]);
            }

            Console.ReadLine();
        }

        private static void Sql()
        {
            conn.Open();
            SqlCommand myCommand1 = new SqlCommand("SELECT * FROM Desserts", conn);
            SqlDataReader myReader1 = myCommand1.ExecuteReader();
            while (myReader1.Read())
            {
                Des.Add(myReader1["Dessert"].ToString());
                DesPri.Add(myReader1["Prijs"].ToString());
            }

            conn.Close();

            conn.Open();
            SqlCommand myCommand2 = new SqlCommand("SELECT * FROM Soepen", conn);
            SqlDataReader myReader2 = myCommand2.ExecuteReader();
            while (myReader2.Read())
            {
                Soep.Add(myReader2["Soepen"].ToString());
                SoepPri.Add(myReader2["Prijs"].ToString());
            }
            conn.Close();
        }
    }
}
