@echo off
REM Simple usage substitution module
REM Run "example1.bat WORD" to get all substitutions for WORD

SET programm_path="..\..\PasswordListGenerator\bin\Debug\PasswordListGenerator.exe"

%programm_path% subs %1