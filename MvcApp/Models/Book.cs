using System.ComponentModel.DataAnnotations;
using System;

namespace MvcApp.Models
{
    public class Book
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Название обязательно")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Автор обязателен")]
        public string Author { get; set; }

        [Display(Name = "Год издания")]
        [Range(1000, 2100, ErrorMessage = "Введите корректный год")]
        public int? YearPublish { get; set; }

        public string Genre { get; set; }

        [Display(Name = "Оглавление")]
        public string Contents { get; set; }
    }
}