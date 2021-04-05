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
                string pfizerData = webClient.DownloadString("https://data.cdc.gov/resource/saz5-9hgg.json");
                JSchema schema = JSchema.Parse(System.IO.File.ReadAllText("pfizerSchema.json"));
                JArray jsonArray = JArray.Parse(pfizerData);
                IList<string> validationEvents = new List<string>();
                var currentWeek1 = jsonArray[0].ToArray();
                var currentWeek = currentWeek1[1].ToString();
                if (jsonArray.IsValid(schema, out validationEvents))
                {

                    JArray result_array = new JArray();
                    JArray jarray = new JArray();
                    var length_arr = jsonArray.Count();
                    for (int i=0; i<63; i++)
                    {
                        result_array.Add(jsonArray[i]);

                    }

                    string result_string = result_array.ToString();

                    var pfizerVaccineDistributionAllocations = PfizerVaccineDistributionAllocations.FromJson(result_string);

                    /*ViewData["PfizerVaccineDistributionAllocations"] = pfizerVaccineDistributionAllocations;*/
                    ViewData["PfizerVaccineDistributionAllocations"] = pfizerVaccineDistributionAllocations;
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
        public void OnPost()
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
                    JArray jarray = new JArray();
                    foreach (JObject i in jsonArray
                        .Where(obj => obj["jurisdiction"].Value<string>() == jurisdiction))
                    {
                        result_array.Add(i);
                    }

                    string result_string = result_array.ToString();

                    var pfizerVaccineDistributionAllocations = PfizerVaccineDistributionAllocations.FromJson(result_string);

                    /*ViewData["PfizerVaccineDistributionAllocations"] = pfizerVaccineDistributionAllocations;*/
                    ViewData["PfizerVaccineDistributionAllocations"] = pfizerVaccineDistributionAllocations;
                   
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
