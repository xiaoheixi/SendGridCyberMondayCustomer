using System.Collections;
using Microsoft.Data.SqlClient;
using SendGrid;
using SendGrid.Helpers.Mail;
Console.WriteLine("Getting Connection ...");
var datasource = @"yqme-prod-syd.database.windows.net"; //Server
var database = "CafeOrder"; //Database Name
var username = "wikiUser@yqme-prod-syd"; //Username
var password = "atJnLwcM8o7dDAU6N60N9jcUaIkhOQ2F4Nq92bGoGo2KSWNPpY"; //Password
string connString = @"Data Source=" + datasource + ";Initial Catalog=" + database + ";Persist Security Info=True;User ID=" + username + ";Password=" + password;
var Command = @"SELECT TOP (1000) [MerchantId]
      ,[WebsiteName],fullName,EmailAddress,UserLogin.mobile
  FROM [dbo].[vCyberMondayMerchants] as m
  LEFT JOIN userLogin ON userLogin.userLoginId = m.primaryUserId
  ORDER BY MerchantId";
SqlConnection conn = new SqlConnection(connString);
SqlCommand Comm1 = new SqlCommand(Command, conn);
var arlist = new ArrayList();
try {
    Console.WriteLine("Openning Connection ...");
    conn.Open();
    Console.WriteLine("Connection successful!");
    SqlDataReader DR1 = Comm1.ExecuteReader();
    while (DR1.Read()) {
        arlist.Add(DR1.GetValue(0).ToString() + ", " + DR1.GetValue(1).ToString() + ", " + DR1.GetValue(2).ToString() + ", " + DR1.GetValue(3).ToString() + ", " + DR1.GetValue(4).ToString());
    }
    conn.Close();
    foreach (string x in arlist) {
        Console.WriteLine(x);
    }
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
    https://www.codeproject.com/Questions/155444/how-to-display-data-from-sql-database-to-textbox-u
    https://www.w3schools.com/cs/cs_foreach_loop.php
*/