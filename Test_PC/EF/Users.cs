using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test_PC.EF
{
    public class Users
    {
        /// <summary>
        /// идентификатор пользователя
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// /// идентификатор пользователя
        /// /// </summary>
        public int Id_User { get; set; }
        /// <summary>
        /// дата создания
        /// </summary>
        public DateTime? createAt { get; set; }
        /// <summary>
        /// имя пользователя
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// ссылка на связь запрос-пользователь
        /// </summary>
        public virtual List<RequestToUser> RequestToUsers { get; set; }

        public Users()
        {
            RequestToUsers = new List<RequestToUser>();
        }
    }
}