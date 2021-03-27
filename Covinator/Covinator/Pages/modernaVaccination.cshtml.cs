using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using QuickType;

namespace Covinator.Pages
{
    public class modernaVaccinationModel : PageModel
    {
        public void OnGet()
        {
            using (var webClient = new WebClient())
            {
                string jsonString = webClient.DownloadString("https://data.cdc.gov/resource/b7pe-5nws.json");

                JSchema schema = JSchema.Parse(System.IO.File.ReadAllText("modernaSchema.json"));
                JArray jsonArray = JArray.Parse(jsonString);
                IList<string> validationEvents = new List<string>();
                if (jsonArray.IsValid(schema, out validationEvents))
                {
                    var modernaVaccineDistributionAllocations = ModernaVaccineDistributionAllocations.FromJson(jsonString);
                    ViewData["ModernaVaccineDistributionAllocations"] = modernaVaccineDistributionAllocations;

                }
                else
                {
                    foreach (string evt in validationEvents)
                    {
                        Console.WriteLine(evt);
                        ViewData["ModernaVaccineDistributionAllocations"] = new ModernaVaccineDistributionAllocations();
                    }
                }
                
            }
        }
    }
}
