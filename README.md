# QRCodeGenerator App
Initially started as a QR code generator app for personal use, but later decided to use it also as a playground
for experimenting with some concepts/small building blocks and libraries in .NET.

| Builds |
| :----------- |
| [![Main Branch Build](https://dev.azure.com/klassanov/QR%20Generator/_apis/build/status%2Fklassanov.QRCodeGeneratorApp?branchName=master&label=main%20branch%20build)](https://dev.azure.com/klassanov/QR%20Generator/_build/latest?definitionId=27&branchName=master) |
| TODO      | 



## List of Experiments/Features

- Global Exception Handling
- Health Checks
- Minimal APIs
- Custom Mediator Impelementation
- Logical CQRS Pattern
- Attempt to Build Clean Architecture Structure
- Central Package Management using Directory.Build.props
- Custom middleware addition 3 options: Conventional, Factory, Inline. Consume a scoped service in custom middleware

## List of First-Time Libraries Usage

- AspNetCore.HealthChecks.*
- Carter
- Scalar.AspNetCore
- Scrutor



## TODOs List

- Rate Limiter
- MongoDB Integration
- Redis Integration (Caching, Distributed Locking, Message Broker)
- Result Object Pattern
- Azure Pipelines for CI/CD