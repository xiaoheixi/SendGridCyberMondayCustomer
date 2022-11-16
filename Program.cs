using SendGrid;
using SendGrid.Helpers.Mail;
namespace Example
{
    internal class Example
    {
        private static void Main()
        {
            Execute().Wait();
        }
        static async Task Execute()
        {
            var apiKey = Environment.GetEnvironmentVariable("CyberMondayCustomerTest");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("confirmation@yqme.com.au", "YQme");
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress("justin.zhao@techsol.net.au", "Justin");
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            Console.WriteLine("Email sent!");
        }
    }
}