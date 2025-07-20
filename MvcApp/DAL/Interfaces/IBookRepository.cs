using System.Collections.Generic;
using MvcApp.Models;

namespace MvcApp.DAL.Interfaces
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
