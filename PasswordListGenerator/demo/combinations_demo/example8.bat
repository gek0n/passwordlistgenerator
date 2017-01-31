@echo off
REM Simple usage combination module
REM Run "example8.bat WORD" to get all combinations with delimiter for words in file example8.txt

SET programm_path="..\..\PasswordListGenerator\bin\Debug\PasswordListGenerator.exe"

%programm_path% comb -i example8.txt --delimiter="|_|" -m 3