<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" CodeBehind="BookEdit.aspx.cs" Inherits="ConsTestTask.WebFormsApp.Pages.BookEdit" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>Редактирование книги</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.ckeditor.com/4.22.1/full/ckeditor.js"></script>
</head>
<body>
    <form id="form1" runat="server" class="container mt-4">
        <h2 class="mb-4">Редактирование книги</h2>
        <asp:HiddenField ID="hfBookId" runat="server" />

        <asp:ValidationSummary ID="valSummary" runat="server"
            ValidationGroup="Book" CssClass="alert alert-danger"
            HeaderText="Исправтьте ошибки:" />

        <div class="card p-4 shadow-sm">
            <div class="mb-3">
                <label for="txtTitle" class="form-label">Название</label>
                <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" MaxLength="200" />
                <asp:RequiredFieldValidator ID="rfvTitle" runat="server"
                    ControlToValidate="txtTitle"
                    ErrorMessage="Название обязательно"
                    Display="Dynamic"
                    CssClass="text-danger"
                    ValidationGroup="Book"/>
            </div>

            <div class="mb-3">
                <label for="txtAuthor" class="form-label">Автор</label>
                <asp:TextBox ID="txtAuthor" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvAuthor" runat="server"
                    ControlToValidate="txtAuthor"
                    ErrorMessage="Автор обязателен"
                    Display="Dynamic"
                    CssClass="text-danger"
                    ValidationGroup="Book" />
            </div>

            <div class="mb-3">
                <label for="txtYear" class="form-label">Год издания</label>
                <asp:TextBox ID="txtYear" runat="server" CssClass="form-control" TextMode="Number" />
                <asp:RangeValidator ID="rvYearRange" runat="server"
                    ControlToValidate="txtYear"
                    MinimumValue="1450"
                    MaximumValue="3000"
                    Type="Integer"
                    ErrorMessage="Год должен быть числом от 1450"
                    Display="Dynamic"
                    CssClass="text-danger"
                    ValidationGroup="Book" />
                <asp:CustomValidator ID="cvYearMax" runat="server"
                    ControlToValidate="txtYear"
                    OnServerValidate="cvYearMax_ServerValidate"
                    ClientValidationFunction="validateYearMax"
                    ErrorMessage="Год не может быть больше текущего"
                    Display="Dynamic"
                    CssClass="text-danger"
                    ValidationGroup="Book" />
            </div>

            <div class="mb-3">
                <label for="txtGenre" class="form-label">Жанр</label>
                <asp:TextBox ID="txtGenre" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                    ControlToValidate="txtTitle"
                    ErrorMessage="Название обязательно"
                    Display="Dynamic"
                    CssClass="text-danger"
                    ValidationGroup="Book"/>
            </div>

            <div class="mb-3">
                <label class="form-label">Оглавление</label>
                <textarea id="txtContentsHtml" name="txtContentsHtml" runat="server" class="form-control"></textarea>
                <asp:CustomValidator ID="cvContents" runat="server"
                    ErrorMessage="Оглавление слишком короткое (минимум 5 символов текста)"
                    OnServerValidate="cvContents_ServerValidate"
                    ClientValidationFunction="validateContents"
                    Display="Dynamic"
                    CssClass="text-danger"
                    ValidationGroup="Book" />
            </div>

            <div class="d-flex gap-2">
                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success"
                    Text="Сохранить" OnClick="BtnSave_Click" ValidationGroup="Book" />
                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-secondary"
                    Text="Отмена" PostBackUrl="BooksList.aspx" CausesValidation="false" />
            </div>
        </div>
    </form>

    <script>
        CKEDITOR.replace('txtContentsHtml', {
            height: 300,
            toolbar: [
                { name: 'clipboard', items: ['Cut', 'Copy', 'Paste', 'Undo', 'Redo'] },
                { name: 'basicstyles', items: ['Bold', 'Italic', 'Underline', 'Strike', '-', 'RemoveFormat'] },
                { name: 'paragraph', items: ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', 'Blockquote'] },
                { name: 'links', items: ['Link', 'Unlink'] },
                { name: 'insert', items: ['Image', 'Table', 'HorizontalRule', 'SpecialChar'] },
                '/',
                { name: 'styles', items: ['Styles', 'Format', 'Font', 'FontSize'] },
                { name: 'colors', items: ['TextColor', 'BGColor'] },
                { name: 'tools', items: ['Maximize', 'Source'] }
            ]
        });

        // Клиентская валидация года (верхняя граница = текущий год)
        function validateYearMax(sender, args) {
            var val = args.Value.trim();
            if (val === '') { args.IsValid = true; return; }
            var n = parseInt(val, 10);
            var currentYear = new Date().getFullYear();
            args.IsValid = (n <= currentYear);
        }

        // Клиентская валидация содержания (минимум 5 видимых символов)
        function validateContents(sender, args) {
            // Синхронизируем textarea с редактором
            if (CKEDITOR.instances['txtContentsHtml']) {
                CKEDITOR.instances['txtContentsHtml'].updateElement();
            }
            var raw = document.getElementById('txtContentsHtml').value || '';
            // Удаляем теги и &nbsp;
            var text = raw.replace(/<[^>]*>/g, '').replace(/&nbsp;/g, ' ').trim();
            args.IsValid = (text.length === 0) || (text.length >= 5); // если хочешь сделать вообще обязательным — убери (text.length === 0) ||
        }
    </script>
</body>
</html>
