using System.Collections;
using Microsoft.Data.SqlClient;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.IO;
var apiKey = Environment.GetEnvironmentVariable("CyberMondayCustomerTest");
var client = new SendGridClient(apiKey);
var from = new EmailAddress("confirmation@yqme.com.au", "YQme");
var to = new EmailAddress("justin.zhao@techsol.net.au", "Justin");
var templateId = "d-bed201f8dccf4f4ca2bcd20bbc2a9215";
Console.WriteLine("Getting Connection ...");
var datasource = @"yqme-prod-syd.database.windows.net"; //Server
var database = "CafeOrder"; //Database Name
var username = "wikiUser@yqme-prod-syd"; //Username
var password = "atJnLwcM8o7dDAU6N60N9jcUaIkhOQ2F4Nq92bGoGo2KSWNPpY"; //Password
string connString = @"Data Source=" + datasource + ";Initial Catalog=" + database + ";Persist Security Info=True;User ID=" + username + ";Password=" + password;
string rootFolder = @"C:\Users\Justin Zhao\Documents";
string textFile = @"C:\Users\Justin Zhao\Documents\CyberMondayCustomers.csv";
var Command = @"SELECT TOP (36000) * FROM vCyberMondayCustomerList";
SqlConnection conn = new SqlConnection(connString);
SqlCommand Comm1 = new SqlCommand(Command, conn);
var arlist = new ArrayList();
var count = 1;
try {
    Console.WriteLine("Openning Connection ...");
    conn.Open();
    Console.WriteLine("Connection successful!");
    
    // SqlDataReader DR1 = Comm1.ExecuteReader();
    // while (DR1.Read()) {
    //     arlist.Add(DR1.GetValue(0).ToString() + ", " + DR1.GetValue(1).ToString() + ", " + DR1.GetValue(2).ToString() + ", " + DR1.GetValue(3).ToString() + ", " + DR1.GetValue(4).ToString() + ", " + DR1.GetValue(5).ToString() + ", " + DR1.GetValue(6).ToString());
    // }

    conn.Close();
    // foreach (string x in arlist) {
    //     var dynamicTemplateData = new
    //     {
    //         subject = "Cyber Monday Specials!",
    //         UserLoginId = x.Split(',')[0].TrimStart().Replace("'", ""), 
    //         FullName = x.Split(',')[1].TrimStart().Replace("'", ""),
    //         EmailAddress = x.Split(',')[2].TrimStart().Replace("'", ""),
    //         Mobile = x.Split(',')[3].TrimStart().Replace("'", ""),
    //         Merchant = x.Split(',')[4].TrimStart().Replace("'", ""),
    //         DefaultHostname = x.Split(',')[5].TrimStart().Replace("'", ""),
    //         ThumbnailImage = x.Split(',')[6].TrimStart().Replace("'", "")
    //     };
    //     var msg = MailHelper.CreateSingleTemplateEmail(from, to, templateId, dynamicTemplateData);
    //     var response = await client.SendEmailAsync(msg);
    //     if (response.IsSuccessStatusCode)
    //     {
    //         Console.WriteLine("Email " + count + " has been sent successfully");
    //     }
    //     count++;
    // }
    if (File.Exists(textFile)) {  
        // Read a text file line by line.  
        string[] lines = File.ReadAllLines(textFile);  
        foreach(string x in lines) {
            Console.WriteLine(x); 
        var dynamicTemplateData = new
        {
            subject = "Cyber Monday Specials!",
            UserLoginId = x.Split(',')[0].TrimStart().Replace("'", ""), 
            FullName = x.Split(',')[1].TrimStart().Replace("'", ""),
            EmailAddress = x.Split(',')[2].TrimStart().Replace("'", ""),
            Mobile = x.Split(',')[3].TrimStart().Replace("'", ""),
            Merchant = x.Split(',')[4].TrimStart().Replace("'", ""),
            DefaultHostname = x.Split(',')[5].TrimStart().Replace("'", ""),
            ThumbnailImage = x.Split(',')[6].TrimStart().Replace("'", "")
        };
        var msg = MailHelper.CreateSingleTemplateEmail(from, to, templateId, dynamicTemplateData);
        var response = await client.SendEmailAsync(msg);
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("Email " + count + " has been sent successfully");
        }
        count++; 
        } 
    }
}
catch (Exception e) {
    Console.WriteLine("Error: " + e.Message);
}
/*
    References
    https://www.twilio.com/blog/send-emails-with-csharp-handlebars-templating-and-dynamic-email-templates
    https://www.codeproject.com/Questions/155444/how-to-display-data-from-sql-database-to-textbox-u
    https://www.w3schools.com/cs/cs_foreach_loop.php
    https://www.c-sharpcorner.com/UploadFile/mahesh/how-to-read-a-text-file-in-C-Sharp/
*/