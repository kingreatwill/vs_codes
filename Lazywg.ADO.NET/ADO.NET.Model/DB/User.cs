using ADO.NET.Common;
using ADO.NET.SqlHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET.Model.DB
{
    public class User
    {
        /// <summary>
        /// ID
        /// </summary>
        [SqlParam(IsParam = true, IsPK = true)]
        public string ID { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Sex
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// Age
        /// </summary>
        public Nullable<int> Age { get; set; }

        /// <summary>
        /// IsDelete
        /// </summary>
        public Nullable<bool> IsDelete { get; set; }

        /// <summary>
        /// CreateTime
        /// </summary>
        public Nullable<DateTime> CreateTime { get; set; }

        public User Copy()
        {
            return this.MemberwiseClone() as User;
        }
    }
}
