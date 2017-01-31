@echo off
REM Simple usage combination module
REM Run "example4.bat WORD" to get all combinations with repetititons 
REM with length of 4 elements for words in file example4.txt

SET programm_path="..\..\PasswordListGenerator\bin\Debug\PasswordListGenerator.exe"

%programm_path% comb -i example4.txt -m 4 -r