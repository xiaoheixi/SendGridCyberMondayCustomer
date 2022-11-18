using SendGrid;
using SendGrid.Helpers.Mail;  
foreach(string x in File.ReadAllLines(@"C:\Users\Justin Zhao\Documents\CyberMondayCustomersTest.csv")) {
    var response = await new SendGridClient(Environment.GetEnvironmentVariable("CyberMondayCustomerTest")).SendEmailAsync(MailHelper.CreateSingleTemplateEmail(
    new EmailAddress("confirmation@yqme.com.au", "YQme"),
    new EmailAddress("justin.zhao@techsol.net.au", x.Split(',')[1].TrimStart().Replace("'", "")), "d-bed201f8dccf4f4ca2bcd20bbc2a9215",
    new {UserLoginId = x.Split(',')[0].TrimStart().Replace("'", ""), FullName = x.Split(',')[1].TrimStart().Replace("'", ""),
    EmailAddress = x.Split(',')[2].TrimStart().Replace("'", ""), Mobile = x.Split(',')[3].TrimStart().Replace("'", ""),
    Merchant = x.Split(',')[4].TrimStart().Replace("'", ""), DefaultHostname = x.Split(',')[5].TrimStart().Replace("'", ""),
    ThumbnailImage = x.Split(',')[6].TrimStart().Replace("'", "") }));
}