%~d1
cd "%~p1"
cd "ReportGenerator"
call cmd /K "Reportgenerator.exe -reports:"../../Acr.Assist.CommentMicroService.Service.Tests/coverage.opencover.xml" -targetdir:"../Reports"" 
