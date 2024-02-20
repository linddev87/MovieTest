# About
This project is my submission for a test I was given. The solution is not production ready, but you are welcome to find inspiration/clone whatever you can use. 

## Task
A new feature is to be added to an existing system with many users:
- Allow users to choose their favorite movie from a list of movies
- The user should be able to filter the list by title and release year
- A list of movies to be included in the list is provided in a CSV file once every year. A sample CSV file has been provided.

## Solution Structure

The solution structure is inspired by Clean Architecture with the following layers:
- Domain layer at the center defining the interfaces and core datamodel.
- Application layer containing services that depend on the domain layer. This layer is where repositories and external services are connected. 
- Infrastructure layer which implements the persistence-related interfaces defined in the application layer including the EntityFramework DbContext
- Outer 'Driver' layer containing the following:
	- A WebApi used for querying the movie database
	- A Sqlite3 database currently stored on the local machine
	- A Console App for running the import service

## Setup on your local machine 
The the web api and the console app can be run locally after following these steps:
- Clone the repository to your local machine.
- Create new appsettings.json files in the ConsoleApp and Api projects respectively using the appsettings-template.json files as your base. 
- IMPORTANT: Replace the [PATH-TO-REPOSITORY] placeholders with the path to the repository on your local machine.

A sqlite3 database is included in the /Database folder so the API should be ready to go!

## Backlog

Priority:
- Integration with existing system:
	- Does the client interact directly with this API or is there a gateway inbetween?
	- Which authorization and authentication do we want?
	- How and where is the relation between user and movie defined?
	- Which database provider should be used in production?

- Add unit tests:
	- Application.CsvImportService: Insert new entities and handle exceptions
	- Application.MovieService: Test caching
	- Api.Endpoints: Test return types on success and on exception
<br/>

Nice-To-Have:
- Find a better way to import movies in a production scenario
	- Is once per year really often enough? 
	- Is there a better data source than a CSV file from an unknown source? (IMDb/TMDb come to mind)

- Consider enriching the Movie entity
	- Should we add reviews, reviewers, list of actors, instructors etc?
	- Can other data sources provide this information? (Again IMDb/TMDb come to mind)
