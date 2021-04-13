using IronPython.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Scripting.Hosting;
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
        public IList<string> JurisdictionsList = new List<string>();

        public PfizerVaccination(ILogger<PfizerVaccination> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            
            using (var webClient = new WebClient())
            {
                string pfizerData = webClient.DownloadString("https://data.cdc.gov/resource/saz5-9hgg.json");
                JSchema pfizerSchema = JSchema.Parse(System.IO.File.ReadAllText("pfizerSchema.json"));
                JArray jsonArray = JArray.Parse(pfizerData);
                IList<string> validationEvents = new List<string>();
                var currentWeek1 = jsonArray[0];

                var currentWeek = currentWeek1["week_of_allocations"];


                List<PfizerVaccineDistributionAllocations> currentWeekAllocations = new List<PfizerVaccineDistributionAllocations>();
                if (jsonArray.IsValid(pfizerSchema, out validationEvents))

                {

                    JArray result_array = new JArray();
                    JArray jarray = new JArray();
                    var length_arr = jsonArray.Count();
                    
            
                    foreach (JObject i in jsonArray
                        .Where(obj => obj["week_of_allocations"].ToString() == currentWeek.ToString()))
                    {
                        result_array.Add(i);
                    }

                    string result_string = result_array.ToString();

                    PfizerVaccineDistributionAllocations [] pfizerAllocations = PfizerVaccineDistributionAllocations.FromJson(result_string);


                    ViewData["PfizerVaccineDistributionAllocations"] = pfizerAllocations;

                    ViewData["CurrentWeek"] = currentWeek;
                    

                }
                else
                {


                    foreach (string evt in validationEvents)
                    {
                        Console.WriteLine(evt);
                        ViewData["PfizerVaccineDistributionAllocations"] = new PfizerVaccineDistributionAllocations []{ };
                        
                    }
                }
            }
        }
        public void OnPost()
        {
            var jurisdiction = Request.Form["jurisdiction"];

            using (var webClient = new WebClient())
            {
                string pfizerData = webClient.DownloadString("https://data.cdc.gov/resource/saz5-9hgg.json");
                JSchema schema = JSchema.Parse(System.IO.File.ReadAllText("pfizerSchema.json"));
                JArray jsonArray = JArray.Parse(pfizerData);
                IList<string> validationEvents = new List<string>();
                var currentWeek1 = jsonArray[0];

                var currentWeek = currentWeek1["week_of_allocations"];
                
                if (jsonArray.IsValid(schema, out validationEvents))
                {

                    JArray result_array = new JArray();
                    JArray jarray = new JArray();
                    foreach (JObject i in jsonArray
                        .Where(obj => obj["jurisdiction"].Value<string>() == jurisdiction))
                    {
                        result_array.Add(i);
                    }

                    string result_string = result_array.ToString();

                    var pfizerAllocations = PfizerVaccineDistributionAllocations.FromJson(result_string);

                    /*ViewData["PfizerVaccineDistributionAllocations"] = pfizerVaccineDistributionAllocations;*/
                    ViewData["PfizerVaccineDistributionAllocations"] = pfizerAllocations;
                    ViewData["Jurisdiction"] = jurisdiction;
                    ViewData["CurrentWeek"] = currentWeek;

                }
                else
                {


                    foreach (string evt in validationEvents)
                    {
                        Console.WriteLine(evt);
                        ViewData["PfizerVaccineDistributionAllocations"] = new PfizerVaccineDistributionAllocations();
                    }
                }
            }
        }
    }
}
