# Work in progress
 
 Zygieldesk - zendesk like customer service app. Clean architecture + CQRS + MediatR

 # How to launch, to test progress:

 1. Clone repository (master branch).
 2. Set Zygieldesk.API as a Startup Project.
 3. Provide valid Connection string in appsettings.json "ZygieldeskConnectionString" section.
 4. Open NuGet Package Manager Console
 5. In Package Manager Console set Default project to Zygieldesk.Persistance
 6. In console use "add-migration MigrationName" command, next use "update-database" command
 7. Launch project
 8. Every function from master branch should be working with SwaggerUI or any other app eg. Postman