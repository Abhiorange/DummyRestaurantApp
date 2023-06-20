using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Models
{
    public class PostUserEditVm
    {
        public string name { get; set; }
        public string email { get; set; }
        public string contact { get; set; }
        public int UserId { get; set; }
        public string password { get; set; }
        public int companyId { get; set; }

    }
}
