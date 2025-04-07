# WhaleSpotting API
The WhaleSpotting API is a controller-based ASP.NET api.
# Getting Started
First, navigate to the ./api directory in the project repo.
## Installing dependencies
To install dependencies, run:
```
$ dotnet restore
```
## Setting up the database
Please refer to "Setting up spatial data in the database" below before setting database 
First, create a new role in pgAdmin called `whale_spotting` with password `whale_spotting` and the following priveleges:
- Can login
- Create databases
- Inherit rights from the parent roles


## Setting up spatial data in the database 
Prior to setting up database, install spatial data extension "PostGIS" for PostGres via stack builder application
Open pgAdmin --> Under databases , you can find the `whale_spotting` database. Right click on it and choose Query Tool. In the tab that appears, run the below queries to enable spatial data :

Create Extension postgis;
Create Extension postgis_topology;

Then run:
```
$ dotnet ef database update
```

You can check that this has worked by right clicking on 'Databases' in pgAdmin and then clicking 'refresh'.

## Default Admin User
Currently, one default admin user is set up with username = whale_spotting and password  “Whale_spotting1”. 

# Running the code
You can run the code using `dotnet run`.

You can run the linter using `dotnet format WhaleSpottingBackend.sln --verbosity diagnostic`.

You can ensure it makes no changes using `dotnet format WhaleSpottingBackend.sln --verify-no-changes --verbosity diagnostic`.

# CI Pipeline
CI pipeline will be automatically triggered on push and pull request. Please check the CI Pipeline [Actions](https://github.com/techswitch-learners/WhaleSpottingMarch2025/actions) in GitHub to see if your commit passed. Diagnostic informaton will have more details on the where it failed.

## Linting/format rules
This pipeline will fail if you don't follow the code style rules:
### Indentation and spacing
- tab width should be equal to 4 spaces
- all curly brackets (braces) should start and end on a new line
- delete any unnecessary trailing white spaces or empty lines at the end of your file
- mark your field declarations correctly, for instance `readonly` for readonly properties. See [Add readonly modifier](https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/style-rules/ide0044)
- list your system directive imports first. For instance list `using System.IO` before `using Microsoft.AspNetCore.Mvc`. See [dotnet formatting options](https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/style-rules/dotnet-formatting-options)
