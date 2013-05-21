# Jaevner

Jaevner is a tool for exporting data from a Lotus Notes calendar and importing it into a Google Calendar. Yes, there are other tools that are available that do this. Yes, it can be done using the export and import features of Lotus Notes and Google Calendar. But I have created this anyway.


## Installation

There are two parts to this solution. The first is a piece of LotusScript code that exports calendar data to a CSV-file (without a header). The second part is a C# console application that reads this data and imports it into Google Calendar.

### The LotusScript Agent code

The code can be pasted into the code window in a Lotus notes Agent. The Agent can then be set to run on a schedule, or when you feel like it. How Lotus Notes works is beyond the scope of this document.

When you paste the LotusScript code into Lotus Notes, the Const declarations end up in the (Options) section instead of the (Declarations) section. Feel free to move the code if you have strong feelings about such things...

The constants that you can easily tweak are:

* DAYS_TO_KEEP represents how far back in time events will be exported from
* EXPORT_PATH is the directory on your hard drive where files will be written, and where the application should be if you start it after exporting
* RUN_PROGRAM_AFTER_EXPORT defines if you want to start the Jaevner application right after exporting data
* START_PROGRAM_MINIMIZED sets how the application will behave when it is run


### The console application

This application will be called directly from the LotusScript code with the path to the exported file as a parameter. This is the default behavior, but requires you to update the configuration file for the application.


### Technical stuff

The C# console application uses the Google GData API to communicate with Google Calendar.

The .bat file in CSharp\Build will create a single Jaevner.exe file using ILRepack. You must have compiled the project in Release mode first though.

Jaevner.exe can be called with up to five parameters:

* The path to the file containing calendar data
* Calendar URL
* User name
* Password
* Number of days to keep calendar data - older calendar entries will be deleted

Example:

    Jaevner.exe c:\temp\whatever.csv https://www.google.com/calendar/feeds/yourcalendarusername@gmail.com/public/full your.username@gmail.com yoUrP4ssw0rd 14


However, only the path to the file containing calendar data is required. The other settings can be read from a settings file.


## Known issues

* Some exported event descriptions are missing some blankspaces
* All day events are not handled properly yet
* Some encoding issues
* There should be more error handling.


## But what does the word Jaevner actually mean?

Jaevner is the Danish word for sauce thickening.
