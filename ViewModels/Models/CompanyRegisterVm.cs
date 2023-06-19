using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Models
{
    public class CompanyRegisterVm
    {
        [Required(ErrorMessage = "CompanyName is required")]
        [Display(Name = "CompanyName")]
        public string companyName { get; set; }
        [Required(ErrorMessage = "CompanyCode is required")]
        [Display(Name = "CompanyCode")]
        public string companyCode { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Password")]

        public string password { get; set; }
    }
}
