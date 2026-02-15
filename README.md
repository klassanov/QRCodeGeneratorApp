# QRCodeGenerator App
Initially started as a QR code generator app for personal use, but later decided to use it also as a playground
for experimenting with some concepts/small building blocks and libraries in .NET.

| Build Azure Pipelines vs Branches|
| :----------- |:-----------:|:-----------:|:-----------:|:-----------:|
| |CI/CD|Nuget|Create Release|azd Deploy|
| develop|[![Build Status](https://dev.azure.com/klassanov/QR%20Generator/_apis/build/status%2FQRCodeGeneratorApp-CI-CD?branchName=develop&label=CI-CD)](https://dev.azure.com/klassanov/QR%20Generator/_build/latest?definitionId=27&branchName=develop)|[![Build Status](https://dev.azure.com/klassanov/QR%20Generator/_apis/build/status%2FQRCodeGeneratorApp-Nuget?branchName=develop&label=Nuget)](https://dev.azure.com/klassanov/QR%20Generator/_build/latest?definitionId=29&branchName=develop) | - | - |
| main|TODO|-|TODO|TODO|
| hotfix/*|TODO|-|-|-|




## List of Experiments/Features

- Global Exception Handling
- Health Checks
- Minimal APIs
- Custom Mediator Impelementation
- Logical CQRS Pattern
- Attempt to Build Clean Architecture Structure
- Central Package Management using Directory.Build.props
- Custom middleware addition 3 options: Conventional, Factory, Inline. Consume a scoped service in custom middleware
- Private NuGet Feed using Azure Artifacts: Package, Push and Consume a private NuGet package QRCodeGenerator.Helper
- Server Sent Events (SSE) implementation
- Channel implementation
- Azure Pipelines for CI/CD
- MongoDB Integration

## List of First-Time Libraries Usage

- AspNetCore.HealthChecks.*
- Carter
- Scalar.AspNetCore
- Scrutor



## TODOs List

- Rate Limiter
- Redis Integration (Caching, Distributed Locking, Message Broker)
- Result Object Pattern
- Aspire Integration Testing
