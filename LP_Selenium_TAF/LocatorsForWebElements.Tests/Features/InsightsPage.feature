Feature: Insights page functionality

Scenario Outline: Article title matches the carousel title
	Given I navigate to the EPAM website
	When I open the Insights page
		And I open the article from the carousel after <numberOfSwipes> swipes
	Then The article title matches the carousel slide title

	Examples: 
	| numberOfSwipes |
	| 2              |
	| 3              |