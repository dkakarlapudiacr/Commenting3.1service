%~d1
cd "%~p1"
cd ".."
cd "Acr.Assist.CommentMicroService.Service.Tests"
call cmd /K "dotnet test /p:CollectCoverage=true /p:Exclude=\"[*]Acr.Assist.CommentMicroService.Core.*,[*]Acr.Assist.CommentMicroService.Data.*\" /p:ExcludeByFile="../Acr.Assist.CommentMicroService.Service/Startup.Service.cs" /p:CoverletOutputFormat=opencover" 
