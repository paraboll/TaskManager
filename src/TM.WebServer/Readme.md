1) Создание службы: 
    sc create "TM.WebServer" binpath="путь до TM.WebServer.exe"
    sc start TM.WebServer
    sc stop TM.WebServer
    sc stop TM.WebServer
    sc delete TM.WebServer

2) Настройка пути для развертывания сервиса указываеттся в WindowsService.json

{
  "Kestrel": {
    "Endpoints": {
      "Http": {
        "Url": "http://localhost:5000"
      },
      "Https": {
        "Url": "https://localhost:5001"
      }
    }
  }
}

3) swagger: http://localhost:5000/swagger/index.html

4) Пример appsettings.json

{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "DbConfig": {
    //"Provider": "SQLServer",
    //"ConnectionString": "Server=localhost;Database=helloappdb1;User Id=sa;Password=sa;TrustServerCertificate=false;"
    "Provider": "InMemory"
  },

  "JwtToken": {
    "ValidAudience": "Audience",
    "ValidIssuer": "Issuer",
    "Secret": "Some_very_long_secret_key_123456",
    "ExpiryInMinutes": 180
  },

  "ReportFolder" : "Reports",
  "AllowedHosts": "*"
}