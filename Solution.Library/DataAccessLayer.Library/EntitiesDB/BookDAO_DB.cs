using Model.Library;
using Model.Library.InterfacesDAO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DataAccessLayer.Library.EntitiesDB
{
    public class BookDAO_DB : IBookDAO
    {
        public Book CreateBook(Book book)
        {
            SqlConnection conn = DBConnectionProva.GetSqlConnection();

            try
            {
                string sqlText = "INSERT INTO Books (Title, AuthorName, AuthorSurname, Publisher, Quantity, IsDeleted)" +
                    "VALUES (@Title, @AuthorName, @AuthorSurname, @Publisher, @Quantity, @IsDeleted)";
                SqlCommand cmd = new SqlCommand(sqlText, conn);
                //cmd.Parameters.AddWithValue("@BookId", book.ID);
                cmd.Parameters.AddWithValue("@Title", book.Title);
                cmd.Parameters.AddWithValue("@AuthorName", book.AuthorName);
                cmd.Parameters.AddWithValue("@AuthorSurname", book.AuthorSurname);
                cmd.Parameters.AddWithValue("@Publisher", book.PublishingHouse);
                cmd.Parameters.AddWithValue("@Quantity", book.Quantity);
                cmd.Parameters.AddWithValue("@IsDeleted", 0);

                //ritorna tutte le righe che ho modificato 
                cmd.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                string message = e.Message;
            }
            int id = ReadAllBooks().Select(b => b.ID).Max();
            book.ID = id;
            conn.Close();
            return book;

        }

        public bool DeleteBook(Book book)
        {
            bool delete = true;
            using (SqlConnection conn = DBConnectionProva.GetSqlConnection())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"up_DeleteBook";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlParameter id = new SqlParameter("BookId", System.Data.SqlDbType.Int);
                    id.Value = book.ID;
                    cmd.Parameters.Add(id);

                    cmd.ExecuteNonQuery();

                    //Book deletedBook = ReadAllBooks().Where(d => d.ID == book.ID).FirstOrDefault();
                    //if ( deletedBook.IsDeleted != 1)                                                        
                    //{
                    //    delete = false; 
                    //}

                    conn.Close();
                    return delete;
                }
            }
        }

        public List<Book> ReadAllBooks()
        {
            List<Book> books = new List<Book>();

            using (SqlConnection conn = DBConnectionProva.GetSqlConnection())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM Books";

                    //to receive the data from the previous command
                    SqlDataReader reader = cmd.ExecuteReader();

                    //ora bisogna leggere i dati. Li legge in sequenza 
                    while (reader.Read())
                    {
                        int id = Int32.Parse(reader["BookId"].ToString());
                        string title = reader["Title"].ToString();
                        string authorName = reader["AuthorName"].ToString();
                        string authorSurname = reader["AuthorSurname"].ToString();
                        string publisher = reader["Publisher"].ToString();
                        int quantity = Int32.Parse(reader["Quantity"].ToString());
                        bool isDeleted = bool.Parse(reader["IsDeleted"].ToString());

                        Book book = new Book()
                        {
                            ID = id,
                            Title = title,
                            AuthorName = authorName,
                            AuthorSurname = authorSurname,
                            PublishingHouse = publisher,
                            Quantity = quantity,
                            IsDeleted = isDeleted
                        };
                        books.Add(book);

                    }
                    reader.Close();
                }
                conn.Close();
            }
            return books;
        }

        public Book UpdateBook(int bookId, Book bookWithNewValues)
        {
            using (SqlConnection conn = DBConnectionProva.GetSqlConnection())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"up_UpdateBook";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlParameter _bookId = new SqlParameter("BookId", System.Data.SqlDbType.Int);
                    _bookId.Value = bookId;
                    cmd.Parameters.Add(_bookId);
                    SqlParameter title = new SqlParameter("Title", System.Data.SqlDbType.VarChar, 350);
                    title.Value = bookWithNewValues.Title;
                    cmd.Parameters.Add(title);
                    SqlParameter authorName = new SqlParameter("AuthorName", System.Data.SqlDbType.VarChar, 200);
                    authorName.Value = bookWithNewValues.AuthorName;
                    cmd.Parameters.Add(authorName);
                    SqlParameter authorSurname = new SqlParameter("AuthorSurname", System.Data.SqlDbType.VarChar, 200);
                    authorSurname.Value = bookWithNewValues.AuthorSurname;
                    cmd.Parameters.Add(authorSurname);
                    SqlParameter publisher = new SqlParameter("Publisher", System.Data.SqlDbType.VarChar, 200);
                    publisher.Value = bookWithNewValues.PublishingHouse;
                    cmd.Parameters.Add(publisher);
                    SqlParameter quantity = new SqlParameter("Quantity", System.Data.SqlDbType.Int);
                    quantity.Value = bookWithNewValues.Quantity;
                    cmd.Parameters.Add(quantity);
                    SqlParameter isDeleted = new SqlParameter("IsDeleted", System.Data.SqlDbType.Bit);
                    isDeleted.Value = bookWithNewValues.IsDeleted;
                    cmd.Parameters.Add(isDeleted);

                    cmd.ExecuteNonQuery();

                    conn.Close();
                }
            }
            return bookWithNewValues;
        }
    }
}


