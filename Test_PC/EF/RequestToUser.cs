using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test_PC.EF
{
    public class RequestToUser
    {
        /// <summary>
        /// идентификтор связи
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// идентификатор запроса
        /// </summary>
        public int RequestId { get; set; }
        /// <summary>
        /// идентификатор пользователя
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// ссылка на запрос
        /// </summary>
        public virtual Requests Requests { get; set; }
        /// <summary>
        /// ссылка на пользователя
        /// </summary>
        public virtual Users Users { get; set; }
    }
}