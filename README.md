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

Build the Jaevner.exe file by going to the CSharp\BuildExe folder and start build.bat.

After building the console application (Jaevner.exe) and the associated settings.json files should be placed in the folder specifed in EXPORT_PATH in the LotusScript code. After doing this, edit the settings.json file and enter the URL to your Google calendar, your user name and password. It is recommended that you use an application-specific password and not your master password, of course.

The application will be called directly from the LotusScript code. You can also call it yourself. It takes two parameters:

* The path to the file containing calendar data
* Number of days to keep calendar data - older calendar entries will be deleted (optional, 14 days is used if omitted)

Example:

    Jaevner.exe c:\temp\whatever.csv 30


### Technical stuff

The C# console application uses the Google GData API to communicate with Google Calendar.

The .bat file in CSharp\Build creates a single Jaevner.exe file using ILRepack.


## Known issues

* Some exported event descriptions are missing some blankspaces


## But what does the word Jaevner actually mean?

Jaevner is the Danish word for sauce thickening.
