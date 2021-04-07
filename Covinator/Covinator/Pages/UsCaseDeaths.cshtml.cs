using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using QuickType;
using System;
using System.Collections.Generic;
using System.Net;

namespace Covinator.Pages
{
    public class UsCaseDeathsModel : PageModel
    {
        private readonly ILogger<UsCaseDeathsModel> _logger;

        public UsCaseDeathsModel(ILogger<UsCaseDeathsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            try
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
                        _logger.LogError("us cases json validation failed");
                        foreach (string evt in validationEvents)
                        {
                            _logger.LogWarning($"Error while validating us cases schema {evt}");
                            Console.WriteLine(evt);
                            ViewData["ModernaVaccineDistributionAllocations"] = new ModernaVaccineDistributionAllocations();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured OnGet US Death cases {ex}");
            }
        }
    }
}