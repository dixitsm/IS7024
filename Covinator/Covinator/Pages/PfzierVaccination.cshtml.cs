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
    public class PfzierVaccination : PageModel
    {
        private readonly ILogger<PfzierVaccination> _logger;

        public PfzierVaccination(ILogger<PfzierVaccination> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            using (var webClient = new WebClient())
            {
             string jsonString = webClient.DownloadString("https://data.cdc.gov/resource/saz5-9hgg.json");
                var pfizerVaccineDistributionAllocations = PfizerVaccineDistributionAllocations.FromJson(jsonString);
                ViewData["PfizerVaccineDistributionAllocations"] = pfizerVaccineDistributionAllocations;
            }
        }
    }
}
