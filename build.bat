@echo off

set MsBuildPath="C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\MSBuild\15.0\Bin\MSBuild.exe"

if "%1"=="" %MsBuildPath% build.targets
if not "%1"=="" %MsBuildPath% build.targets /target:%1
