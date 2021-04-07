using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using QuickType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Covinator.Pages
{
    public class PfizerVaccination : PageModel
    {
        private readonly ILogger<PfizerVaccination> _logger;

        public PfizerVaccination(ILogger<PfizerVaccination> logger)
        {
            _logger = logger;
        }

        public IList<string> JurisdictionsList = new List<string>();

        public void OnGet()
        {
            try
            {
                using (var webClient = new WebClient())
                {
                    string pfizerData = webClient.DownloadString("https://data.cdc.gov/resource/saz5-9hgg.json");
                    JSchema schema = JSchema.Parse(System.IO.File.ReadAllText("pfizerSchema.json"));
                    JArray jsonArray = JArray.Parse(pfizerData);
                    IList<string> validationEvents = new List<string>();

                    var currentWeek = jsonArray.FirstOrDefault()?["week_of_allocations"]?.Value<DateTime>().ToShortDateString();

                    if (jsonArray.IsValid(schema, out validationEvents))
                    {
                        JArray result_array = new JArray();

                        for (int i = 0; i < 63; i++)
                        {
                            result_array.Add(jsonArray[i]);
                        }

                        var pfizerVaccineDistributionAllocations = PfizerVaccineDistributionAllocations.FromJson(result_array.ToString());

                        PopulateJurisdictions(jsonArray);

                        ViewData["PfizerVaccineDistributionAllocations"] = pfizerVaccineDistributionAllocations;
                        ViewData["CurrentWeek"] = currentWeek;
                    }
                    else
                    {
                        _logger.LogError("pfizer json validation failed");
                        foreach (string evt in validationEvents)
                        {
                            _logger.LogWarning($"Error while validating pfizerschema {evt}");
                            Console.WriteLine(evt);
                            ViewData["PfizerVaccineDistributionAllocations"] = new PfizerVaccineDistributionAllocations();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured OnGet Pfizer {ex}");
            }
        }

        public void OnPost(string State)
        {
            try
            {
                var jurisdiction = Request.Form["jurisdiction"];
                using (var webClient = new WebClient())
                {
                    string pfizerData = webClient.DownloadString("https://data.cdc.gov/resource/saz5-9hgg.json");
                    JSchema schema = JSchema.Parse(System.IO.File.ReadAllText("pfizerSchema.json"));
                    JArray jsonArray = JArray.Parse(pfizerData);
                    IList<string> validationEvents = new List<string>();

                    if (jsonArray.IsValid(schema, out validationEvents))
                    {
                        JArray result_array = new JArray();

                        foreach (JObject i in jsonArray
                            .Where(obj => obj["jurisdiction"].Value<string>() == jurisdiction))
                        {
                            result_array.Add(i);
                        }

                        PopulateJurisdictions(jsonArray);
                        var pfizerVaccineDistributionAllocations = PfizerVaccineDistributionAllocations.FromJson(result_array.ToString());

                        /*ViewData["PfizerVaccineDistributionAllocations"] = pfizerVaccineDistributionAllocations;*/
                        ViewData["PfizerVaccineDistributionAllocations"] = pfizerVaccineDistributionAllocations;
                    }
                    else
                    {
                        _logger.LogError("pfizer json validation failed");
                        foreach (string evt in validationEvents)
                        {
                            _logger.LogWarning($"Error while validating pfizerschema {evt}");
                            Console.WriteLine(evt);
                            ViewData["PfizerVaccineDistributionAllocations"] = new PfizerVaccineDistributionAllocations();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured OnPost Pfizer {ex}");
            }
        }

        private void PopulateJurisdictions(JArray jsonArray)
        {
            _logger.LogInformation("Callinb");
            List<string> locations = new List<string>();
            foreach (JObject item in jsonArray)
            {
                string name = item.GetValue("jurisdiction").ToString();
                locations.Add(name);
            }
            JurisdictionsList = locations.Distinct().ToList();//updates to only distinct 63 records
            JurisdictionsList = JurisdictionsList.OrderBy(x => x).ToList(); //sorts the list alphabettically to populate in dropdown
        }
    }
}