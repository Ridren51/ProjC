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


