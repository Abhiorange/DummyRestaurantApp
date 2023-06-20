using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestaurantApp.Models;
using System.Configuration;
using System.Diagnostics;
using System.Security.Claims;
using ViewModels.Models;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RestaurantApp.Areas.admin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        Uri baseAddress = new Uri("https://localhost:7189/api");

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult userLogin()
        {
            return PartialView("_userLogin");
        }

        [HttpGet]
        public IActionResult userRegister1()
        {
            return PartialView("_userRegister");
        }

        public IActionResult companyLogin()
        {
            return PartialView("_companyLogin");
        }
        public IActionResult companyRegister()
        {
            return PartialView("_companyRegister");
        }
        public IActionResult dashborad()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(CompanyLoginVm model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string serializedData = JsonConvert.SerializeObject(model);
                    StringContent stringContent = new StringContent(serializedData, Encoding.UTF8, "application/json");
                 
                    HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Admin/Login", stringContent).Result;
                    if (response.IsSuccessStatusCode)
                    {
                  
                        var content = response.Content.ReadAsStringAsync().Result;

                        if (content == "false")
                        {
                            TempData["error"] = "Invalid Credential";
                            return PartialView("_userLogin");
                        }
                        else
                        {
                                TempData["success"] = "Logged in done succesfully";
                                return Redirect("dashborad");          
                        }
                    }
                }
                catch (HttpRequestException ex)
                {
                    ViewData["ErrorMessage"] = "Error: " + ex.Message;
                    return View("Index");
                }
                catch (Exception ex)
                {
                    ViewData["ErrorMessage"] = "Error: " + ex.Message;
                    return View("Index");
                }
            }
            return View("Index");
        }
       
       /* public IActionResult UserLogin() //For User Login
        {
            return View();
        }*/
        [HttpPost]
        public IActionResult UserLogin(UserLoginVm model) //For User Login
        {
            if(ModelState.IsValid)
            {
                try
                {
                    string serializedData = JsonConvert.SerializeObject(model);
                    StringContent stringContent = new StringContent(serializedData, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Admin/UserLogin", stringContent).Result;
                    if (response.IsSuccessStatusCode)
                    {

                        var content = response.Content.ReadAsStringAsync().Result;

                        if (content == "false")
                        {
                            TempData["error"] = "Invalid Credential";
                            return View("Index");
                        }
                        else
                        {
                            TempData["success"] = "Logged in done succesfully";
                            return Redirect("dashborad");
                        }
                    }
                }
                catch (HttpRequestException ex)
                {
                    ViewData["ErrorMessage"] = "Error: " + ex.Message;
                    return View("Index");
                }
                catch (Exception ex)
                {
                    ViewData["ErrorMessage"] = "Error: " + ex.Message;
                    return View("Index");
                }
            }
            return View("Index");
        }
        [HttpGet]
        public IActionResult Register() //For Company Register
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(CompanyRegisterVm model) //For Company Register
        {
            if(ModelState.IsValid)
            {
                try
                {
                    string serializedData = JsonConvert.SerializeObject(model);
                    StringContent stringContent = new StringContent(serializedData, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Admin/Register", stringContent).Result;
                }
                catch (HttpRequestException ex)
                {
                    ViewData["ErrorMessage"] = "Error: " + ex.Message;
                    return View("Index");
                }
                catch (Exception ex)
                {
                    ViewData["ErrorMessage"] = "Error: " + ex.Message;
                    return View("Index");
                }
            }
            return View("Index");
        }
        [HttpGet]
        public IActionResult UserRegister() //For User Register for taking All copanies
        {
            UserRegisterVm model = new UserRegisterVm();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Admin/UserRegister").Result;

            if (response.IsSuccessStatusCode)
            {
                var companiesJson = response.Content.ReadAsStringAsync().Result;
                model.Companies = JsonConvert.DeserializeObject<List<SelectListItem>>(companiesJson);
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult AllUsers() //rendering the list of users in UI
        {
            List<Userinfo> model = new List<Userinfo>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Admin/AllUsersInfo").Result;
            if (response.IsSuccessStatusCode)
            {
                var userJson = response.Content.ReadAsStringAsync().Result;
                model= JsonConvert.DeserializeObject<List<Userinfo>>(userJson);
            }
            return View(model);

        }
        [HttpPost]
        public IActionResult UserRegister(UserRegisterVm model)
        {
            PostUserRegisterVm user = new PostUserRegisterVm
            {
                name=model.name,
                companyId=model.companyId,
                email=model.email,
                contact=model.contact,
                userCode=model.userCode,
                password=model.password
            };
                try
                {
                    string serializedData = JsonConvert.SerializeObject(user);
                    StringContent stringContent = new StringContent(serializedData, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Admin/AddUser", stringContent).Result;

                }
                catch (HttpRequestException ex)
                {
                    ViewData["ErrorMessage"] = "Error: " + ex.Message;
                    return View("Index");
                }
                catch (Exception ex)
                {
                    ViewData["ErrorMessage"] = "Error: " + ex.Message;
                    return View("Index");
                }
            
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult GetUserById(int id)
        {
            UserEditVm model = new UserEditVm();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Admin/GetUserById/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                var userJson = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<UserEditVm>(userJson);
            }
            return View("UserEditPage", model);
        }
        public IActionResult DeleteUserById(int id)
        {
            try
            {
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Admin/DeleteUserById/"+id).Result;
            }
            catch (HttpRequestException ex)
            {
                ViewData["ErrorMessage"] = "Error: " + ex.Message;
                return View("Index");
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Error: " + ex.Message;
                return View("Index");
            }
            return RedirectToAction("AllUsers");
        }
        [HttpPost]
        public IActionResult GetUserById(UserEditVm model)
        {
            PostUserEditVm user = new PostUserEditVm
            {
                UserId=model.UserId,
                name=model.name,
                password=model.password,
                companyId=model.companyId,
                email=model.email,
                contact=model.contact
            };
            try
            {
                string serializedData = JsonConvert.SerializeObject(user);
                StringContent stringContent = new StringContent(serializedData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Admin/EditUser", stringContent).Result;

            }
            catch (HttpRequestException ex)
            {
                ViewData["ErrorMessage"] = "Error: " + ex.Message;
                return View("Index");
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Error: " + ex.Message;
                return View("Index");
            }
            return RedirectToAction("AllUsers");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}