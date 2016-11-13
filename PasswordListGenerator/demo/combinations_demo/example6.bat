@echo off
REM Simple usage combination module
REM Run "example6.bat WORD" to get all combinations with suffixes for words in file example6.txt

SET programm_path="..\..\PasswordListGenerator\bin\Debug\PasswordListGenerator.exe"

%programm_path% comb -i example6.txt --suffix="_suffix"