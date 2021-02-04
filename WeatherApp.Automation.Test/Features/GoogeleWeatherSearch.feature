Feature: GoogeleWeatherSearch

Scenario Outline: As a User, I should verify the weather in google search for a particular locations.
	Given city is <city>
	And state is <state>
	And unit is <unit>
	When Search google with city, state and unit
	Then Teamperature displayed should match value from open weather api
	Examples:
	| city       | state                | unit     |
	| 'Kochi'    | 'Kerala'             | 'metric' |
	| 'Thiruvananthapuram'   | 'Kerala'   | 'metric' |
	| 'Manali'   | 'Himachal Pradesh'   | 'metric' |