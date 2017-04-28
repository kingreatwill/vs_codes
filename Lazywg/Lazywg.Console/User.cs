using System;

namespace Lazywg.Console
{
    public class User
    {
        /// <summary>
        /// ID
        /// </summary>
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
