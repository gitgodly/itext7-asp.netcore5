using iPdfDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Kernel.Colors;
using iText.Kernel.Pdf.Canvas.Draw;

namespace iPdfDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            var stream = new MemoryStream();
            /*---------------------------------------------*/

            PdfWriter writer = new(stream);
            PdfDocument pdf = new PdfDocument(writer);

            Document document = new Document(pdf, iText.Kernel.Geom.PageSize.A4.Rotate());
            document.SetMargins(5,30,5,30);

            Paragraph header = new Paragraph("BCMCH BeHive").SetBold().SetTextAlignment(TextAlignment.CENTER).SetFontSize(50);
            Paragraph subheader = new Paragraph("Development Team").SetTextAlignment(TextAlignment.CENTER).SetFontSize(15).SetFontColor(ColorConstants.RED);

            document.Add(header);
            document.Add(subheader);

            Table table = new Table(UnitValue.CreatePercentArray(5)).UseAllAvailableWidth();

            for (int i = 0; i < 50; i++)
            {
                table.AddCell("Cell No abcddsads dsdsad : "  + i.ToString());
            }

            document.Add(table);

            var str = "eget vulputate lobortis. Suspendisse ex nisl, ultricies at congue sit amet, consequat non purus. Aliquam nec tempus enim. Quisque eget maximus nibh. Morbi sagittis quis magna eget suscipit. Sed ultrices at tellus.";

            str = str + str;
            str = str + str; str += str; str = str + str;


            Paragraph np = new Paragraph(str)
            .SetTextAlignment(TextAlignment.LEFT)
            .SetFontSize(12)
            .SetMarginTop(50);
            document.Add(np);
            document.Close();


            /*-------------------------------*/




            byte[] byte1 = stream.ToArray();

            System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
            {
                FileName = "myfile.pdf",
                Inline = true  // false = prompt the user for downloading;  true = browser to try to show the file inline
            };
            Response.Headers.Add("Content-Disposition", cd.ToString());
            Response.Headers.Add("X-Content-Type-Options", "nosniff");

            return File(byte1, "application/pdf");


        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}