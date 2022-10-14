﻿# Work in progress
 
 Zygieldesk - zendesk like customer service app. Clean architecture + CQRS + MediatR

 # Demo:
 SwaggerUI Demo avaiable: http://zygieldeskapi.tk
 Admin account:

 ``login: admin@admin.com``

 ``password : admin``

 demo last update: 14.10.2022

 # How to launch on local machine:
 PRE REQUISITES : .NET 6 FRAMEWORK + IDE (tested on Visual Studio)

 1. Clone repository (master branch).
 2. Set Zygieldesk.API as a Startup Project.
 3. Provide valid Connection string in appsettings.json "ZygieldeskConnectionString" section.
 4. Open NuGet Package Manager Console
 5. In Package Manager Console set Default project to Zygieldesk.Persistance
 6. In console use "add-migration MigrationName" command, next use "update-database" command
 7. Launch project
 8. Every function from master branch should be working with SwaggerUI or any other app eg. Postman
 9. To be authorized for every single function, make sure you are logged in to admin account:

 ``login: admin@admin.com``

 ``password : admin``

 You can do so by using login method on SwaggerUI/any other app like Postman.
 Than with SwaggerUI paste jwtToken to Value field in Authorize function located on top right.
 With apps like Postman, add Authorize header with "Bearer jwtToken".