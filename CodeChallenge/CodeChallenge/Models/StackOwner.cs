using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallenge.Models
{
    public class StackOwner
    {
        public int reputation { get; set; }
        public int user_id { get; set; }
        public string user_type { get; set; }
        public string profile_image { get; set; }
        public string display_name { get; set; }
        public string link { get; set; }
    }
}
