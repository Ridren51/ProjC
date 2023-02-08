# UML 

The EasySave project in C# was designed to simplify data backup operations.
This tool uses the concepts of object-oriented programming according to a MVVM structure to provide a simple and intuitive user 
interface. In this page, we will explore the UML of the EasySave project, which shows how the different classes and objects 
interact to provide a flexible and easy to use data backup solution.

# Gantt 

``` mermaid 
gantt
        title Gantt Diagram
        dateFormat DD-MM-YYYY
        section Phase 1 / Research & Implementation
        Environment setup :done, 23-01-2023, 2d
        GitHub:done, 23-01-2023, 1d
        "Gantt" implementation : done, 24-01-2023, 1d
        Deliverable 0 :crit, 25-01-2023, 1d
        Class diagram: active, 25-01-2023, 2d
        Sequence diagram : active, 26-01-2023, 2d
        UML diagrams : active, 25-01-2023, 4d
        section Phase 2 / Models & Coding
        Variable transposition : 27-01-2023, 5d
        Translate: 29-01-2023, 4d
        Create the basic functions :30-01-2023, 7d
        First operating model :04-02-2023, 2d
        Stable version CLI operation:05-02-2023, 4d 
        Model application checks :07-02-2023, 2d
        Deliverable 1 :crit, 07-02-2023, 1d
        Deliverable 2 :crit, 10-02-2023, 1d
        Post-delivery coorectives:11-02-2023, 2d
        Application, Model Interface :09-02-2023, 14d
        Deliverable 3 :crit, 23-02-2023, 1d
        section Phase 3 / Soutenance
        Powerpoint preparation:22-02-2023, 2d
        Deliverable 4 :crit, 24-0
```
## Explanations 

A Gantt chart is a project planning tool used to visualize the progress of a project over time. It is often used for planning 
software development projects, including those developed with object-oriented programming (OOP).

The Gantt chart presents the project schedule using horizontal bars to represent tasks and dates to mark the scheduled start and
finish of each task. This helps developers plan their time and track deadlines to ensure that the project is on track.

When developing in OOP, the Gantt chart can be used to plan the design and implementation phase of the project, including tasks
such as creating UML diagrams, coding classes and objects, and model testing. It can also include planning the delivery and 
maintenance phase of the project, including preparing slides for the presentation, and post-delivery fixes.

This code represents our Gantt chart:
It shows the duration of the project by breaking down the tasks into the units of time that we have defined. 
The diagram is divided into three sections: 

Phase 1 / Research & Implementation   
Phase 2 / Models & Coding 
Phase 3 / Support

# Sequence Diagram

``` mermaid
sequenceDiagram    
    User->>+View:Launch Application
    View->>+ViewModel:Set Default Language
    ViewModel->>+ConsoleLanguage:Get Text 
    ConsoleLanguage->>+ViewModel:Reply Text
    ViewModel->>+View:Reply Text in Language
    View->>+User:Show Text
    User->>+View:User Input
    View->>+ViewModel:Send User Input
    ViewModel->>+BackupJob:Request new BackupJob
    BackupJob->>+LogManager:Request new LogManager
    LogManager->>+RealTimeLog:Request new RealTimeLog
    RealTimeLog->>+LogManager:Reply RealTimeLog
    LogManager->>+DailyLog:Request Daily Log every 24h
    LogManager->>+BackupJob:Reply LogManager
    BackupJob->>+ViewModel:Reply BackupJob
    ViewModel->>+View:Reply View
```
## Explanations

When the application is launched, the view sets the default language using the template view. The model view then gets text from
the language console and sends a response to the text to the view. The view displays the text in the language chosen for the user.

When the user enters data, the view sends it to the model-view, which sends it to BackupJob for a new backup request. BackupJob 
requests a new LogManager, which in turn requests a new RealTimeLog. The RealTimeLog replies to the LogManager, which replies to
BackupJob with the LogManager. BackupJob then sends a response to the model-view, which passes the response to the view for display. 
Finally, the LogManager makes a daily log request every 24 hours.

# Class Diagram

``` mermaid
classDiagram
    View --* Adapter
    Adapter *-- ConsoleLanguage
    TransferFile --* BackupJob
    TransferFile --> Log
    TransferFile ..|> ITransferFile
    BackupJob --* Adapter
    BackupJob ..|> IBackupJob
    BackupJob --> LogManager
    BackupEnum --o BackupJob
    Adapter *-- Storage
    Adapter --> BackupEnum
    Storage --|> ExternalDisk
    Storage --|> NetworkDisk
    Storage --|> LocalDisk
    LogManager *-- RealTimeLog
    LogManager *-- DailyLog
    RealTimeLog --|> Log
    DailyLog --|> Log
    JsonLog --o Log
    Log ..|> ILog
    
    class View{
      -Adapter _adapter
      -ConsoleLanguage _consoleLanguage
      +View()
      +void Start()
      -void ParseInput(int)
      -void ShowHelp()
      -void SetLanguage()
      -void AddBackupJob()
      -void DeleteBackupJob()
      -void ShowBackupQueue()
      -void RunBackups()
      -void RunSpecificBackup()
    }

    class Adapter{
        +List ~BackupJob~ BackupJobs 
        +ConsoleLanguage ConsoleLanguage 
        +Adapter() 
        +void AddBackupJob(string,string,string,string,int)
        +Boolean IsBackupQueueFull()
        +void RemoveBackupJob(int)
        +BackupJob GetBackupJob(int)
        +Boolean IsNameValid(string?)
        +Boolean IsDirectoryValid(string?)
        +async void RunSpecificBackup(int)
        +async void RunAllBackups()
        -void GenerateEnglishLanguage()
        -void GenerateFrenchLanguage()
        +void SetLanguage(string language)
        +string GetFrenchLanguage()
        +string GetEnglishLanguage()
        +$string GetEnumDescription(Enum)
        +$string GetEnumValues()
        +$BackupEnum GetBackupTypeByIndex(int)
    }

    class ConsoleLanguage{
        -Dictionary ~string, Dictionary~string, string~~ _languages;
        -string _currentLanguage;
        +ConsoleLanguage()
        +void AddLanguage(string, Dictionary'string, string')
        +void SetLanguage(string)
        +string GetString(string)
    }   

    class TransferFile {
        +string BackupName
        +string SourcePath
        +string TargetPath
        +long Size 
        +double? TransferTime
        +TransferFile(string,string,string,long,double?)
    }

    class BackupJob {
        -string _name
        -$System.Timers.Timer timer
        -string _sourceDirectory
        -string _targetDirectory
        +BackupEnum Type 
        +BackupJob(string,string,string,BackupEnum)
        +override string ToString()
        +async Task DoBackup()
        -string GetFileSourcePath(DirectoryInfo,DirectoryInfo,FileInfo)
        -string GetFileTargetPath(DirectoryInfo,DirectoryInfo,FileInfo)
        -$byte[] GetFileHash(string,SHA256)
        -bool CompareHash(DirectoryInfo,DirectoryInfo,FileInfo)
        -void CopyDirectory(string,string,string)
    }

    class ITransferFile {
        << Interface >>
        +string BackupName 
        +string SourcePath 
        +string TargetPath 
        +long Size 
        +double? TransferTime 
    }

    class IBackupJob {
        << Interface >>
        +Task DoBackup();
    }

    class BackupEnum {
        <<Enumeration>>
        +Differential = 1
        +Full = 2
    }

    class DailyLog {
        +DailyLog()
        +override Type LogType => typeof(DailyLog)
        +override void UpdateLog(ITransferFile)
    }

    class ILog {
        << Interface >>
        +void UpdateLog(ITransferFile);
    }

    class JsonLog {
        +string Name 
        +string SourceFilePath 
        +string TargetFilePath 
        +long? FileSize 
        +double? FileTransferTime 
        +string? State 
        +int? TotalFilesToCopy 
        +long? TotalFilesSize 
        +int? NbFilesLeftToDo 
        +int? Progression 
        -string _dateFormat = "dd/MM/yyyy HH:mm:ss";
        +string Time 
        +JsonLog(ITransferFile,RealTimeLog)
        +JsonLog(ITransferFile,DailyLog)
    }

    class Log {
        #string _logPath = "";
        +*Type LogType
        +*void UpdateLog(ITransferFile);
        #void CreateLogFile()
        #string getLogJSON(ITransferFile)
    }

    class LogManager{
        -$DailyLog dailyLog = new DailyLog()
        +$RealTimeLog GetNewRealTimeLog(long,int)
        +$void UpdateDailyLog(ITransferFile)
    }

    class RealTimeLog {
        +override Type LogType => typeof(RealTimeLog);
        -string _state;
        -long _totalFilesSize;
        -int _totalFiles;
        -int _filesLeft;
        -string State 
        -int TotalFiles  
        +long TotalFilesSize 
        +int FilesLeft
        +RealTimeLog(long,int)
        +override void UpdateLog(ITransferFile)
    }

    class ExternalDisk {
        ExternalDisk(string,string) : base(name,path)
    }

    class LocalDisk {
        LocalDisk(string,string) : base(name,path)
    }

    class NetworkDisk {
        NetworkDisk(string,string) : base(name,path)
    }

    class Storage {
        +string Name 
        +string Path 
        +Storage(string,string)
    }
```
## Explanations

This class diagram represents a backup system with several components: View, Adapter, ConsoleLanguage, TransferFile, BackupJob,
Log, ITransferFile, IBackupJob, BackupEnum, Storage, ExternalDisk, NetworkDisk, LocalDisk, LogManager, RealTimeLog, DailyLog,
and JsonLog.

The View class acts as a user interface, allowing the user to start the backup process, add a backup job, delete a backup job, 
show the backup queue, run backups, and run a specific backup. The View class contains an instance of the Adapter class and the
ConsoleLanguage class.

The Adapter class acts as an intermediary between the View class and the BackupJob class. It is responsible for adding, removing,
and retrieving backup jobs, validating the backup name and directory, and running backups. The Adapter class also generates 
different languages, sets the language, and gets the backup type based on the index.

The ConsoleLanguage class contains a dictionary of languages and allows setting and getting the string values for different 
languages.

The TransferFile class represents a file to be backed up, with properties such as BackupName, SourcePath, TargetPath, Size, 
and TransferTime.

The BackupJob class represents a backup job, with properties such as Name, SourceDirectory, TargetDirectory, and BackupEnum Type.
The BackupJob class contains the logic for performing the backup, including copying directories and files and comparing file 
hashes.

The ITransferFile class is an interface that defines the properties of a transfer file, such as BackupName, SourcePath,
TargetPath, Size, and TransferTime.

The IBackupJob interface defines the behavior for performing a backup job.

The BackupEnum is an enumeration with two values: Differential and Full, which represent different backup types.

The Storage class is an abstract class that represents a storage device, with concrete implementations such as ExternalDisk, 
NetworkDisk, and LocalDisk.

The LogManager class contains instances of the RealTimeLog and DailyLog classes, and it is responsible for logging the status
of the backup process.

The RealTimeLog class represents a real-time log of the backup process, and it updates the log when a transfer file is added.

The DailyLog class represents a daily log (every 24 hours) of the backup process, and it updates the log when a transfer file is added.

The JsonLog class represents a JSON-formatted log of a transfer file, with properties such as Name, SourceFilePath, 
TargetFilePath, FileSize, and FileTransferTime.

## Visibility 

To describe the visibility (or encapsulation) of an attribute or method/function that is a part of a class (i.e. a class member),
optional notation may be placed before that members' name:
```C#
+ Public
- Private
# Protected
~ Package/Internal
* Abstract 
$ Static
```

## RelationShip

In a class diagram, you can represent the relationships between classes using various types of links, also known as associations.
Some of the basic types of links in a class diagram are:

Association: represents a basic relationship between two classes, where an instance of one class is associated with an instance 
of another class.

Aggregation: represents a relationship where one class is a part of another class, and the whole-part relationship exists between 
the classes.

Composition: represents a relationship where one class is a part of another class, and the whole-part relationship is strong, 
meaning that the lifetime of the part is dependent on the lifetime of the whole.

Inheritance: represents a relationship where one class is a specialized version of another class, and the subclass inherits the
attributes and behaviors of the superclass.

Realization: represents a relationship where a class implements an interface and satisfies its obligations.

Dependency: represents a relationship where a change in one class may affect another class, but the latter does not own the former.

```C#
classDiagram
classA --|> classB : Inheritance
classC --* classD : Composition
classE --o classF : Aggregation
classG --> classH : Association
classI -- classJ : Link(Solid)
classK ..> classL : Dependency
classM ..|> classN : Realization
classO .. classP : Link(Dashed)
```


