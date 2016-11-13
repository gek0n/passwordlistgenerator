@echo off
REM Simple usage combination module
REM Run "example2.bat WORD" to get all combinations with repetitions for words in file example2.txt

SET programm_path="..\..\PasswordListGenerator\bin\Debug\PasswordListGenerator.exe"

%programm_path% comb -i example2.txt -r