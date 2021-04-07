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
    public class modernaVaccinationModel : PageModel
    {
        private readonly ILogger<modernaVaccinationModel> _logger;

        public modernaVaccinationModel(ILogger<modernaVaccinationModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            try
            {
                using (var webClient = new WebClient())
                {
                    string modernaData = webClient.DownloadString("https://data.cdc.gov/resource/b7pe-5nws.json");

                    JSchema schema = JSchema.Parse(System.IO.File.ReadAllText("modernaSchema.json"));
                    JArray jsonArray = JArray.Parse(modernaData);
                    IList<string> validationEvents = new List<string>();
                    if (jsonArray.IsValid(schema, out validationEvents))
                    {
                        var modernaVaccineDistributionAllocations = ModernaVaccineDistributionAllocations.FromJson(modernaData);
                        ViewData["ModernaVaccineDistributionAllocations"] = modernaVaccineDistributionAllocations;
                    }
                    else
                    {
                        _logger.LogError("moderna json validation failed");
                        foreach (string evt in validationEvents)
                        {
                            _logger.LogWarning($"Error while validating moderna schema {evt}");
                            Console.WriteLine(evt);
                            ViewData["ModernaVaccineDistributionAllocations"] = new ModernaVaccineDistributionAllocations();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured OnGet Moderna {ex}");
            }

        }
    }
}