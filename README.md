The solution was built using C# .NET Core 2.0 in order to allow the use of Linux containers. 
It contains 3 different projects:

- MQ.CleaningRobot:
  - Domain layer, referenced by the API project and the Client Host.

- MQ.CleaningRobot.Api:
  - Web API with a single controller.
  - The controller has a single method that must be called using POST and accepts a json as input parameter.
  - Exactly as the client application, it will return a json containing the results of the cleaning operation.
  - Swagger UI was configured to be displayed at the root level.
  - It's configured to run inside a Docker Linux container if the docker-compose is set as Startup Project.

- MQ.CleaningRobot.Client:
  - Console application that accepts two arguments "sourceFileName" and "targetFileName"
  - As the console application was created using .NET Core 2.0, it would be necessary to run the following command in order to generate the Windows executable file:
    "dotnet publish MQ.CleaningRobot.Client -c Release -r win10-x64" (without quotation marks)