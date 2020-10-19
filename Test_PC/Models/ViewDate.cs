using System;
using System.ComponentModel.DataAnnotations;

namespace Test_PC.Models
{
    public class ViewDate
    {
        /// <summary>
        /// определяет будет ли использоваться фильтр
        /// </summary>
        [Display(Name = "Фильтр по дате")]
        public bool NeedDate { get; set; }
        /// <summary>
        /// Начальная дата
        /// </summary>
        [Display(Name = "Начальная дата")]
        [Required(ErrorMessage = "Введите начальную дату!")]
        [DataType(DataType.Date)]
        [DisplayFormat(HtmlEncode = false, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime? StartDate { get; set; }
        /// <summary>
        /// Конченая дата
        /// </summary>
        [Display(Name = "Конечная дата")]
        [Required(ErrorMessage = "Введите конченую дату!")]
        [DataType(DataType.Date)]
        [DisplayFormat(HtmlEncode = false, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime? FinalDate { get; set; }
    }
}