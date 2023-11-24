using Microsoft.AspNetCore.Mvc;
using MovieStore.Models.DTO;
using MovieStore.Repositories.Abstract;

namespace MovieStore.Controllers
{
    public class UserAuthenticationController : Controller
    {
        private IUserAuthenticationService AuthService;
        public UserAuthenticationController(IUserAuthenticationService AuthService)
        {
            this.AuthService = AuthService;
            
        }
        public async Task<IActionResult> Register()
        {
            var model = new RegistrationModel()
            {
                Email = "admin@gmail.com",
                        Username = "admin",
                        Name = "Ravindra",
                        Password = "Admin@123",
                        PasswordConfirm = "Admin@123",
                        Role = "Admin"
            };
            var result = await AuthService.RegisterAsync(model);
            return Ok(result.Message);


        }
        public async Task<IActionResult> Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            var result= await AuthService.LoginAsync(model);
            if(result.StatusCode==1)           
                return RedirectToAction("Index","Home");
           
            else
            {
                TempData["msg"] = "Login failed";
                return RedirectToAction(nameof(Login));
            }
        }


        public async Task<IActionResult> LogOut()
        {
            await    AuthService.LogoutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}
