cd src\GL.Console.Secrets

msbuild GL.Console.Secrets.csproj /t:Rebuild /p:TargetFrameworkVersion=v4.5;Configuration=Release;OutputPath=..\..\deploy\net45\
msbuild GL.Console.Secrets.csproj /t:Rebuild /p:TargetFrameworkVersion=v4.6;Configuration=Release;OutputPath=..\..\deploy\net46\
msbuild GL.Console.Secrets.csproj /t:Rebuild /p:TargetFrameworkVersion=v4.7;Configuration=Release;OutputPath=..\..\deploy\net47\

cd ..\GL.Console.Secrets.Core

dotnet publish GL.Console.Secrets.Core.csproj -c Release -o ..\..\deploy\netcoreapp20\

cd ..\..\