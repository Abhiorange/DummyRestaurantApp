using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Models;

namespace dataRepository.Interface
{
    public interface ICompanyRepository
    {
        public int loginrepo(CompanyLoginVm model);
        public int userloginrepo(UserLoginVm model);
        public int registerrepo(CompanyRegisterVm model);
        public List<Userinfo> GetUsersList();
        public int adduser(PostUserRegisterVm model);
        public List<SelectListItem> GetCompanyList();
        public UserEditVm GetUserById(int id);
        public int EditUser(PostUserEditVm model);
        public int deleteUserById(int id);

     }
}
