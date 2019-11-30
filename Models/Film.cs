using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FilmsCatalog.Models
{
    public class Film
    {//название, описание,год выпуска, режиссёр, пользователь, который выложил информацию, постер
        public int Id { get; set; }
        [Display(Name = "Название Фильма")]
        [Required(ErrorMessage = "Пожалуйста, введите название фильма")]
        [MaxLength(256, ErrorMessage = "Максимальная длинна 256 символа")]
        public string Title {get; set; }
        [Display(Name = "Описание фильма")]        
        [MaxLength(1024, ErrorMessage = "Максимальная длинна 1024 символа")]
        public string Description { get; set; }
        [Display(Name = "Год выпуска")]
        public int Year { get; set; }
        [Display(Name = "Режисёр фильма")]
        public string Director { get; set; }
        [Display(Name = "Добавил фильм")]
        public string user { get; set; }
        [Display(Name = "Постер")]
        public string Poster { get; set; }
    }
}