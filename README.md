# RouterManager
Use Selenium WebDriver to manage a NetGear router

## Example:
To peek the usage:
```
RouterCommand.exe --help
```
To block a device
```
RouterCommand.exe -d EchoShow -c Blocked
```
To allow a device
```
RouterCommand.exe -d EchoShow -c Allowed
```

## Set up as a Windows Scheduled Task
Open *Task Scheduler*,

Set New Trigger to be "On a schedule".




```
RouterCommand.exe -d EchoShow -c Blocked
```



```
RouterCommand.exe -d EchoShow -c Allowed
```



Set up as a Windows Scheduled Task

Open Task Scheduler,
Create a new Task



Set New Trigger to be "On a schedule".

```
C:\Jobs\RouterCommand\RouterCommand.exe -d EchoShow -c Blocked
```


Add another job called Allow EchoShow






Make sure both tasks have a status of Ready.

