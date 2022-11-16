// using SendGrid;
// using SendGrid.Helpers.Mail;
// namespace Example
// {
//     internal class Example
//     {
//         private static void Main()
//         {
//             Execute().Wait();
//         }
//         static async Task Execute()
//         {
//             var apiKey = Environment.GetEnvironmentVariable("CyberMondayCustomerTest");
//             var client = new SendGridClient(apiKey);
//             var from = new EmailAddress("confirmation@yqme.com.au", "YQme");
//             var subject = "Sending with SendGrid is Fun";
//             var to = new EmailAddress("justin.zhao@techsol.net.au", "Justin");
//             var plainTextContent = "plainTextContent";
//             var htmlContent = "Dear Valued Merchant, \x0AMonday 28th November is Cyber Monday. Here at YQme we are planning to give all your \n customers 5% off all their orders for the day. This discount will be covered by YQme. \n We will be sending out an email to your customers, informing them of the promotion this \n Thursday, the 17th. This will be folowed up on Cyber Monday with an SMS and a link to your website. \n This promotion will help drive sales for a Mondayu. If you would like to add your own promotion on \n top of the 5% please get in contact with us. \n \n Enjoy!\n The Team at YQme";
//             var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
//             var response = await client.SendEmailAsync(msg);
//             Console.WriteLine("Email sent!");
//         }
//     }
// }

using SendGrid;
using SendGrid.Helpers.Mail;

var apiKey = Environment.GetEnvironmentVariable("CyberMondayCustomerTest");
var client = new SendGridClient(apiKey);
var from = new EmailAddress("confirmation@yqme.com.au", "YQme");
var to = new EmailAddress("justin.zhao@techsol.net.au", "Justin");

var templateId = "d-bed201f8dccf4f4ca2bcd20bbc2a9215";
var dynamicTemplateData = new
{
    subject = $"To-Do List for {DateTime.UtcNow:MMMM}",
    recipientName = "Valued Customer", 
    todoItemList = new[]
    {
        new { title = "Organize invoices", dueDate = "11 June 2022", status = "Completed" },
        new { title = "Prepare taxes", dueDate = "12 June 2022", status = "In progress" },
        new { title = "Submit taxes", dueDate = "25 June 2022", status = "Pending" },
    }
};
var msg = MailHelper.CreateSingleTemplateEmail(from, to, templateId, dynamicTemplateData);

var response = await client.SendEmailAsync(msg);
if (response.IsSuccessStatusCode)
{
    Console.WriteLine("Email has been sent successfully");
}

//https://www.twilio.com/blog/send-emails-with-csharp-handlebars-templating-and-dynamic-email-templates