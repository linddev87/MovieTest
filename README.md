Backlog:
- Add unit tests:
	- Application.CsvImportService: Insert new entities and handle exceptions
	- Application.MovieService: Test caching
	- Api.Endpoints: Test return types on success and on exception

- Integration with existing system:
	- Does the client interact directly with this API or is there a gateway inbetween?
	- Which authorization and authentication do we want?
	- How and where is the relation between user and movie defined?

- Find a better way to import movies in a production scenario
	- Is once per year really often enough? 
	- Is there a better data source than a CSV file from an unknown source? (IMDb/TMDb come to mind)

- Consider enriching the Movie entity
	- Should we add reviews, reviewers, list of actors, instructors etc?
	- Can other data sources provide this information? (Again IMDb/TMDb come to mind)
