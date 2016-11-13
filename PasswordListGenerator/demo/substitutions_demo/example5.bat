@echo off
REM Simple usage substitution module
REM Run "example5.bat" to get all substitutions for words in example5.txt, writen to result_example5.txt

SET programm_path="..\..\PasswordListGenerator\bin\Debug\PasswordListGenerator.exe"

type example4.txt | %programm_path% subs -i -o result_example5.txt