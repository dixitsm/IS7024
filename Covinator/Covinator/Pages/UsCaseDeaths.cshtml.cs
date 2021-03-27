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
    public class UsCaseDeathsModel : PageModel
    {
        /*public void OnGet()
        {
            using (var webClient = new WebClient())
            {
                string jsonString = webClient.DownloadString("https://data.cdc.gov/resource/9mfq-cb36.json");
                JSchema schema = JSchema.Parse(System.IO.File.ReadAllText("casesDeathsSchema.json"));
                JArray jsonArray = JArray.Parse(jsonString);
                IList<string> validationEvents = new List<string>();
                if (jsonArray.IsValid(schema, out validationEvents))
                {
                    var casesDeaths = CasesDeaths.FromJson(jsonString);
                    ViewData["CasesDeaths"] = casesDeaths;
                }
                else
                {
                    foreach (string evt in validationEvents)
                    {
                        Console.WriteLine(evt);
                        ViewData["CasesDeaths"] = new CasesDeaths();
                    }
                }
                
            }
        }*/
        public void OnGet()
        {
            using (var webClient = new WebClient())
            {
                string usCasesData = webClient.DownloadString("https://data.cdc.gov/resource/9mfq-cb36.json");

                JSchema schema = JSchema.Parse(System.IO.File.ReadAllText("casesDeathsSchema.json"));
                JArray jsonArray = JArray.Parse(usCasesData);
                IList<string> validationEvents = new List<string>();
                if (jsonArray.IsValid(schema, out validationEvents))
                {
                    var casesDeaths1 = CasesDeaths.FromJson(usCasesData);
                    ViewData["CasesDeaths"] = casesDeaths1;

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
