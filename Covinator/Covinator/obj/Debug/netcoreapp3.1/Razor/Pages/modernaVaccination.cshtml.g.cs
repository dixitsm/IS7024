#pragma checksum "C:\Users\Moiz\OneDrive\Documents\GitHub\IS7024\IS7024\Covinator\Covinator\Pages\modernaVaccination.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d74df20344441af955ca87b92b9eab5ddbbd4aa1"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(Covinator.Pages.Pages_modernaVaccination), @"mvc.1.0.razor-page", @"/Pages/modernaVaccination.cshtml")]
namespace Covinator.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\Moiz\OneDrive\Documents\GitHub\IS7024\IS7024\Covinator\Covinator\Pages\_ViewImports.cshtml"
using Covinator;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d74df20344441af955ca87b92b9eab5ddbbd4aa1", @"/Pages/modernaVaccination.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"806bedba9cec3a90284c204ec2f2ac243c17bf78", @"/Pages/_ViewImports.cshtml")]
    public class Pages_modernaVaccination : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "C:\Users\Moiz\OneDrive\Documents\GitHub\IS7024\IS7024\Covinator\Covinator\Pages\modernaVaccination.cshtml"
  
    var modernaVaccineDistributionAllocationss = (QuickType.ModernaVaccineDistributionAllocations[])ViewData["ModernaVaccineDistributionAllocations"];

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""text-center"">
    <h1 class=""display-4"">Moderna Vaccine Distribution Allocations</h1>
    <hr />
    <table border=""1"" weidth=""90%"">
        <tr>
            <th>Jurisdiction</th>
            <th>Week of allocations</th>
            <th>1st dose allocations</th>
            <th>2nd dose allocations</th>
");
#nullable restore
#line 16 "C:\Users\Moiz\OneDrive\Documents\GitHub\IS7024\IS7024\Covinator\Covinator\Pages\modernaVaccination.cshtml"
               foreach (QuickType.ModernaVaccineDistributionAllocations modernaVaccineDistributionAllocations in modernaVaccineDistributionAllocationss)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\r\n                    <td>");
#nullable restore
#line 19 "C:\Users\Moiz\OneDrive\Documents\GitHub\IS7024\IS7024\Covinator\Covinator\Pages\modernaVaccination.cshtml"
                   Write(modernaVaccineDistributionAllocations.Jurisdiction);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </td>\r\n                    <td>");
#nullable restore
#line 20 "C:\Users\Moiz\OneDrive\Documents\GitHub\IS7024\IS7024\Covinator\Covinator\Pages\modernaVaccination.cshtml"
                   Write(modernaVaccineDistributionAllocations.WeekOfAllocations);

#line default
#line hidden
#nullable disable
            WriteLiteral(" of allocations </td>\r\n                    <td>");
#nullable restore
#line 21 "C:\Users\Moiz\OneDrive\Documents\GitHub\IS7024\IS7024\Covinator\Covinator\Pages\modernaVaccination.cshtml"
                   Write(modernaVaccineDistributionAllocations.The1StDoseAllocations);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </td>\r\n                    <td>");
#nullable restore
#line 22 "C:\Users\Moiz\OneDrive\Documents\GitHub\IS7024\IS7024\Covinator\Covinator\Pages\modernaVaccination.cshtml"
                   Write(modernaVaccineDistributionAllocations.The2NdDoseAllocations);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </td>\r\n\r\n                </tr>\r\n");
#nullable restore
#line 25 "C:\Users\Moiz\OneDrive\Documents\GitHub\IS7024\IS7024\Covinator\Covinator\Pages\modernaVaccination.cshtml"
 } 

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n\r\n        </table>\r\n\r\n\r\n\r\n    </div>\r\n\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Covinator.Pages.modernaVaccinationModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<Covinator.Pages.modernaVaccinationModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<Covinator.Pages.modernaVaccinationModel>)PageContext?.ViewData;
        public Covinator.Pages.modernaVaccinationModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591