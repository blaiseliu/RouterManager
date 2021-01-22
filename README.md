# RouterManager
Use Selenium WebDriver to manage a NetGear router

## Example:
To peek the usage:
```
RouterCommand.exe --help
```
![](images/2021-01-22-17-07-16.png)
To block a device
```
RouterCommand.exe -d EchoShow -c Blocked
```
![](images/2021-01-22-17-07-34.png)

To allow a device
```
RouterCommand.exe -d EchoShow -c Allowed
```

## Set up as a Windows Scheduled Task
Open **Task Scheduler**,

Create a new Task.
![](images/2021-01-22-17-07-49.png)

Set New Trigger to be "On a schedule".
![](images/2021-01-22-17-08-19.png)
![](images/2021-01-22-17-08-30.png)

Action is set to "Start a program".

Use `RouterCommand.exe` as the Program and `-d EchoShow -c Blocked` as arguments.
![](images/2021-01-22-17-09-27.png)
![](images/2021-01-22-17-09-35.png)

In the same way, set up another job called **Allow EchoShow**.

![](images/2021-01-22-17-10-15.png)

![](images/2021-01-22-17-10-22.png)

![](images/2021-01-22-17-10-28.png)

![](images/2021-01-22-17-10-51.png)

Make sure both tasks have a status of **Ready**.

![](images/2021-01-22-17-11-02.png)