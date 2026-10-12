Feature: Website navigation functionality

Scenario Outline: Navigate to Services Section
	Given I navigate to the EPAM website
	When I hover over the "Services" link in the main navigation menu
		And I select service category '<category>' from the dropdown
	Then The page contains '<category>' title
		And The section ‘Our Related Expertise’ is displayed on the page

	Examples: 
	| category       |
	| Generative AI  |
	| Responsible AI |