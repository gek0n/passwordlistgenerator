﻿using System.Reflection;
using System.Runtime.InteropServices;
using CommandLine;

// Управление общими сведениями о сборке осуществляется с помощью 
// набора атрибутов. Измените значения этих атрибутов, чтобы изменить сведения,
// связанные со сборкой.
[assembly: AssemblyTitle("PasswordListGenerator")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("PasswordListGenerator")]
[assembly: AssemblyCopyright("Copyright © 2016 Zagurskiy Mikhail")]
[assembly: AssemblyInformationalVersion("1.1")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
// from CommandLineParser.Text
[assembly: AssemblyLicense(
	"\nThis is free software. You may redistribute copies of it under the terms of",
	"the MIT License")]
[assembly: AssemblyUsage(
	"\nUsage: PasswordListGenerator <command> [keys] required_value")]

// Параметр ComVisible со значением FALSE делает типы в сборке невидимыми 
// для COM-компонентов.  Если требуется обратиться к типу в этой сборке через 
// COM, задайте атрибуту ComVisible значение TRUE для этого типа.
[assembly: ComVisible(false)]

// Следующий GUID служит для идентификации библиотеки типов, если этот проект будет видимым для COM
[assembly: Guid("4064887e-7307-4f28-addd-6fb6906cf279")]

// Сведения о версии сборки состоят из следующих четырех значений:
//
//      Основной номер версии
//      Дополнительный номер версии 
//   Номер сборки
//      Редакция
//
// Можно задать все значения или принять номера сборки и редакции по умолчанию 
// используя "*", как показано ниже:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.1.*")]
[assembly: AssemblyFileVersion("1.0.0.0")]
