using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace authenticationAndAuthorizationApp.Pages.Account
{
    public class Login: PageModel
    {
        [BindProperty]
        public Credential Accreditation { get; set; }
        public void OnGet()
        {
        }
    }

    public class Credential
    {
        [Required]
        public string UserName { get; set; }    

        [Required]
        [DataType(DataType.Password)]   
        public string Password { get; set; }
    }
}
