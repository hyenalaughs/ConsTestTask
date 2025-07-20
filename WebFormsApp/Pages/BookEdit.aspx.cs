using WebFormsApp.DAL.Repositories;
using WebFormsApp.DAL.Interfaces;
using System;
using ConsTestTask.Library;
using System.Web.UI.WebControls;

namespace ConsTestTask.WebFormsApp.Pages
{
    public partial class BookEdit : System.Web.UI.Page
    {
        private readonly IBookRepository _repository = new BookRepository();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack && !String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    LoadBook(Request.QueryString["id"]);
                }
            }
            catch (Exception ex)
            {
                LogError("Ошибка при загрузке страницы", ex);
                ShowErrorMessage("Не удалось загрузить данные книги.");
            }
        }

        private void LoadBook(string id)
        {
            try
            {
                var book = _repository.GetById(id);

                if (book == null)
                    return;

                hfBookId.Value = book.Id.ToString();
                txtTitle.Text = book.Title;
                txtAuthor.Text = book.Author;
                txtYear.Text = book.YearPublish?.ToString();
                txtGenre.Text = book.Genre;

                txtContentsHtml.Value = XmlToHtml(book.Contents);
            }
            catch(Exception ex)
            {
                LogError("Ошибка при загрузке книги.", ex);
                ShowErrorMessage("Не удалось загрузить данные книги.");
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            try
            {
                bool isEdit = Guid.TryParse(hfBookId.Value, out var existingId);
                var book = new Book
                {
                    Id = isEdit ? existingId : Guid.NewGuid(),
                    Title = txtTitle.Text.Trim(),
                    Author = txtAuthor.Text.Trim(),
                    YearPublish = int.TryParse(txtYear.Text, out var y) ? y : (int?)null,
                    Genre = txtGenre.Text.Trim(),
                    Contents = HtmlToXml(Request.Unvalidated["txtContentsHtml"])
                };

                if (isEdit)
                    _repository.Update(book);
                else
                    _repository.Insert(book);

                Response.Redirect("BooksList.aspx");
            }
            catch (Exception ex)
            {
                LogError("Ошибка при сохранении киг.", ex);
                ShowErrorMessage("Не удалось загрузить данные книги.");
            }
        }

        protected void cvYearMax_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(args.Value))
                {
                    args.IsValid = true;
                    return;
                }
                if (!int.TryParse(args.Value, out var y))
                {
                    args.IsValid = false;
                    return;
                }
                args.IsValid = y <= DateTime.Now.Year;
            }
            catch (Exception ex)
            {
                LogError("Ошибка при валидации года", ex);
                args.IsValid = false;
            }
        }

        protected void cvContents_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                var raw = Request.Unvalidated["txtContentsHtml"] ?? "";
                var stripped = System.Text.RegularExpressions.Regex
                    .Replace(raw, "<.*?>", string.Empty)
                    .Replace("&nbsp;", " ")
                    .Trim();

                if (string.IsNullOrEmpty(stripped))
                {
                    args.IsValid = true;
                    return;
                }

                args.IsValid = stripped.Length >= 5;
            }
            catch(Exception ex)
            {
                LogError("Ошибка при валидации содержания.", ex);
                args.IsValid = false;
            }
        }

        private string HtmlToXml(string html)
        {
            return $"<Contents><![CDATA[{html ?? ""}]]></Contents>";
        }

        private string XmlToHtml(string xml)
        {
            if (string.IsNullOrWhiteSpace(xml)) return "";
            try
            {
                var doc = new System.Xml.XmlDocument();
                doc.LoadXml(xml);
                return doc.InnerText;
            }
            catch(Exception ex)
            {
                LogError("Ошибка при маппинге XML в HTML", ex);
                return xml;
            }
        }

        private void LogError(string message, Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[ERROR] {message}: {ex.Message}");
        }

        private void ShowErrorMessage(string message)
        {
            ClientScript.RegisterStartupScript(
                this.GetType(),
                "ErrorAlert",
                $"alert('{message}');",
                true);
        }
    }
}