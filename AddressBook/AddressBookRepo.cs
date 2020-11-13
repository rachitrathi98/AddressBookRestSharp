using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace AddressBook
{
    public class AddressBookRepoDB
    {
        public static string connectionString = "Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog =AddressBookService; User ID = racrathi; Password=Rachit123*";

        static SqlConnection connection;

        /// <summary>
        /// UC16 RetrieveDataFromDB
        /// </summary>
        /// <returns></returns>
        public static ContactsDB RetrieveData()
        {
            try
            {
                connection = new SqlConnection(connectionString);

                ContactsDB contactsDB = new ContactsDB();

                string query = "select c.FirstName, c.LastName, c.City, c.PhoneNumber, bk.B_Name, bk.B_Type from Contacts c inner join BookNameType bk on c.B_ID=bk.B_ID where c.FirstName='Rachit'";

                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        contactsDB.first_name = reader.GetString(0);
                        contactsDB.last_name = reader.GetString(1);
                        contactsDB.city = reader.GetString(2);
                        contactsDB.phone = reader.GetString(3);
                        contactsDB.B_Name = reader.GetString(4);
                        contactsDB.B_Type = reader.GetString(5);
                    }
                }
                else
                {
                    Console.WriteLine("No rows found");
                }
                reader.Close();

                return contactsDB;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            finally
            {
                connection.Close();
            }

        }
        /// <summary>
        /// UC17 UpdateDetailsInDB
        /// </summary>
        /// <returns></returns>
        public static string UpdateDetailsInDB()
        {
            string state = "";
            try
            {
                connection = new SqlConnection(connectionString);
                string query = "update Contacts set State='Karnataka' where FirstName='Akhil'; select * from Contacts where FirstName='Akhil'";

                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        state = reader.GetString(4);
                    }
                }
                else
                {
                    Console.WriteLine("Row isn't updated");
                }
                reader.Close();
                return state;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return state;
            }
            finally
            {
                connection.Close();
            }
        }
        public static string GetDateBetweenRange()
        {
            string First_Name = "";
            string Combine_Name = "";
            try
            {
                connection = new SqlConnection(connectionString);
                string query = "select * from Contacts where SDate between cast('2020-01-01' as date) and cast('2020-01-08' as date)";

                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        First_Name = reader.GetString(0);
                        Combine_Name = Combine_Name+First_Name;
                    }
                }
                else
                {
                    Console.WriteLine("Row isn't updated");
                }
                reader.Close();
                return Combine_Name;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return Combine_Name;
            }
            finally
            {
                connection.Close();
            }
        }
        public bool AddContactDetailsInDB(ContactsDB model)
        {
            try
            {
                using (connection)
                {
                    connection = new SqlConnection(connectionString);
                    SqlCommand command = new SqlCommand("InsertIntoMultipleTables", connection);
                    connection.Open();

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@BookID", model.B_ID);
                    command.Parameters.AddWithValue("@BookName", model.B_Name);
                    command.Parameters.AddWithValue("@BookType", model.B_Type);
                    command.Parameters.AddWithValue("@FirstName", model.first_name);
                    command.Parameters.AddWithValue("@LastName", model.last_name);
           
                    var result = command.ExecuteNonQuery();
                    //this.connection.Close();
                    if (result != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                connection.Close();
                return false;
            }
            finally
            {
                connection.Close();
            }
        }
        public void AddContactDetailsInDBusingThread(List<ContactsDB> contactDetails)
        {
            ///In this we are passing a list and for each contact in the list different thread is being vrear
            foreach (var contact in contactDetails)
            {
                Stopwatch s = new Stopwatch();
                s.Start();
                Thread thread = new Thread(() =>
                {
                    Console.WriteLine("Thread: " + Thread.CurrentThread.ManagedThreadId);
                    Console.WriteLine($"Contact added:{contact.first_name} {contact.last_name}");
                    if (AddContactDetailsInDB(contact))
                    {
                        Console.WriteLine("Contact Successfully Added");
                    }
                    else
                    {
                        Console.WriteLine("Contact Not Added");
                    }
                });
                thread.Start();
                thread.Join();
            }
            s.Stop();
            Console.WriteLine("Elapsed time to add contacts:" + s.ElapsedMilliseconds);

        }
    }
}
