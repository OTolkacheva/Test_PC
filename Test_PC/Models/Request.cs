using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Test_PC.Annotations;
using Test_PC.Db;

namespace Test_PC.Models
{
    public class Request
    {
        [Display(Name = "Сервис")]
        [Required(ErrorMessage = "Выберете сервис!")]
        public int BaseAddressId { get; set; }
        /// <summary>
        /// базовый адрес
        /// </summary>
        public string BaseAddress { get; set; }
        /// <summary>
        /// limit=
        /// </summary>
        [Display(Name = "Limit")]
        [LimitPage(ErrorMessage = "Limit должен быть числом!")]
        public string Limit { get; set; }
        /// <summary>
        /// page=
        /// </summary>
        [Display(Name = "Page")]
        [LimitPage(ErrorMessage = "Page должен быть числом!")]
        public string Page { get; set; }
        /// <summary>
        /// search=
        /// </summary>
        [Display(Name = "Search")]
        public string Search { get; set; }
        /// <summary>
        /// полный запрос
        /// </summary>
        public string AllRequest
        {
            get
            {
                var allreq = BaseAddress + (Limit != null || Page != null || !string.IsNullOrEmpty(Search) ? "?" : string.Empty) + (Page != null ? $"page={Page + (Limit != null || !string.IsNullOrEmpty(Search) ? "&" : string.Empty)}" : string.Empty) + (Limit != null ? $"limit={Limit + (!string.IsNullOrEmpty(Search) ? "&" : string.Empty)}" : string.Empty) + (!string.IsNullOrEmpty(Search) ? $"search={Search}" : string.Empty);
                return allreq;

            }
        }
        /// <summary>
        /// для вывода select на странице
        /// </summary>
        public IEnumerable<SelectListItem> SelectList { get; set; }

        public Request()
        {
            IRepository db = new DbReposytory();
            SelectList = new SelectList(db.GetServiceSettings(), "Id", "BaseAddress"); //заполняем SelectList данными
            db.Dispose();
        }
    }
}