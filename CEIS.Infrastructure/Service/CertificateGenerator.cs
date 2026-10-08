using CEIS.Application.Interfaces.Repositories;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace CEIS.Infrastructure.Service
{
    public class CertificateGenerator : ICertificateGenerator
    {
        public byte[] GenerateCertificate(string studentName, string eventTitle, DateOnly eventDate)
        {
            var document = Document.Create(containar =>
            {
                containar.Page(page =>
                {
                    page.Size(PageSizes.A4.Landscape());
                    page.Margin(50);
                    page.DefaultTextStyle(x => x.FontSize(16));

                    page.Content().Column(col =>
                    {
                        col.Item().AlignCenter().Text("Certificate of Participation")
                            .FontSize(32).Bold();

                        col.Item().PaddingTop(30).AlignCenter().Text("This is to certify that").FontSize(18);

                        col.Item().PaddingTop(10).AlignCenter().Text(studentName)
                            .FontSize(26).Bold();

                        col.Item().PaddingTop(10).AlignCenter().Text($"has successfully participated in \"{eventTitle}\"")
                            .FontSize(18);

                        col.Item().PaddingTop(5).AlignCenter().Text($"held on {eventDate:dd MMMM yyyy}")
                            .FontSize(16);

                        col.Item().PaddingTop(50).AlignCenter().Text("EventSphere - College Event Information System")
                            .FontSize(12).Italic();
                    });
                });
            });

            return document.GeneratePdf();
        }
    }
}
