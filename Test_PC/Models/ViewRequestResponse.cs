using System.Collections.Generic;

namespace Test_PC.Models
{
    public class ViewRequestResponse
    {
        /// <summary>
        /// запрос
        /// </summary>
        public string Request { get; set; }
        /// <summary>
        /// пользователи, полученные из этого запроса
        /// </summary>
        public List<User> LUs { get; set; }
        public ViewRequestResponse()
        {
            LUs = new List<User>();
        }
    }
}