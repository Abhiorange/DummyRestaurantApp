using dataRepository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System.Data;
using ViewModels.Models;

namespace RestaurantWebApi.Areas.admin.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AdminController : ControllerBase
    {
        
        private readonly ICompanyRepository _companyrepo;
        private readonly IConfiguration _configuration;
        public string connectionstring = "Server=LAPTOP-HP9R4JU3\\SQLEXPRESS;Database=RestaurantPOS;Trusted_Connection=True;Encrypt=False";
        public AdminController(ICompanyRepository companyrepo, IConfiguration configuration)
        {
            _companyrepo = companyrepo;
            _configuration = configuration;

        }

        [HttpPost]
        public IActionResult Login(CompanyLoginVm model)
        {
            var userId = _companyrepo.loginrepo(model);
            if (userId != 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Invalid credentials");
            }
          
        }


        [HttpPost]
        public IActionResult Register(CompanyRegisterVm model)
        {
            var i = _companyrepo.registerrepo(model);
            if(i>0)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Not Updated");
            }
           

        }
        [HttpPost]
        public IActionResult AddUser(PostUserRegisterVm model) //Add Users in database
        {
            var i = _companyrepo.adduser(model);
            if(i>0)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Not Updated");
            }
        }

        [HttpGet]
        public IActionResult UserRegister() //Get the company list
        {
            var companies = _companyrepo.GetCompanyList();
            return Ok(companies);
        }
        [HttpGet]
        public IActionResult AllUsersInfo()
        {
            var users = _companyrepo.GetUsersList();
            return Ok(users);
        }
        [HttpPost]
        public IActionResult UserLogin(UserLoginVm model) //User is Logged in
        {
            var userId = _companyrepo.userloginrepo(model);
            if (userId != 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Invalid credentials");
            }
        }


    }
}
/*using (SqlCommand command = new SqlCommand("GetCompanyList", connection))
{
    command.CommandType = CommandType.StoredProcedure;

    using (SqlDataReader reader = command.ExecuteReader())
    {
        while (reader.Read())
        {
            int companyId = (int)reader["CompanyId"];
            string companyName = (string)reader["CompanyName"];
            viewModel.Companies.Add(new SelectListItem { Value = companyId.ToString(), Text = companyName });
        }
    }
}*/