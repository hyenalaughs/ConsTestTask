using ConsTestTask.Library;
using System.Collections.Generic;

namespace WebFormsApp.DAL.Interfaces
{
    internal interface IBookRepository
    {
        List<Book> GetAll();
        Book GetById(string id);
        int Insert(Book book);
        bool Update(Book book);
        void Delete(string id);
    }
}
