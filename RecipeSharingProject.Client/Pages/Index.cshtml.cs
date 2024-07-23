using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RecipeSharingProject.Client.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private HttpClient httpClient;
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            httpClient = new HttpClient();
        }

        public void OnGet()
        {

        }
    }
}
