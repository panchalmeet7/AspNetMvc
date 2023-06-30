using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class User
    {
        public int user_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }

        public string password { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime deleted_at { get; set; }

        public string Role { get; set; }



    }
}
