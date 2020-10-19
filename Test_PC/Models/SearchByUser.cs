using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Test_PC.Annotations;
using Test_PC.Db;

namespace Test_PC.Models
{
    public class SearchByUser : IValidatableObject
    {
        /// <summary>
        /// пользователь для поиска
        /// </summary>
        [Display(Name = "Пользователь")]
        public int UserId { get; set; }
        /// <summary>
        /// для вывода select на странице
        /// </summary>
        public IEnumerable<SelectListItem> SelectList { get; set; }
        /// <summary>
        /// определяет будет ли использоваться фильтр
        /// </summary>
        [Display(Name = "Фильтр по дате")]
        public bool NeedDate { get; set; }
        /// <summary>
        /// Начальная дата
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayFormat(HtmlEncode = false, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime? StartDate { get; set; }
        /// <summary>
        /// Конечная дата
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayFormat(HtmlEncode = false, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime? FinalDate { get; set; }

        public SearchByUser()
        {
            IRepository db = new DbReposytory();
            SelectList = new SelectList(db.GetAllUsers(), "Id", "Name"); //заполняем SelectList данными
            db.Dispose();
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (this.NeedDate)
            {
                if (this.StartDate == null)
                {
                    errors.Add(new ValidationResult("Введите начальную дату!"));
                }
            }
            if (this.NeedDate)
            {
                if (this.FinalDate == null)
                {
                    errors.Add(new ValidationResult("Введите конченую дату!"));
                }
            }
            if (UserId <= 0)
            {
                errors.Add(new ValidationResult("Выберете пользователя!"));
            }

            return errors;
        }
    }
}