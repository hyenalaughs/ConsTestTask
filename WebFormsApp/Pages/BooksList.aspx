<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BooksList.aspx.cs" Inherits="ConsTestTask.WebFormsApp.Pages.BooksList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Список книг</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" class="container m-4">
        <h2 class="mb-4">Список книг</h2>

        <div class="mb-3">
            <asp:Button id="btnAdd" runat="server" CssClass="btn btn-primary" 
                Text="Добавить книгу" OnClick="BtnAdd_Click" />
        </div>

        <asp:GridView ID="gvBooks" runat="server" AutoGenerateColumns="false" 
            CssClass="table table-bordered table-hover" OnRowCommand="gvBooks_RowCommand">
            <Columns>
                <asp:BoundField DataField="Title" HeaderText="Название"/>
                <asp:BoundField DataField="Author" HeaderText="Автор"/>
                <asp:BoundField DataField="YearPublish" HeaderText="Год публикации"/>
                <asp:BoundField DataField="Genre" HeaderText="Жанр"/>

                <asp:TemplateField HeaderText="Действие" >
                    <ItemTemplate>
                        <asp:LinkButton runat="server" CssClass="btn btn-sm btn-warning me-2"
                                                CommandName="EditBook" CommandArgument='<%# Eval("Id") %>'>Редактировать</asp:LinkButton>
                        <asp:LinkButton runat="server" CssClass="btn btn-sm btn-danger"
                            CommandName="DeleteBook" CommandArgument='<%# Eval("Id") %>' OnClientClick="return confirm('Удалить книгу?');">
                            Удалить
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                </Columns>
        </asp:GridView>
    </form>
</body>
</html>
