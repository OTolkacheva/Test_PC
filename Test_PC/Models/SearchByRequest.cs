using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Test_PC.Db;

namespace Test_PC.Models
{
    public class SearchByRequest : IValidatableObject
    {
        /// <summary>
        /// определяет будет ли использоваться фильтр
        /// </summary>
        [Display(Name = "Фильтр по пользователю")]
        public bool NeedName { get; set; }
        /// <summary>
        /// Имя пользователя для фильтра
        /// </summary>
        [Display(Name = "Пользователь")]
        public string Name { get; set; }
        /// <summary>
        /// запрос для поиска
        /// </summary>
        /// 
        [Display(Name = "Запрос")]
        public int RequestId { get; set; }
        /// <summary>
        /// для вывода select на странице
        /// </summary>
        public IEnumerable<SelectListItem> SelectList { get; set; }


        public SearchByRequest()
        {
            IRepository db = new DbReposytory();
            SelectList = new SelectList(db.GetDoneRequst(), "Id", "Request"); //заполняем SelectList данными
            db.Dispose();
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (this.NeedName)
            {
                if (string.IsNullOrWhiteSpace(this.Name))
                {
                    errors.Add(new ValidationResult("Введите пользователя!"));
                }
            }
            if (RequestId <= 0)
            {
                errors.Add(new ValidationResult("Выберете запрос!"));
            }

            return errors;
        }
    }
}