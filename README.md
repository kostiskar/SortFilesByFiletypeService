# SortFilesByFiletypeService

A windows service that scans files in a specific directory and according to their extension move them to a subdirectory with similar files. For example .img and .png files will be moved automatically to the IMG folder.

# What I Learned

* Create windows services
* Work with filesystem
* Use app.config for configuration

Usage
-----

1. Change directory path pointing to the folder you want to apply the service. At app.config change lines 8 - 12 with your path

2. Compile the solution using Visual Studio

3. Inside yourPath\SortFilesByFiletypeService\bin\Debug you will find a file called SortFilesByFiletypeService.exe

4. To start the service 

```bash
  SortFilesByFiletypeService.exe install start
```
5. To unistall the service 

```bash
 SortFilesByFiletypeService.exe  uninstall
```
