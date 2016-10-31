@echo off
REM Simple usage substitution module
REM Run "example4.bat" to get all substitutions for words in example4.txt, writen to result_example4.txt

SET programm_path="..\..\PasswordListGenerator\bin\Debug\PasswordListGenerator.exe"

type example4.txt | %programm_path% subs -i > result_example4.txt