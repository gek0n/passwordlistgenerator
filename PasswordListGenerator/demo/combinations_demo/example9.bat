@echo off
REM Simple usage combination module
REM Run "example9.bat WORD" to put all combinations for words in file example9.txt into output.txt

SET programm_path="..\..\PasswordListGenerator\bin\Debug\PasswordListGenerator.exe"

%programm_path% comb -i example9.txt -o output.txt