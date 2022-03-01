using iPdfDemoCore5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Kernel.Colors;
using iText.Kernel.Pdf.Canvas.Draw;
using System.IO;

namespace iPdfDemoCore5.Controllers
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
            document.SetMargins(5, 30, 5, 30);

            Paragraph header = new Paragraph("BCMCH BeHive").SetBold().SetTextAlignment(TextAlignment.CENTER).SetFontSize(25)
                .SetMargin(2);
            Paragraph subheader = new Paragraph("Development Team").SetTextAlignment(TextAlignment.CENTER).SetFontSize(14)
                 .SetMargin(2)
            .SetFontColor(ColorConstants.BLUE);

            document.Add(header);
            document.Add(subheader);

            ILineDrawer line = new DashedLine(1f);

            line.SetColor(ColorConstants.BLACK);
            LineSeparator ls = new LineSeparator(line);
            ls.SetMarginBottom(5);
            ls.SetMarginTop(5);
            
            Table table = new Table(UnitValue.CreatePercentArray(4)).UseAllAvailableWidth();
            table.SetPadding(5);
            
            for (int i = 0; i < 12; i++)
            {
                table.AddCell("Employee ID : " + i.ToString());
            }

            document.Add(table);
            document.Add(ls);
            document.Add(ls);

            var str = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur porttitor diam ac vulputate pretium. Nunc dolor odio, dictum et odio vitae, imperdiet dapibus mauris. Sed posuere eros quis fringilla iaculis. Nulla tincidunt, dolor viverra imperdiet dictum, lacus ante mollis odio, at luctus arcu sem fermentum justo. Nullam tellus quam, bibendum vel eros non, dapibus finibus lacus. Vivamus molestie est convallis libero elementum, ac vulputate magna scelerisque. Pellentesque vel lorem pharetra, laoreet nisi eu, tincidunt mi. Vestibulum maximus pulvinar eros a venenatis. Nulla ullamcorper sem in tristique iaculis. Pellentesque ut commodo enim. Sed scelerisque nisl in justo tristique, at tincidunt lacus egestas. Suspendisse nec odio quis nisi congue scelerisque sed ut arcu. Curabitur eu metus mi. Aliquam ipsum risus, sollicitudin in blandit nec, tempus ut mi.\nDonec ac urna ipsum. Donec non finibus turpis, eget dignissim nunc. Suspendisse non purus efficitur, semper mi vitae, volutpat nulla. Etiam mollis enim ac urna ornare, at venenatis erat convallis. Quisque sit amet accumsan erat, ac egestas turpis. In vitae purus massa. Mauris aliquet vehicula euismod. Fusce tincidunt tincidunt orci, eget vulputate justo mollis sit amet. Vivamus malesuada porta nisi, faucibus sodales lectus faucibus a. Cras sit amet egestas odio. Maecenas ac congue felis, ac lobortis sapien. Aliquam malesuada, dui et fringilla posuere, arcu felis pulvinar lectus, non congue turpis quam vel risus. Nulla eu volutpat ex, id pulvinar dolor. Nunc vehicula semper tincidunt. Cras elit ex, porttitor eu metus eu, laoreet tempus odio. Proin bibendum augue orci.\nDonec maximus erat ac nibh convallis, at cursus urna dignissim. Nunc et ante ut elit auctor mollis sollicitudin id sem. Sed porttitor id odio at euismod. Integer viverra, nunc accumsan porta dapibus, metus diam viverra dui, ac tempus velit eros ac justo. Duis at bibendum eros, eget dignissim nibh. Donec mauris sem, viverra eu elit sit amet, pellentesque lobortis lectus. Curabitur rutrum tortor neque, vel varius orci placerat ac. Nullam blandit posuere placerat. Integer vel molestie tellus. Nulla auctor eget nunc nec efficitur. Aliquam pulvinar facilisis nisl, id imperdiet leo scelerisque eleifend. Sed a metus vel massa cursus laoreet. Sed at finibus ipsum. Nam sollicitudin luctus ullamcorper.\nDonec efficitur id nulla non fermentum. Suspendisse tristique tincidunt nisi eu interdum. Maecenas ac tellus nec sem lacinia molestie a id felis. Nam eu arcu placerat, aliquam nulla id, varius diam. Vivamus felis ligula, volutpat at ante sit amet, porttitor bibendum diam. Integer nibh tellus, mollis ac finibus et, tincidunt sed lorem. Pellentesque ullamcorper fringilla odio quis fringilla. Suspendisse luctus, quam eu euismod blandit, odio ex pellentesque enim, vitae consectetur nunc ante at metus. Fusce lacinia elementum orci, at dapibus nunc sodales eu. Curabitur a congue justo. Aliquam imperdiet fringilla nunc, sed lobortis nunc pulvinar vitae. Sed lacus ligula, consectetur sit amet lacinia eu, ornare a erat. Morbi aliquam sagittis tincidunt. Aenean diam est, aliquam sed ante congue, aliquam venenatis dolor.";

            str = str + str;
            str = str + str; str += str; str = str + str;


            Paragraph np = new Paragraph(str)
            .SetTextAlignment(TextAlignment.JUSTIFIED)
            .SetFontSize(10)
            .SetMarginTop(20);
            document.Add(np);

            document.Add(ls);

            

 

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
