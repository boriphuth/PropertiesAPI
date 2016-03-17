Feature: Properties

@mytag
Scenario Outline: Add property
	Given I create a new property (<Address>,<Price>,<Name>,<PropertyDescription>,<Id>)
    And ModelState is correct
	Then the system should return <StatusCode>

Examples: 
	| Address             | Price   | Name        | PropertyDescription | StatusCode | Id  |
	| London, Essex       | 250.000 | Court House | New Development     | 201        |  1  |
	| Essex, Romford      | 200.000 | New House   | House               | 201        |  2  |
	| London, Soho        | 300.000 | Court House | New Development     | 201        |  3  |
	| London, Wanstead    | 350.000 | New House   | House               | 201        |  4  |
	| London, Stratford   | 370.000 | Court House | New Development     | 201        |  5  |
	| Essex, Liverpool St | 500.000 | New House   | House               | 201        |  6  |


Scenario Outline: Get Properties
	Given I request to view properties with pagination (<page>,<pageSize>,<address>,<priceMin>,<priceMax>,<Id>)
	Then system should return Pagination Headers

Examples: 
	| page | pageSize | address | priceMin | priceMax | Id |
	| 1    | 3        |   None  | 250.000  | 370.000  | 0  |

Scenario Outline: Delete Properties
	Given I create a new property (<address>,<Price>,<Name>,<PropertyDescription>,<Id>)
    When I delete a property
	And I request to view properties with pagination (<page>,<pageSize>,<address>,<priceMin>,<priceMax>,<Id>)
	Then the system should not return any results 

Examples: 
	| page | pageSize | address       | Price   | Name        | PropertyDescription | priceMin | priceMax | Id |
	| 1    | 1        | London  Essex | 250.000 | Court House | New Development     | 250.000  | 370.000  | 8  |

Scenario Outline: Update Properties
	Given I create a new property (<Address>,<Price>,<Name>,<PropertyDescription>,<Id>)
	When I update an existing property (<newAddress>,<newPrice>)
	And I request to view properties with pagination (<page>,<pageSize>,<address>,<priceMin>,<priceMax>,<Id>)
	Then the updated property should be included in the list

Examples: 
	| Address             | Price   | Name      | PropertyDescription |	newAddress			|	newPrice	|  Id |
	| Essex, Liverpool St | 500.000 | New House | House               |	London Liverpool St	|	550.000		|  1  |

Scenario Outline: ETag Flow
	Given I create a new property (<Address>,<Price>,<Name>,<PropertyDescription>,<Id>)
	When I request to view properties with pagination (<page>,<pageSize>,<address>,<priceMin>,<priceMax>,<Id>)
	Then the server should assign an Etag to the response
	And the system should return <SuccessStatusCode>
	When I request to view properties
	Then the system should return <NotModifedStatusCode>

Examples: 
	| Address             | Price   | Name      | PropertyDescription | Id | nMin | nMax | SuccessStatusCode | NotModifedStatusCode |
	| Essex  Liverpool St | 500.000 | New House | House               | 1  | 1    | 11   | 200               | 304                  |
