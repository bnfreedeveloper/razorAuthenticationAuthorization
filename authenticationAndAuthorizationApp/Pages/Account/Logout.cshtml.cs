using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace authenticationAndAuthorizationApp.Pages.Account
{
    public class LogoutModel : PageModel
    {
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
           await HttpContext.SignOutAsync("CookieAuth");
            return RedirectToPage("/index");
        }
    }
}
