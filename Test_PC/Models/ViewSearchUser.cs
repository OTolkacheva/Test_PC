using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Test_PC.Models
{
    public class ViewSearchUser
    {
        /// <summary>
        /// определяет будет ли использоваться фильтр
        /// </summary>
        [Display(Name = "Фильтр по пользователю")]
        public bool NeedName { get; set; }
        /// <summary>
        /// имя пользователя для поиска
        /// </summary>
        [Display(Name = "Пользователь")]
        [Required(ErrorMessage = "Введите пользователя!")]
        public string Name { get; set; }
    }
}