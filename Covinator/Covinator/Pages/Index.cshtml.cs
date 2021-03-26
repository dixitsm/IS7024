using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using QuickType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;



namespace Covinator.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;



        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }



        public void OnGet()
        {
            using (var webClient = new WebClient())
            {
                string jsonString = webClient.DownloadString("https://data.cdc.gov/resource/b7pe-5nws.json");
                var modernaVaccineDistributionAllocations = ModernaVaccineDistributionAllocations.FromJson(jsonString);
                ViewData["ModernaVaccineDistributionAllocations"] = modernaVaccineDistributionAllocations;
            }
        }
    }
}