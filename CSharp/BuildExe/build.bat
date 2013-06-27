set jpath=..\Jaevner.ConsoleApp\bin\Release

..\packages\ILRepack.1.22.2\tools\ILRepack.exe %jpath%\Jaevner.ConsoleApp.exe %jpath%\Jaevner.Core.dll %jpath%\Google.GData.AccessControl.dll %jpath%\Google.GData.Calendar.dll %jpath%\Google.GData.Client.dll %jpath%\Google.GData.Extensions.dll %jpath%\Newtonsoft.Json.dll %jpath%\StructureMap.dll /lib:..\CSharp\Jaevner.ConsoleApp\bin\Release\ /out:Jaevner.exe
copy %jpath%\Settings.json .

pause
