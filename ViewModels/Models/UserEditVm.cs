using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Models
{
    public class UserEditVm
    {
        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        public string name { get; set; }
        [Required(ErrorMessage = "Email is required")]
       
        public string email { get; set; }
        [Required(ErrorMessage = "Contact is required")]
       
        public string contact { get; set; }
        [Required(ErrorMessage = "CompanyId is required")]
        
        public List<SelectListItem> Companies { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public int UserId { get; set; }
        public string password { get; set; }
        public int companyId { get; set; }
    }
}
