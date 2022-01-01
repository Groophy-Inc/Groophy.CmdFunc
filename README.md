# CmdFunc
[![NuGet version (Groophy.CmdFunc)](https://img.shields.io/nuget/v/Groophy.CmdFunc.svg?style=flat-square)](https://www.nuget.org/packages/Groophy.CmdFunc/1.0.1)

[Source Code](https://github.com/Groophy-Inc/Groophy.CmdFunc/blob/main/Groophy.CmdFunc/CmdFunc.cs)

## Usage

### example
```c#
CmdFunc c = new CmdFunc(false, "C:/");
Console.WriteLine(c.Input("Echo hello world"));
```
### output:
```
Hello world
```

### an other example
```c#
CmdFunc c = new CmdFunc(false, "C:/");
Console.WriteLine(c.Input("dir"));
```
### output:
![work](https://user-images.githubusercontent.com/77299279/147858188-e37865bb-10f5-4f7c-9e20-4796af82b30d.PNG)

### Follow Code
![followcode](https://user-images.githubusercontent.com/77299279/147858221-c42714a1-1aec-443b-81cc-a291085c80b4.PNG)

~Groophy Lifefor
