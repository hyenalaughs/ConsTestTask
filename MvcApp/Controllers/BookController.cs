using MvcApp.DAL.Repositories;
using MvcApp.DAL.Interfaces;
using System.Web.Mvc;
using MvcApp.Models;
using System;

namespace MvcApp.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _repository = new BookRepository();

        public ActionResult Index()
        {
            try
            {
                var books = _repository.GetAll();
                return View(books);
            }
            catch(Exception ex) 
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return View("Error", model: "Ошибка рпи загрузке книг.");
            }
        }

        public ActionResult Edit(Guid? id)
        {
            try
            {
                if (id == null) return View(new Book());

                var book = _repository.GetById(id.Value.ToString());
                if (book != null)
                    book.Contents = XmlToHtml(book.Contents);

                return View(book ?? new Book());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return View("Error", model: "Ошибка рпи загрузке книг.");
            }
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(Book book)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(book);

                book.Contents = HtmlToXml(book.Contents);

                if (book.Id == Guid.Empty)
                    _repository.Insert(book);
                else
                    _repository.Update(book);

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                ViewBag.ErrorMessage = "Не удалось создать книгу.";
                return View(book);
            }
        }

        public ActionResult Delete(Guid id)
        {
            try
            {
                _repository.Delete(id.ToString());
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return View("Error", model: "Ошибка пр удалении книг.");
            }
        }

        private string XmlToHtml(string xml)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(xml))
                    return string.Empty;

                const string cdataStart = "<![CDATA[";
                const string cdataEnd = "]]>";
                int start = xml.IndexOf(cdataStart);
                int end = xml.LastIndexOf(cdataEnd);

                if (start >= 0 && end > start)
                {
                    return xml.Substring(start + cdataStart.Length, end - (start + cdataStart.Length));
                }

                return xml;
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка при попытке html to xml: {ex.Message}");
                return string.Empty;
            }
        }

        private string HtmlToXml(string html)
        {
            return $"<![CDATA[{html}]]>";
        }
    }
}