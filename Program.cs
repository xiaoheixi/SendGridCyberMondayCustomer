using Microsoft.Data.SqlClient;
using SendGrid;
using SendGrid.Helpers.Mail;
Console.WriteLine("Getting Connection ...");
var datasource = @"yqme-prod-syd.database.windows.net"; //Server
var database = "CafeOrder"; //Database Name
var username = "wikiUser@yqme-prod-syd"; //Username
var password = "atJnLwcM8o7dDAU6N60N9jcUaIkhOQ2F4Nq92bGoGo2KSWNPpY"; //Password
string connString = @"Data Source=" + datasource + ";Initial Catalog=" + database + ";Persist Security Info=True;User ID=" + username + ";Password=" + password;
SqlConnection conn = new SqlConnection(connString);
try {
    Console.WriteLine("Openning Connection ...");
    conn.Open();
    Console.WriteLine("Connection successful!");
}
catch (Exception e) {
    Console.WriteLine("Error: " + e.Message);
}
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