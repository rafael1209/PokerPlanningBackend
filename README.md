# PokerPlanningBackend

### Add `appsettings.json`
```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "Google": {
    "ClientId": "",
    "ClientSecret": "",
    "RedirectUrl": ""
  },
  "Jwt": {
    "SecretKey": "",
    "Issuer": ""
  },
  "Email": {
    "SmtpServer": "smtp.gmail.com",
    "SmtpPort": 587,
    "SmtpUser": "",
    "SmtpPassword": ""
  },
  "MongoDb": {
    "ConnectionString": "",
    "Name": ""
  },
  "PasswordHasher": {
    "salt": ""
  }
}
```
