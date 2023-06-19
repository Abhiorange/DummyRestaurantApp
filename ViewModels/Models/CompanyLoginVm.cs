using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ViewModels.Models
{
    public class CompanyLoginVm
    {
        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        public string name { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [Display(Name = "Password")]
        public string password { get; set; }
    }
}
