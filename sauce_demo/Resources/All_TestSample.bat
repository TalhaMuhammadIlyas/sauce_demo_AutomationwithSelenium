@ECHO OFF
ECHO Demo Automation Executed Started.

call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"

VSTest.Console.exe C:\Users\Lenovo\source\repos\sauce_demo\sauce_demo\bin\Debug\sauce_demo.dll

PAUSE  