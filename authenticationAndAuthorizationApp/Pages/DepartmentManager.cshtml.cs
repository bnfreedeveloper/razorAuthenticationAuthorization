using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace authenticationAndAuthorizationApp.Pages
{
    [Authorize(Policy ="ManagerOnly")]
    public class DepartmentManagerModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
