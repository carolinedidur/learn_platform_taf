Feature: Website footer functionality

Scenario Outline: Download file from footer 
	Given I navigate to the EPAM website
	When I download '<itemTitle>' from the footer
	Then File '<fileName>' is downloaded successfully

	Examples: 
	| itemTitle               | fileName                    |
	| Code of Ethical Conduct | Code_of_Ethical_Conduct.pdf |