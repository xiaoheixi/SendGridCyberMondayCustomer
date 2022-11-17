using SendGrid;
using SendGrid.Helpers.Mail;
var apiKey = Environment.GetEnvironmentVariable("CyberMondayCustomerTest");
var client = new SendGridClient(apiKey);
var from = new EmailAddress("confirmation@yqme.com.au", "YQme");
var to = new EmailAddress("justin.zhao@techsol.net.au", "Justin");
var templateId = "d-bed201f8dccf4f4ca2bcd20bbc2a9215";
string textFile = @"C:\Users\Justin Zhao\Documents\CyberMondayCustomers.csv";
var count = 1;
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
/*
    References
    https://www.twilio.com/blog/send-emails-with-csharp-handlebars-templating-and-dynamic-email-templates
    https://www.codeproject.com/Questions/155444/how-to-display-data-from-sql-database-to-textbox-u
    https://www.w3schools.com/cs/cs_foreach_loop.php
    https://www.c-sharpcorner.com/UploadFile/mahesh/how-to-read-a-text-file-in-C-Sharp/
*/