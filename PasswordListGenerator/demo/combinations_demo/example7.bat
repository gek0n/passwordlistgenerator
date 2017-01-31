@echo off
REM Simple usage combination module
REM Run "example7.bat WORD" to get all combinations with prefixes for words in file example7.txt

SET programm_path="..\..\PasswordListGenerator\bin\Debug\PasswordListGenerator.exe"

%programm_path% comb -i example7.txt --prefix="prefix_"