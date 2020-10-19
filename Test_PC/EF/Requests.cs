using System;
using System.Collections.Generic;

namespace Test_PC.EF
{
    public class Requests
    {
        /// <summary>
        /// идентификатор запроса
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// запрос
        /// </summary>
        public string Request { get; set; }
        /// <summary>
        /// дата выполнения
        /// </summary>
        public DateTime? executAt { get; set; }
        /// <summary>
        /// ссылка на связь запрос-пользователь
        /// </summary>
        public virtual List<RequestToUser> RequestToUsers { get; set; }

        public Requests()
        {
            RequestToUsers = new List<RequestToUser>();
        }
    }
}