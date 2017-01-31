@echo off
REM Simple usage combination module
REM Run "example3.bat WORD" to get all combinations with length of 4 elements for words in file example3.txt

SET programm_path="..\..\PasswordListGenerator\bin\Debug\PasswordListGenerator.exe"

%programm_path% comb -i example3.txt -m 4