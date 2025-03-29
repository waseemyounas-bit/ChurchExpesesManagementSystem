using BoldReports.RDL.DOM;
using Entities;
using Entities.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services;

namespace Church_App.Pages.ManagementSystem
{
    public class DonationreportsModel : PageModel
    {
        private readonly IDonationService _donationService;
        public DonationreportsModel(IDonationService donationService)
        {
            _donationService = donationService;
        }
        public string report { get; set; } = "DonationReport.rdlc";
        public List<BoldReports.Models.ReportViewer.ReportDataSource> Dataset = new List<BoldReports.Models.ReportViewer.ReportDataSource>();
        public List<BoldReports.Models.ReportViewer.ReportParameter> parameters = new List<BoldReports.Models.ReportViewer.ReportParameter>();
        List<DonationReportDS> datalist = new List<DonationReportDS>();
        public byte[] ImageByteArray { get; private set; }
        List<Logomodel> logos = new List<Logomodel>();
        public List<SelectListItem> Options = new List<SelectListItem>();
        [BindProperty]
        public string SelectedOption { get; set; }
        [BindProperty]
        public DateTime fromdate { get; set; } = DateTime.Now;
        [BindProperty]
        public DateTime todate { get; set; } = DateTime.Now;
        public void OnGet()
        {
            SelectedOption="All";
            Addopetions();
            PreviewDonationReport();
        }
        private void Addopetions()
        {
            Options.Add(new SelectListItem { Text = "All", Value = "All" });
            Options.Add(new SelectListItem { Text = "Visitor", Value = "Visitor" });
            Options.Add(new SelectListItem { Text = "Member", Value = "Member" });
        }
        private void PreviewDonationReport()
        {
            #region SetupLogo
            string imagePath = "logo-dark.png"; // Replace with your image path
            string physicalPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imagePath);

            if (System.IO.File.Exists(physicalPath))

            {

                ImageByteArray = System.IO.File.ReadAllBytes(physicalPath);


                logos.Add(new Logomodel { Logo = ImageByteArray, venderlogo = ImageByteArray });
            }
            else
            {
                // Handle the case where the image file does not exist
                // You can set a default image or display an error message.
            }
            #endregion
            List<Donation> collection = new List<Donation>();
            if (SelectedOption=="All")
            {
                collection = _donationService.GetAllDonations(fromdate,todate);
            }else if (SelectedOption== "Visitor")
            {
                collection = _donationService.GetAllDonations(fromdate, todate,"V");
            }
            else if (SelectedOption == "Member")
            {
                collection = _donationService.GetAllDonations(fromdate, todate, "M");
            }
            foreach (var item in collection)
            {
                datalist.Add(new DonationReportDS { Date= item.Date.ToString("d"), DonnerType = item.MemberId == null ? "Visitor" : "Member", Name = item.MemberId == null ? item.Visitor.FName+" "+item.Visitor.LName : item.Member.FName + " " + item.Member.LName, Amount =Convert.ToDouble(item.Amount) });
            }
               
            parameters.Add(new BoldReports.Models.ReportViewer.ReportParameter() { Name = "type", Values = new List<string>() { SelectedOption } });
            parameters.Add(new BoldReports.Models.ReportViewer.ReportParameter() { Name = "startdate", Values = new List<string>() { fromdate.ToString("dd-MMM-yyyy") } });
            parameters.Add(new BoldReports.Models.ReportViewer.ReportParameter() { Name = "enddate", Values = new List<string>() { todate.ToString("dd-MMM-yyyy") } });
            BoldReports.Models.ReportViewer.ReportDataSource reportDataSource2 = new BoldReports.Models.ReportViewer.ReportDataSource();
            reportDataSource2.Name = "DataSet1";
            reportDataSource2.Value = logos;
            BoldReports.Models.ReportViewer.ReportDataSource reportDataSource = new BoldReports.Models.ReportViewer.ReportDataSource();
            reportDataSource.Name = "DataSet2";
            reportDataSource.Value = datalist;
            Dataset = new List<BoldReports.Models.ReportViewer.ReportDataSource> { reportDataSource2, reportDataSource };
        }

        public async Task<IActionResult> OnPostButtonClickAsync()
        {
            Addopetions();
            SelectedOption = Request.Form["SelectedOption"];
            fromdate = Convert.ToDateTime(Request.Form["fromdate"]);
            todate = Convert.ToDateTime(Request.Form["todate"]);

            PreviewDonationReport();
            return Page();
        }
    }
}
