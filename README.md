# EasySave
EasySave is a backup solution developed by the ProSoft4 team for Windows users. It allows users to save folders or files on different storage media with a logical and adapted infrastructure for future use and potential evolutions.

## Installation
- Simple and up-to-date installation via GitHub
- Quick and simple installer
- Interface similar to Windows utilities

## Options
- Easy to use and intuitive interface
- Progress visual to track backup progress
- Automatic backups module
- Ability to select multiple files or folders to backup

## Environment of execution
- Windows 7-11 machine
- .NetFramework 4.5-...
- Backup hardware (Virtual/External hard disk..)


## GIT Naming Convention 
We use the following convention : https://www.conventionalcommits.org/en/v1.0.0/

fix(vue) in case we fix a bug
docs(UML) in the case of minor changes to text or deliverables
feat(backup) added new functionality
BREAKING CHANGE:Explanations if you change a critical element of the application
In the case of a breaking chancge composed of a fix or a structural element you can add a 
fix!(vue): Comment


## Code Naming Convention :
We use the following convention : https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions

Camel Case (camelCase): In this standard, the first letter of the word always in small letter and after that each word starts with a capital letter.
Pascal Case (PascalCase): In this the first letter of every word is in capital letter.
Underscore Prefix (_underScore): For underscore ( __ ), the word after _ use camelCase terminology.

Most elements adopt the Pascal method: 
Public field      / MamboJambo
Protected field   / MamboJambo
Internal field	  / MamboJambo
Property 	      / MamboJambo
Method		      / MamboJambo
Class		      / MamboJambo

In the case of an interface we still use the Pascal method but with a capital I in front of it.

Interface 	      / IMamboJambo

For local variables and function parameters we use the Camel method

Local variable    / mamboJambo
Parameter	      / mamboJambo

And finally for private fields we apply the camel method by adding a _ in front of it.

Private field	  / _mamboJambo