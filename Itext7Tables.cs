using System;
using System.IO;
using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace iText.Samples.Sandbox.Tables
{
    public class ColumnWidthExample
    {
        public static readonly string DEST = "results/sandbox/tables/column_width_example.pdf";

        public static void Main(String[] args)
        {
            FileInfo file = new FileInfo(DEST);
            file.Directory.Create();

            new ColumnWidthExample().ManipulatePdf(DEST);
        }

        private void ManipulatePdf(string dest)
        {
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(dest));
            Document doc = new Document(pdfDoc, PageSize.A4.Rotate());

            float[] columnWidths = {1, 5, 5};
            Table table = new Table(UnitValue.CreatePercentArray(columnWidths));

            PdfFont f = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            Cell cell = new Cell(1, 3)
                .Add(new Paragraph("This is a header"))
                .SetFont(f)
                .SetFontSize(13)
                .SetFontColor(DeviceGray.WHITE)
                .SetBackgroundColor(DeviceGray.BLACK)
                .SetTextAlignment(TextAlignment.CENTER);

            table.AddHeaderCell(cell);
            
            for (int i = 0; i < 2; i++)
            {
                Cell[] headerFooter =
                {
                    new Cell().SetBackgroundColor(new DeviceGray(0.75f)).Add(new Paragraph("#")),
                    new Cell().SetBackgroundColor(new DeviceGray(0.75f)).Add(new Paragraph("Key")),
                    new Cell().SetBackgroundColor(new DeviceGray(0.75f)).Add(new Paragraph("Value"))
                };

                foreach (Cell hfCell in headerFooter)
                {
                    if (i == 0)
                    {
                        table.AddHeaderCell(hfCell);
                    }
                    else
                    {
                        table.AddFooterCell(hfCell);
                    }
                }
            }

            for (int counter = 0; counter < 100; counter++)
            {
                table.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph((counter + 1).ToString())));
                table.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("key " + (counter + 1))));
                table.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("value " + (counter + 1))));
            }

            doc.Add(table);

            doc.Close();
        }
    }
}
