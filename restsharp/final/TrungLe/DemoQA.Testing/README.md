# Automation Test Project for DemoQA

An automation test project for DemoQA web, built on .NET 8 (C# is the main programming language), NUnit 3.

## Projects in this Solution

There are 3 projects in this solution:

1. **DemoQA.Core**: Contains all things that help to work with API, reading configuration file, creating test reports, etc.
2. **DemoQA.Service**: Contains all things that help to work with API, similar to API Helper.
3. **DemoQA.Testing**: You write tests here and it depends on DemoQA.Core and DemoQA.Service.

## Dependency Packages

| Package         | Description                               | Link                                     |
|-----------------|-------------------------------------------|------------------------------------------|
| ExtentReports   | Beautifully crafted reports               | [https://www.extentreports.com/docs/versions/4/net](https://www.extentreports.com/docs/versions/4/net) |

## Development Tools

This project is set up by using Visual Studio 2022, so you can use it as the main IDE.  
You can also use Visual Code for this project, but you need to install .NET 5 SDK and some extensions for C# language to run this project effectively.

## Configuration Files

- The `appsetting.json` file is the main config file of this project, it allows you to configure the application URL.
- The `extent-config.xml` file is the config file of the ExtentReports library, it allows you to customize the report template like title, theme, etc.

## How to Run Tests

1. **Visual Studio 2022**:
   - Use Test Explorer to select tests to run.
2. **Visual Code**:
   - Install the .NET Core Test Explorer extension and then select tests to run.
3. **Command Lines**:
   - Restore all dependency packages: `dotnet restore`
   - Build project: `dotnet build`
   - Run tests: `dotnet test`
