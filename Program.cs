using SendGrid;
using SendGrid.Helpers.Mail;
var apiKey = Environment.GetEnvironmentVariable("CyberMondayCustomerTest");
var client = new SendGridClient(apiKey);
var from = new EmailAddress("confirmation@yqme.com.au", "YQme");
var to = new EmailAddress("justin.zhao@techsol.net.au", "Justin");
var templateId = "d-bed201f8dccf4f4ca2bcd20bbc2a9215";
var dynamicTemplateData = new
{
    subject = "Cyber Monday Specials!",
    recipientName = "Insert PatronName", 
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
/*
    References
    https://www.twilio.com/blog/send-emails-with-csharp-handlebars-templating-and-dynamic-email-templates
*/