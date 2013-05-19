# Jaevner

Jaevner is a tool for exporting data from a Lotus Notes calendar and importing it into a Google Calendar. Yes, there are other tools that are available that do this. Yes, it can be done using the export and import features of Lotus Notes and Google Calendar. But I have created this anyway.


## Installation

There are two parts to this solution. The first is a piece of LotusScript code that exports calendar data to a CSV-file (without a header). The second part is a C# console application that reads this data and imports it into Google Calendar.

### The LotusScript Agent code

The code can be pasted into the code window in a Lotus notes Agent. The Agent can then be set to run on a schedule, or when you feel like it. How Lotus Notes works is beyond the scope of this document.

You can tweak the directory where the calendar files are exported.


### The console application

This application will be called directly from the LotusScript code.


### Technical stuff

The C# console application uses the Google GData API to communicate with Google Calendar.

The .bat file in CSharp\Build will create a single Jaevner.exe file using ILRepack. You must have compiled the project in Release mode first though.

Jaevner.exe should be called with five parameters:

* The path to the file containing calendar data
* Calendar URL
* User name
* Password
* Number of days to keep calendar data - older calendar entries will be deleted

Example:

    Jaevner.exe c:\temp\whatever.csv https://www.google.com/calendar/feeds/yourcalendarusername@gmail.com/public/full your.username@gmail.com yoUrP4ssw0rd 14


## Known issues

* All day events are not handled properly yet
* Some encoding issues
* There should be more error handling.


## But what does the word Jaevner actually mean?

Jaevner is the Danish word for sauce thickening.

