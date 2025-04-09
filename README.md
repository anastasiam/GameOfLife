# Game Of Life
There are two main ways to get the application running.

---

## Option 1: Run the pre-published executable

A self-contained version (no pre-installed .NET required) is provided in the repository under the `publish` folder. Choose the folder corresponding to your operating system and run the executable.

### For Windows:

```cmd
cd publish\win-x64
GameOfLife.exe
```

### For macOS:

```cmd
cd publish/osx-x64
./GameOfLife
```

## Option 2: Compile the source code and run the app

### Install .Net
To compile the source code and run the app **.Net 9.0** should be installed on your computer.
If **.Net 9.0** is not installed, please download the installer (version 9.0)
https://dotnet.microsoft.com/en-us/download. Check the installation:

```cmd
dotnet --version
```
Now you can compile and run the app.

### From the GameOfLife project directory run the application (For Windows and macOS):

```cmd
dotnet run
```

*P.S. In preparation to the assignment review I've created a branch with some of the improvements to the original implementation. 
PR with the improvements is here: [if-i-had-more-time](https://github.com/anastasiam/GameOfLife/pull/1)*