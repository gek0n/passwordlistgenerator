@echo off
REM Simple usage substitution module
REM Run "example3.bat" to get all substitutions for words in file example3.txt

SET programm_path="..\..\PasswordListGenerator\bin\Debug\PasswordListGenerator.exe"
type example3.txt | %programm_path% subs -i