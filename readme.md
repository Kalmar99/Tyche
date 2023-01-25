# Tyche - A SaaS Starter

Tyche is a boilerplate / starter app for a Software as a Service (SaaS). It is built with ASP.NET Core on .NET 6 with React.js on the front-end.
It contains all the boilerplate all saas apps must have like a account & user system that supports both normal and B2B customers. 
Authentication & Authorization solution and crucial services like password reset and automatic email sending etc.

## Getting Started 🚀
1. make sure you have .NET 6 sdk installed, you can [find it here](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
2. add configuration to appsettings.json, when developing locally you can set storage connection strings to `UseDevelopmentStorage=true` and use [azurite](https://github.com/Azure/Azurite) to emulate azure blob storage
3. open the solution in Visual Studio or Rider and you are good to go.

## Architecture 🚧
Tyche uses Azure BlobStorage and a Key-Value like approach to storing data, however you are not limited to using azure blob storage if you dont want to! You can just create your own implementation of `IStorageClient` and use whatever database you want under the hood.

## Roadmap 🛣
- Account component that supports both normal and B2B accounts.
  - Add & Remove Users from account
  - Self-service password reset
- Authentication & Authorization component
- Email component


