using EF.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Entity
{
    public class UserBas : IEntity
    {
        public UserBas() { }
      
        public string UserID { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public int Status { get; set; }

        /// <summary>
        /// 返回实体的主键
        /// </summary>
        public string ID
        {
            get { return UserID; }
        }
    }
}
