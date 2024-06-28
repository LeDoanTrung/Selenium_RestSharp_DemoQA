# Automation Test Project for DemoQA

An automation test project for DemoQA web, built on .NET 8 (C# is the main programming language), NUnit 3.

## Projects in this Solution

There are 3 projects in this solution:

1. **DemoQA.Core**: Contains all things that help to work with API, reading configuration file, creating test reports, etc.
2. **DemoQA.Service**: Contains all things that help to work with API, similar to API Helper.
3. **DemoQA.Testing**: You write tests here and it depends on DemoQA.Core and DemoQA.Service.

## Dependency Packages

| Package                                | Description                               | Link                                                                              |
|----------------------------------------|-------------------------------------------|-----------------------------------------------------------------------------------|
| ExtentReports                          | Beautifully crafted reports               | [Documentation](https://www.extentreports.com/docs/versions/4/net)                |
| FluentAssertions                       | Fluent API for asserting test results     | [Website](https://fluentassertions.com/)                                          |
| Microsoft.Extensions.Configuration.Json| Configuration provider for JSON files     | [NuGet](https://www.nuget.org/packages/Microsoft.Extensions.Configuration.Json/)  |
| Newtonsoft.Json                        | JSON framework for .NET                   | [Website](https://www.newtonsoft.com/json)                                        |
| NJsonSchema                            | JSON Schema reader, generator, validator  | [GitHub](https://github.com/RicoSuter/NJsonSchema)                                | 
| RestSharp                              | Simple REST and HTTP API client           | [Website](https://restsharp.dev/)                                                 |
| Selenium.Support                       | Selenium WebDriver support                | [Documentation](https://www.selenium.dev/documentation/en/webdriver/)             |
| Selenium.WebDriver                     | Selenium WebDriver                        | [Documentation](https://www.selenium.dev/documentation/en/webdriver/)             |
| RestSharp.Serializers.NewtonsoftJson   | Newtonsoft JSON serializer for RestSharp  | [Website](https://restsharp.dev/)                                                 |
| Bogus                                  | Fake data generator                       | [GitHub](https://github.com/bchavez/Bogus)                                        |
| Microsoft.NET.Test.Sdk                 | .NET Test SDK                             | [NuGet](https://www.nuget.org/packages/Microsoft.NET.Test.Sdk/)                   |
| NUnit                                  | Unit testing framework                    | [Website](https://nunit.org/)                                                     |
| NUnit3TestAdapter                      | NUnit 3 Test Adapter                      | [GitHub](https://github.com/nunit/nunit3-vs-adapter)                              |
| NUnit.Analyzers                        | NUnit analyzers for static analysis       | [GitHub](https://github.com/nunit/nunit.analyzers)                                |


## Development Tools

This project is set up by using Visual Studio 2022, so you can use it as the main IDE.  
You can also use Visual Code for this project, but you need to install .NET 5 SDK and some extensions for C# language to run this project effectively.

## Configuration Files

- The `appsetting.json` file is the main config file of this project, it allows you to configure the application URL:
{
  "application": {
    "url": "https://demoqa.com"
  }
}
This JSON file specifies the url key under the application section, which configures the base URL for the application under test. Adjust this URL as needed to match your specific testing environment or deployment setup.

- The `extent-config.xml` file is the config file of the ExtentReports library, it allows you to customize the report template like title, theme, etc.

## Test Data
- account_data.json (Located in TestData folder):
{
  "test_user": {
    "userName": "Trung Le",
    "password": "",
    "id": "254dc275-3022-4d61-8ccf-115c6cef3777"
  }
}

Note: For security reasons, the password field in the account_data.json file is intentionally left blank (""). To run the tests successfully, please follow these steps:
Contact Information: Contact ledoantrung1999@gmail.com to obtain the password required for authentication.
Insert Password: Once you have obtained the password, insert it into the "password" field for the "user_1" account in account_data.json.
This ensures that your tests can authenticate properly with the DemoQA application during execution.

## How to Run Tests

1. **Visual Studio 2022**:
   - Use Test Explorer to select tests to run.
2. **Visual Code**:
   - Install the .NET Core Test Explorer extension and then select tests to run.
3. **Command Lines**:
   - Restore all dependency packages: `dotnet restore`
   - Build project: `dotnet build`
   - Run all tests: `dotnet test`
   - Run tests by category: `dotnet test --filter Category=<CategoryName>`
      Example: `dotnet test --filter Category=DeleteBookFromCollection`
   - Run tests in parallel: `dotnet test --parallel`
   - Run tests with specific settings file: `dotnet test --settings "Configuration/appsetting.json"`
   - 