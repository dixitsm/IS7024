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
    public class modernaVaccinationModel : PageModel
    {

        private readonly ILogger<modernaVaccinationModel> _logger;
        public IList<string> JurisdictionsList = new List<string>();

        public modernaVaccinationModel(ILogger<modernaVaccinationModel> logger)
        {
            _logger = logger;
        }
        public void OnGet()
        {
            using (var webClient = new WebClient())
            {
                string modernaData = webClient.DownloadString("https://data.cdc.gov/resource/b7pe-5nws.json");
                JSchema modernaSchema = JSchema.Parse(System.IO.File.ReadAllText("modernaSchema.json"));
                JArray jsonArray = JArray.Parse(modernaData);
                IList<string> validationEvents = new List<string>();
                var currentWeek1 = jsonArray[0];
                var currentWeek = currentWeek1["week_of_allocations"];

                List<ModernaVaccineDistributionAllocations> currentWeekAllocations = new List<ModernaVaccineDistributionAllocations>();
                if (jsonArray.IsValid(modernaSchema, out validationEvents))

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

                    ModernaVaccineDistributionAllocations[] modernaAllocations = ModernaVaccineDistributionAllocations.FromJson(result_string);


                    ViewData["ModernaVaccineDistributionAllocations"] = modernaAllocations;

                    ViewData["CurrentWeek"] = currentWeek;


                }
                else
                {


                    foreach (string evt in validationEvents)
                    {
                        Console.WriteLine(evt);
                        ViewData["ModernaVaccineDistributionAllocations"] = new ModernaVaccineDistributionAllocations[] { };

                    }
                }
            }
        }


        public void OnPost()
        {
            var jurisdiction = Request.Form["jurisdiction"];

            using (var webClient = new WebClient())
            {
                string modernaData = webClient.DownloadString("https://data.cdc.gov/resource/saz5-9hgg.json");
                JSchema modernaschema = JSchema.Parse(System.IO.File.ReadAllText("modernaSchema.json"));
                JArray jsonArray = JArray.Parse(modernaData);
                IList<string> validationEvents = new List<string>();
                var currentWeek1 = jsonArray[0];

                var currentWeek = currentWeek1["week_of_allocations"];

                if (jsonArray.IsValid(modernaschema, out validationEvents))
                {

                    JArray result_array = new JArray();
                    JArray jarray = new JArray();
                    foreach (JObject i in jsonArray
                        .Where(obj => obj["jurisdiction"].Value<string>() == jurisdiction))
                    {
                        result_array.Add(i);
                    }

                    string result_string = result_array.ToString();

                    var modernaAllocations = ModernaVaccineDistributionAllocations.FromJson(result_string);

                    ViewData["ModernaVaccineDistributionAllocations"] = modernaAllocations;
                    ViewData["Jurisdiction"] = jurisdiction;
                    ViewData["CurrentWeek"] = currentWeek;

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

