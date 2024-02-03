using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormsContacsCRUD
{
    public class DataAccessLayer
    {
        private SqlConnection conn = new SqlConnection("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=FormsContactsCRUD;Data Source=DESKTOP-NRDN0QF\\SQLEXPRESS"); 
        
        public void InsertContact(Contact contact)
        {
            try
            {
                conn.Open();
                string query = @"
                                INSERT INTO Contacts (FirstName, LastName, Phone, Addres)
                                VALUES (@Firstname, @LastName, @Phone, @Addres)";

                SqlParameter firstName = new SqlParameter("@FirstName", contact.FirstName);
                SqlParameter lastName = new SqlParameter("@LastName", contact.LastName);
                SqlParameter phone = new SqlParameter("@Phone", contact.Phone);
                SqlParameter address = new SqlParameter("@Addres", contact.Address);

                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.Add(firstName);
                command.Parameters.Add(lastName);
                command.Parameters.Add(phone);
                command.Parameters.Add(address);

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public void UpdateContact(Contact contact)
        {
            try
            {
                conn.Open();
                string query = @"UPDATE Contacts
                SET FirstName = @FirstName,
                    LastName = @LastName,
                    Phone = @Phone,
                    Addres = @Addres
                WHERE Id = @id ";

                SqlParameter Id = new SqlParameter("@id", contact.Id);
                SqlParameter firstName = new SqlParameter("@FirstName", contact.FirstName);
                SqlParameter lastName = new SqlParameter("@LastName", contact.LastName);
                SqlParameter phone = new SqlParameter("@Phone", contact.Phone);
                SqlParameter address = new SqlParameter("@Addres", contact.Address);

                SqlCommand command = new SqlCommand(query, conn);

                command.Parameters.Add(Id);
                command.Parameters.Add(firstName);
                command.Parameters.Add(lastName);
                command.Parameters.Add(phone);
                command.Parameters.Add(address);

                command.ExecuteNonQuery();



            }
            catch (Exception)
            {

                
            }
            finally { conn.Close(); }
        }

        public void DeleteContact(int id)
        {
            try
            {
                conn.Open();
                string query = @"DELETE FROM Contacts WHERE id = @Id";

                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.Add(new SqlParameter("@Id", id));

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                                
            }
            finally { conn.Close(); }
        }
    
        public List<Contact> GetContacts(string search = null)
        {
            List<Contact> contacts = new List<Contact>();
            try
            {
                conn.Open();
                string query = @" select id, FirstName, LastName, Phone, Addres
                                    from Contacts";

                SqlCommand command = new SqlCommand();

                if(!string.IsNullOrEmpty(search))
                {
                    query += @" WHERE FirstName LIKE @Search OR LastName LIKE @Search OR Phone LIKE @Search OR 
                                     Addres LIKE @Search";
                    command.Parameters.Add(new SqlParameter("@Search", $"%{search}%"));
                }

                command.CommandText = query;
                command.Connection = conn;

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    contacts.Add(new Contact
                    {
                        Id = int.Parse(reader["id"].ToString()),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Address = reader["Addres"].ToString()
                    }
                     ) ;
                }
            }
            catch (Exception)
            {

                
            }
            finally
            {
                conn.Close();
            }
            return contacts;
        }
    }
}
