# CDK C# Candidate Exercise

This code sample is designed as a basic test for determining proficiency
with C#, Entity Framework, Web API, and SQL Server. You will have 45 minutes
to complete this Exercise. During this time, you'll have access to a team
member that can answer questions for you, emulating the role of a product
manager, as well as access to the internet if you need to look anything up.

## Required Software

The computer you're using should have all of the necessary software installed
to complete this Exercise. The required software includes the following:

- Microsoft Visual Studio (2015 or greater)
- Microsoft SQL Server Management Studio (2008 or greater)
- Git for Windows
- Postman Client for Windows

## Setup

- Pull the source code down to the local machine
- Execute the DatabaseSetupScript.sql file in SSMS
- Open WebApiHiringExercise in Visual Studio
- Restore required NuGet packages

## Solution Structure

The sample solution contains the following 2 projects:

- WebApiHiringExercise - This is the Web API project itself
- WebApiHiringExercise.Tests - Unit tests for exorcising the Web API code

The Web API project is an ASP.NET Web API project that utilizing Entity
Framework for database access, and MS Test for unit testing coverage.
It also uses the SimpleInjector library to provide a Dependency Injection
container (for more info, you can look [here](https://simpleinjector.org/index.html)).

The Web API project has a single controller, called `RepairOrderController`, which
is where you'll ultimately start working as you progress through the Exercise.

## Tasks

Think of the following items as the stories/bugs in an Agile story system such as TFS
or Jira.  You should work on the stories one at a time, be willing to ask questions of
the "product manager" you're working with, and be thinking about clean coding practices (such
as SOLID and DRY) as you work.

Please also remember that there is a unit test project that will need to be executed after each
change to make sure that nothing has broken.

#### Bugs

- **GET /api/repairorders** - Labor and Total Costs are not being calculated correctly
  - Run the **GET /api/repairorders** endpoint
  - Look at RepairOrderId = 3
  - LaborCost is $100, and TotalCost is $270. This SHOULD be $75 and $245, respectively.
    - Look at the service lines and op codes for this repair order
#### Changes

- Create a new RESTful endpoint for getting a repair order by its VIN.
  - If there are multiple repair orders for the VIN, the API should return the the closest upcoming repair order.
- Create a SQL Server stored procedure for getting repair order status totals (for reporting purposes)
  - For each repair order status, the stored procedure should calculate the total labor, parts, and miscellaneous costs, as well as the total costs, of all repairs in those status'. So, there should be 5 columns returned:
    - RepairOrderStatus
    - TotalLaborCost
    - TotalPartsCost
    - TotalMiscellaneousCost
    - TotalCost
  - Also, the result should be sorted by RepairOrderStatusId