# Jaevner

Jaevner is a tool for exporting data from a Lotus Notes calendar and importing it into a Google Calendar. Yes, there are other tools that are available that do this. Yes, it can be done using the export and import features of Lotus Notes and Google Calendar. But I have created this anyway.

It is a simple one-way export/import. Any changes you make on the Google Calendar side will be overwritten the next time the tool is run.


## Installation

There are two parts to this solution. The first is a piece of LotusScript code that exports calendar data to a CSV-file (without a header). The second part is a C# console application that reads this data and imports it into Google Calendar.

### The LotusScript Agent code

The code can be pasted into the code window in a Lotus notes Agent. The Agent can then be set to run on a schedule, or when you feel like it. How Lotus Notes works is beyond the scope of this document.

When you paste the LotusScript code into Lotus Notes, the Const declarations end up in the (Options) section instead of the (Declarations) section. Feel free to move the code if you have strong feelings about such things...

The constants that you can easily tweak are:

* DAYS\_TO\_KEEP represents how far back in time events will be exported from
* EXPORT\_PATH is the directory on your hard drive where files will be written, and where the application should be if you start it after exporting
* RUN\_PROGRAM\_AFTER\_EXPORT defines if you want to start the Jaevner application right after exporting data
* START\_PROGRAM\_MINIMIZED sets how the application will behave when it is run


### The console application

This application will be called directly from the LotusScript code with the path to the exported file as a parameter. This is the default behavior, but requires you to update the settings file for the application.


### Technical stuff

The C# console application uses the Google GData API to communicate with Google Calendar.

The .bat file in CSharp\Build will create a single Jaevner.exe file using ILRepack. You must have compiled the project in Release mode first though.

Jaevner.exe can be called with one or two command line parameters. They are:

* The path to the file containing calendar data
* Number of days to keep calendar data - older calendar entries will be deleted (optional, 14 days is used if omitted)

Example:

    Jaevner.exe c:\temp\whatever.csv 30

The other settings are specified in the settings.json file. They are the URL to the calendar, your user name and password. You can also specify the number of days to keep data.


## Known issues

* Some exported event descriptions are missing some blankspaces
* Some encoding issues


## To-do

* Move to v3 of the API
* There should be more error handling
* More/better documentation


## But what does the word Jaevner actually mean?

Jaevner is the Danish word for sauce thickening.
