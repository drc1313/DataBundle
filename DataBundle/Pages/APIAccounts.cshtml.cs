using Microsoft.AspNetCore.Mvc.RazorPages;
using DataBundle.BL;

namespace DataBundle.Pages
{
    public class APIAccountsModel : APIAccounts
    {
        private readonly ILogger<APIAccountsModel> _logger;

        public APIAccountsModel(ILogger<APIAccountsModel> logger)
        {
              _logger = logger;
        }

        public void OnGet()
        {
            
        }

    }
}