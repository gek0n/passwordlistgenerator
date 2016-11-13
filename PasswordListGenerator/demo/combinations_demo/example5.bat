@echo off
REM Simple usage combination module
REM Run "example5.bat WORD" to get all combinations with repetititons 
REM with length of 4 elements (more then number of words in file) for words in file example5.txt

SET programm_path="..\..\PasswordListGenerator\bin\Debug\PasswordListGenerator.exe"

%programm_path% comb -i example5.txt -m 4 -r