using ConsTestTask.Library;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System;
using WebFormsApp.DAL.Interfaces;

namespace WebFormsApp.DAL.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly string _connectionString;

        public BookRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["BooksDb"].ConnectionString;
        }

        public List<Book> GetAll()
        {
            var result = new List<Book>();
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("dbo.GetBooks", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        result.Add(ToBookModel(reader));
                }
            }

            return result;
        }

        public Book GetById(string id)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("dbo.GetBookById", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                        return null;

                    return ToBookModel(reader);
                }
            }
        }

        public int Insert(Book book)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("dbo.AddBook", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", book.Id);
                command.Parameters.AddWithValue("@Title", book.Title);
                command.Parameters.AddWithValue("@Author", book.Author);
                command.Parameters.AddWithValue("@YearPublish", book.YearPublish);
                command.Parameters.AddWithValue("@Genre", book.Genre);
                command.Parameters.AddWithValue("@Contents", book.Contents);

                connection.Open();
                var result = command.ExecuteScalar();
                return Convert.ToInt32(result);
            }
        }

        public bool Update(Book book)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("dbo.UpdateBook", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", book.Id);
                command.Parameters.AddWithValue("@Title", book.Title);
                command.Parameters.AddWithValue("@Author", book.Author);
                command.Parameters.AddWithValue("@YearPublish", book.YearPublish);
                command.Parameters.AddWithValue("@Genre", book.Genre);
                command.Parameters.AddWithValue("@Contents", book.Contents);

                connection.Open();
                var rows = (int)command.ExecuteScalar();
                return rows > 0;
            }
        }

        public void Delete(string id)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("dbo.DeleteBook", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                command.ExecuteScalar();
            }
        }

        private Book ToBookModel(SqlDataReader reader)
        {
            return new Book
            {
                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                Title = reader["Title"] as string ?? "",
                Author = reader["Author"] as string ?? "",
                YearPublish = reader["YearPublish"] as int?,
                Genre = reader["Genre"] as string ?? "",
                Contents = reader["Contents"] as string ?? ""
            };
        }
    }
}
