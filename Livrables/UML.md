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



