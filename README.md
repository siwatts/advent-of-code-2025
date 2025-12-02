# Advent of Code 2025

[Advent of Code 2025](https://adventofcode.com)

Just for fun!

Will probably use C++ for most of these, maybe C# for convenience later on

# How-To

## C++

Standalone solutions, compiled to C++23 standard

```sh
g++ program.cc -std=c++23 -Wall
./a.out
```

TODO: Make a single project to re-use code between days

## C#

### Setup

Make a solution file at root level first

```
dotnet new sln --name advent-of-code-2025
```

### Add Day

Create a new C# console app per day. This will create a subdir. of the same name
so cd to where you want this to happen first

```
dotnet new console -n dayX
```

Add the newly created C# project to the top level `.sln` file for vscode functionality

```
dotnet sln advent-of-code-2025.sln add day10/day10-csharp/day10-csharp.csproj
```

### Run

```sh
cd dayX/
dotnet run
# Or dotnet build to only build
```

