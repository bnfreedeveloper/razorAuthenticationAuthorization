using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace authenticationAndAuthorizationApp.Pages.Account
{
    
    public class Login : PageModel
    {
        [BindProperty]
        public Credential Accreditation { get; set; } 
        public void OnGet()
        {
            
        }
        public async Task<IActionResult> OnPost()
        {
            //this.Accreditation.UserName = Request.Form["Accreditation.UserName"]; 
            if (!ModelState.IsValid) return Page();
            if (Accreditation.UserName =="admin" && Accreditation.Password == "password")
            {
                //creatin the security context
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "admin"),
                    new Claim(ClaimTypes.Email, "admin@gmail.com"),
                    new Claim("Department","HR"),
                    new Claim("Admin","true"),
                    new Claim("Manager","true"),
                    new Claim("EmploymentDate","2022-04-01")
                };
                //after adding the claims, we specify the authentication type
                var identity = new ClaimsIdentity(claims, "CookieAuth");
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                var cookieProperties = new AuthenticationProperties
                {
                    IsPersistent = Accreditation.RememberMe,
                };
                //here the scheme must be the same as the one in identity (scheme == authenticationType)
                await HttpContext.SignInAsync("CookieAuth", principal,cookieProperties);
                return RedirectToPage("/index");
            }
            return Page();
        }
    }

    public class Credential
    {
        [Required]
        [Display(Name ="Name")]
        public string UserName { get; set; }    

        [Required]
        [DataType(DataType.Password)]   
        public string Password { get; set; }

        [Display(Name ="Remember Me")]
        public bool RememberMe { get; set; }
    }
}
