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
    public class pfizerCurAlcModel : PageModel
    {
        public IActionResult OnGet()
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

                                      
                    return Content(result_string);

                }
                else
                {


                    foreach (string evt in validationEvents)
                    {
                        Console.WriteLine(evt);
                        
                    }
                }
                return Page();
            }
        }


    }
}
