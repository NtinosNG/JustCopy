# JustCopy Console App with C# and .NET Core

This app simply prompts the user to select a directory for copying and for a target directory to copy it to.
It then recursively copies all the contents including any sub-folders and files.

# How to build

Simply clone the repository and make sure you have .NET 5.0 installed on your device and then open a terminal on the root folder of the project and type:

```
dotnet publish
```

# How to use and test

After the project is built, inside the `JustCopy` folder you'll find a folder named `TESTING`. Copy it and paste it inside your build path:

```
JustCopy\bin\Debug\net5.0\Publish
```

Then either `cd` and run the `JustCopy.exe` from a terminal or simply double click it and follow the brief instructions on the console.

> NOTE - This TESTING folder is only here for allowing quick testing of the app; you can always select custom folders to copy as long as you provide the correct paths!

# Some simple features

Well, not features rather helpful additions!

1. The copying execution is timed so you can see at the end how long it took to do the copying.
2. If you try to copy a folder in a directory with a folder with the same name or multiple copies of it, the app will automatically change the name to the next available one. For example, if you want to copy the folder `Test folder` to a directory where there is alreaady a copy named the same way, then the app will change the name to `Test folder - Copy`; and if there is already a copy named like that it will increase it with a number, i.e. `Test folder - Copy (2)` etc. This way it will ensure that it will create the copy no matter what!

# Some suggestions

In order to properly see all the messages that appear on the console when the app is executing, you should adjust your terminal's `Screen Buffer Size` height to a large number.
With Window's command prompt for example this can be done by --> right clicking on the console window --> Properties --> Layout --> Screen Buffer Size --> Height.
The value 1000 worked fine for my tests.

# Personal test

I've successfully managed to copy ALL the contents of my messy Desktop (24.2GB!) in 6.81 minutes!

![copy-test](https://i.imgur.com/xVYCNvi.png)
