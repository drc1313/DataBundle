using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace DataBundle.Pages
{
    public class APIAccountsModel : PageModel
    {
        private readonly ILogger<APIAccountsModel> _logger;

        public APIAccountsModel(ILogger<APIAccountsModel> logger)
        {
          
            _logger = logger;
        }

        public int Id { get; set; }

        public string? UserAccount { get; set; }

        public string? DocumenationLink { get; set; }

        public string? ApiHostName { get; set; }

        public string? ApiKey { get; set; }



        public void OnGet()
        {
        }




    }
}