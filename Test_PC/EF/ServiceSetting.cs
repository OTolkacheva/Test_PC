using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test_PC.EF
{
    public class ServiceSetting
    {
        /// <summary>
        /// идентификатор сервиса
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// базовый адрес сервиса
        /// </summary>
        public string BaseAddress { get; set; }
    }
}