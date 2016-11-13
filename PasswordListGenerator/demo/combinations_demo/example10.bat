@echo off
REM Simple usage combination module
REM Run "example10.bat WORD" to redirect all combinations for words in file example10.txt into output.txt

SET programm_path="..\..\PasswordListGenerator\bin\Debug\PasswordListGenerator.exe"

%programm_path% comb -i example10.txt > output.txt