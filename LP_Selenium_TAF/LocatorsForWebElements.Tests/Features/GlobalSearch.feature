Feature: Website global search functionality

Scenario Outline: Global search by keyword
	Given I navigate to the EPAM website
	When I perform global search for '<keyword>'
	Then All the search results contain '<keyword>'

	Examples: 
	| keyword    |
	| BLOCKCHAIN |
	| Cloud      |
	| Automation |