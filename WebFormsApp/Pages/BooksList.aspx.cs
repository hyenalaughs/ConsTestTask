using WebFormsApp.DAL.Repositories;
using WebFormsApp.DAL.Interfaces;
using System;

namespace ConsTestTask.WebFormsApp.Pages
{
    public partial class BooksList : System.Web.UI.Page
    {
        private readonly IBookRepository _repository = new BookRepository();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadBooks();
        }

        private void LoadBooks()
        {
            gvBooks.DataSource = _repository.GetAll();
            gvBooks.DataBind();
        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("BookEdit.aspx");
        }

        protected void gvBooks_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditBook")
            {
                Response.Redirect($"BookEdit.aspx?id={e.CommandArgument}");
            }
            else if (e.CommandName == "DeleteBook")
            {
                if (Guid.TryParse(e.CommandArgument.ToString(), out var id))
                {
                    _repository.Delete(id.ToString());
                    LoadBooks();
                }
            }
        }
    }
}