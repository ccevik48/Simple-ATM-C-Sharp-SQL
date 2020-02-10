using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;

namespace sqlBank
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Sql Bank!\n1) Sign in\n2) Create New Account");
            
            string choice = Console.ReadLine().ToString();
            while(choice != "1" || choice != "2")
            {
                if(choice == "1" || choice == "2")
                {
                    break;
                }
                Console.Clear();
                Console.WriteLine("Not a valid choice.\n1) Sign in\n2) Create New Account");
                choice = Console.ReadLine().ToString();
            }
            if(choice == "2")
            {
                Console.WriteLine("Enter First Name: ");
                string fname = Console.ReadLine();
                Console.WriteLine("Enter Last Name: ");
                string lname = Console.ReadLine();
                Console.WriteLine("Enter Username: ");
                string uname = Console.ReadLine();
                Console.WriteLine("Enter Password: ");
                string pword = Console.ReadLine();
                createAccount(fname, lname, uname, pword);
                Console.WriteLine("Creating Account...");
            }
            
            else if(choice == "1")
            {
                Console.Clear();
                Console.WriteLine("Enter Username:");
                string USERNAME = Console.ReadLine().ToString();
                while(!signedIn(USERNAME))
                {
                    if(signedIn(USERNAME))
                    {
                        break;
                    }
                    Console.Clear();
                    Console.WriteLine("No such username exists.");
                    USERNAME = Console.ReadLine().ToString();
                }

                Console.Clear();
                Console.WriteLine(USERNAME+ ", Enter Password:");
                string PASS = Console.ReadLine().ToString();
                while(!signedInPass(PASS))
                {
                    if(signedInPass(PASS))
                    {
                        break;
                    }
                    Console.Clear();
                    Console.WriteLine("Password does not match username: "+ USERNAME);
                    PASS = Console.ReadLine().ToString();
                }
                Console.Clear();

                string action = "0";
                while(action != "6")
                {
                    Console.WriteLine("Welcome, " + getFirstName(USERNAME) + "\nSigned in as " + USERNAME + "\n");
                    Console.WriteLine("\t1) View Balance\n" + 
                                      "\t.) Make Deposit\n" +
                                        "\t\t2) Into Checkings\n" +
                                        "\t\t3) Into Savings\n" +
                                      "\t.) Make Withdrawal\n" +
                                        "\t\t4) From Checkings\n" +
                                        "\t\t5) From Savings\n" +
                                      "\t6) Logout" +
                                      "\t7) Delete Account");
                    action = Console.ReadLine();
                    switch(action)
                    {
                        case "1":
                        Console.WriteLine(getBalance(USERNAME) + "\n");
                        break;
                        case "2":
                        Console.WriteLine("Enter Amount to deposit into Checkings: ");
                        int depositChk = int.Parse( Console.ReadLine() );
                        Console.WriteLine("Depositing ${0} into Checkings...", depositChk);
                        depositCheckings( depositChk, USERNAME);
                        break;
                        case "3":
                        Console.WriteLine("Enter Amount to deposit into Savings: ");
                        int depositSav = int.Parse( Console.ReadLine() );
                        Console.WriteLine("Depositing ${0} into Checkings...", depositSav);
                        depositSavings(depositSav, USERNAME);
                        break;
                        case "4":
                        Console.WriteLine("Enter Amount to withdraw from Checkings: ");
                        int withdrawChk = int.Parse( Console.ReadLine() );
                        Console.WriteLine("Depositing ${0} into Checkings...", withdrawChk);
                        withdrawCheckings(withdrawChk, USERNAME);
                        break;
                        case "5":
                        Console.WriteLine("Enter Amount to withdraw from Savings: ");
                        int withdrawSav = int.Parse( Console.ReadLine() );
                        Console.WriteLine("Depositing ${0} into Checkings...", withdrawSav);
                        withdrawSavings(withdrawSav, USERNAME);
                        break;
                        case "6":
                        Console.WriteLine("Logging Off...");
                        break;
                        case "7":
                        Console.WriteLine("Are you sure? (y/n)");
                        var answer = Console.ReadLine();
                        switch(answer)
                        {
                            case "y":
                            deleteAccount(USERNAME);
                            Environment.Exit(0);
                            break;
                            case "n":
                            //
                            break;
                            default:
                            break;
                        }
                        break;
                        default:
                        Console.WriteLine("Not an option. Enter a value between 1 and 7:");
                        break;
                    }
                }
                

                
            }
            Environment.Exit(0);

            /*
            using(SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = "Server=(localdb)\\ProjectsV13;Database=bankDB;Trusted_Connection=true";
                conn.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM bankUsers ORDER BY FirstName", conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                Console.WriteLine(reader.GetValue(i));
                            }
                            Console.WriteLine();
                        }
                    }
                }
            }*/
        }



        









        public static bool signedIn(string user)
        {
            using(SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = "Server=(localdb)\\ProjectsV13;Database=bankDB;Trusted_Connection=true";
                conn.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM bankUsers WHERE UserName='"+ user +"'", conn))
                {
                    Object result = command.ExecuteScalar();
                    if(result == null)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool signedInPass(string userPass)
        {
            using(SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = "Server=(localdb)\\ProjectsV13;Database=bankDB;Trusted_Connection=true";
                conn.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM bankUsers WHERE Password='"+ userPass +"'", conn))
                {
                    Object result = command.ExecuteScalar();
                    if(result == null)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public static string getFirstName(string user)
        {
            using(SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = "Server=(localdb)\\ProjectsV13;Database=bankDB;Trusted_Connection=true";
                conn.Open();
                
                using (SqlCommand command = new SqlCommand("SELECT FirstName FROM bankUsers WHERE UserName='"+ user +"'", conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            return reader.GetValue(0).ToString();
                        }
                    }
                }
            }
            return "NAME";
        }
        public static string getBalance(string user)
        {
            using(SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = "Server=(localdb)\\ProjectsV13;Database=bankDB;Trusted_Connection=true";
                conn.Open();
                
                using (SqlCommand command = new SqlCommand("SELECT Checkings, Savings FROM bankUsers WHERE UserName='"+ user +"'", conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            return "Checkings: $" + reader.GetValue(0).ToString() + "\nSavings: $" + reader.GetValue(1).ToString();
                        }
                    }
                }
            }
            return "0";
        }
        public static string depositCheckings(int amount, string user)
        {
            using(SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = "Server=(localdb)\\ProjectsV13;Database=bankDB;Trusted_Connection=true";
                conn.Open();
                decimal currBalance = 0;
                
                using (SqlCommand command = new SqlCommand("SELECT Checkings FROM bankUsers WHERE UserName='"+ user +"'", conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            currBalance = Convert.ToDecimal( reader.GetValue(0) );
                        }
                    }
                }
                currBalance += amount;

                using (SqlCommand command = new SqlCommand("UPDATE bankUsers SET Checkings ='" + currBalance+ "' WHERE UserName='"+ user +"'", conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            currBalance = Convert.ToDecimal( reader.GetValue(0) );
                        }
                        command.Parameters.AddWithValue("Checkings", currBalance);
                    }
                }
            }
            return "0";
        }


        public static string depositSavings(int amount, string user)
        {
            using(SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = "Server=(localdb)\\ProjectsV13;Database=bankDB;Trusted_Connection=true";
                conn.Open();
                decimal currBalance = 0;
                
                using (SqlCommand command = new SqlCommand("SELECT Savings FROM bankUsers WHERE UserName='"+ user +"'", conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            currBalance = Convert.ToDecimal( reader.GetValue(0) );
                        }
                    }
                }
                currBalance += amount;

                using (SqlCommand command = new SqlCommand("UPDATE bankUsers SET Savings ='" + currBalance+ "' WHERE UserName='"+ user +"'", conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            currBalance = Convert.ToDecimal( reader.GetValue(0) );
                        }
                        command.Parameters.AddWithValue("Savings", currBalance);
                    }
                }
            }
            return "0";
        }

        public static string withdrawCheckings(int amount, string user)
        {
            using(SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = "Server=(localdb)\\ProjectsV13;Database=bankDB;Trusted_Connection=true";
                conn.Open();
                decimal currBalance = 0;
                
                using (SqlCommand command = new SqlCommand("SELECT Checkings FROM bankUsers WHERE UserName='"+ user +"'", conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            currBalance = Convert.ToDecimal( reader.GetValue(0) );
                        }
                    }
                }
                currBalance -= amount;

                using (SqlCommand command = new SqlCommand("UPDATE bankUsers SET Checkings ='" + currBalance+ "' WHERE UserName='"+ user +"'", conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            currBalance = Convert.ToDecimal( reader.GetValue(0) );
                        }
                        command.Parameters.AddWithValue("Checkings", currBalance);
                    }
                }
            }
            return "0";
        }


        public static string withdrawSavings(int amount, string user)
        {
            using(SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = "Server=(localdb)\\ProjectsV13;Database=bankDB;Trusted_Connection=true";
                conn.Open();
                decimal currBalance = 0;
                
                using (SqlCommand command = new SqlCommand("SELECT Savings FROM bankUsers WHERE UserName='"+ user +"'", conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            currBalance = Convert.ToDecimal( reader.GetValue(0) );
                        }
                    }
                }
                currBalance -= amount;

                using (SqlCommand command = new SqlCommand("UPDATE bankUsers SET Savings ='" + currBalance+ "' WHERE UserName='"+ user +"'", conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            currBalance = Convert.ToDecimal( reader.GetValue(0) );
                        }
                        command.Parameters.AddWithValue("Savings", currBalance);
                    }
                }
            }
            return "0";
        }

        public static void deleteAccount(string user)
        {
            using(SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = "Server=(localdb)\\ProjectsV13;Database=bankDB;Trusted_Connection=true";
                conn.Open();

                using (SqlCommand command = new SqlCommand("DELETE FROM bankUsers WHERE UserName='"+user+"'", conn))
                {
                    Console.WriteLine("Deleting {0}'s Account...", user);
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void createAccount(string firstname, string lastname, string username, string password)
        {
            using(SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = "Server=(localdb)\\ProjectsV13;Database=bankDB;Trusted_Connection=true";
                conn.Open();

                using (SqlCommand command = new SqlCommand(@"INSERT INTO bankUsers (FirstName, LastName, UserName, Password) VALUES(@firstname,@lastname,@username,@password)", conn))
                {
                    command.Parameters.AddWithValue("FirstName", firstname);  
                    command.Parameters.AddWithValue("LastName", lastname);  
                    command.Parameters.AddWithValue("UserName", username); 
                    command.Parameters.AddWithValue("Password", password);
                    command.ExecuteNonQuery();
                }
            }
        }


    }
}
