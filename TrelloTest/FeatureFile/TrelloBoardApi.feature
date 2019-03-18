Feature: TrelloBoardApi crud
//Perform CRUD actions on the public Trello board api. 
//In this case we create a board, close (update) a board, get a single and get all boards en delete a board.


Scenario: Create Trello Board with only the required fields
	Given board name is KnabHomeAssignment
	When I create a new Board
	Then the statuscode should be 200
	And the name should be KnabHomeAssignment


Scenario: Create Trello Board with incorrect key
	Given key is incorrect
	When I create a new Board
	Then the statuscode should be 401
	And the message should be invalid key


Scenario: Update Trello Board by closing it
	Given a new board
	When I close the trello board
	Then the statuscode should be 200
	And Closed should be true


Scenario: Update Trello Board with unknown id
	Given boardId is 666
	When I close the trello board
	Then the statuscode should be 400


Scenario: Get Trelloboard
	Given a new Trello board with name KnabBoard
	When I get the Trello board
	Then the statuscode should be 200
	And the name should be KnabBoard


Scenario: Get all Trello boards 
	Given a new Trello board with name KnabBoard
	And a closed Trello board with the name ClosedBoard
	When I get all the Trello boards
	Then the statuscode should be 200
	And 2 boards should be returned


Scenario: Get all Trello boards while there are none
	When I get all the Trello boards
	Then the statuscode should be 200
	And the response should be an empty array


Scenario: Delete a closed board
	Given a new board
	When I close the trello board
	And I delete the trello board
	Then the statuscode should be 200


Scenario: Delete an active board
	Given a new board
	When I delete the trello board
	Then the statuscode should be 200


Scenario: Delete a Trello Board with an incorrect id
	Given boardId is 666
	When I delete the trello board
	Then the statuscode should be 400


