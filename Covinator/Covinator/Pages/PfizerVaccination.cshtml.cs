using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using QuickType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Covinator.Pages
{
    public class PfizerVaccination : PageModel
    {
        private readonly ILogger<PfizerVaccination> _logger;

        public PfizerVaccination(ILogger<PfizerVaccination> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            using (var webClient = new WebClient())
            {
             string jsonString = webClient.DownloadString("https://data.cdc.gov/resource/saz5-9hgg.json");
                JSchema schema = JSchema.Parse(System.IO.File.ReadAllText("pfizerSchema.json"));
                JArray jsonArray = JArray.Parse(jsonString);
                IList<string> validationEvents = new List<string>(); 
                if (jsonArray.IsValid(schema, out validationEvents)){ 
                    var pfizerVaccineDistributionAllocations = PfizerVaccineDistributionAllocations.FromJson(jsonString);
                    ViewData["PfizerVaccineDistributionAllocations"] = pfizerVaccineDistributionAllocations;

                }
                else
                {
                    foreach(string evt in validationEvents)
                    {
                        Console.WriteLine(evt);
                        ViewData["PfizerVaccineDistributionAllocations"] = new PfizerVaccineDistributionAllocations() ;
                    }
                }
            }
        }
    }
}
