Feature: Website careers search functionality

Scenario Outline: Search remote position by programming language and country
	Given I navigate to the EPAM careers search page
	When I search for remote positions with '<programmingLanguage>' in '<country>'
		And I open the first found position
	Then The position page title contains '<programmingLanguage>'

	Examples: 
	| programmingLanguage | country  |
	| .NET                | Georgia  |
	| Python              | Portugal |
	| Java                | Ukraine  |