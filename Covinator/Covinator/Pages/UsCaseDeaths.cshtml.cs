using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuickType;

namespace Covinator.Pages
{
    public class UsCaseDeathsModel : PageModel
    {
        public void OnGet()
        {
            using (var webClient = new WebClient())
            {
                string jsonString = webClient.DownloadString("https://data.cdc.gov/resource/9mfq-cb36.json");
                var casesDeaths = CasesDeaths.FromJson(jsonString);
                ViewData["CasesDeaths"] = casesDeaths;
            }
        }
    }
}
