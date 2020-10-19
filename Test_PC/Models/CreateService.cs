using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Test_PC.Models
{
    public class CreateService
    {
        /// <summary>
        /// базовый адрес сервиса
        /// </summary>        
        [Required]
        public string BaseAddress { get; set; }
    }
}