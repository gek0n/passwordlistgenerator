@echo off
REM Simple usage combination module
REM Run "example1.bat WORD" to get all combinations for words in file example1.txt

SET programm_path="..\..\PasswordListGenerator\bin\Debug\PasswordListGenerator.exe"

%programm_path% comb -i example1.txt