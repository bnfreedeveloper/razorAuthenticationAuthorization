using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace authenticationAndAuthorizationApp.Pages
{
    [Authorize(Policy = "BelongToDepartment")]
    public class DepartmentModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
