Feature:  Create a Book
  As an API client
  In order to add a person to the address book
  I can create a card

  Background:
    Given I send and accept JSON

  Scenario: Creating a new book
    When I send a POST request to "/api/books" with the following:
    """
      {
        "Title":"Title One",
        "Author":"Author One",
		"Isbn":"A1234"
      }
    """
    Then the response status should be "201"
    And the response should contain the regex "\/api\/books\/\d{1,}"
    When I get the created book
    Then the response status should be "200"

  Scenario: Sending no data when creating a new book
    When I send a POST request to "/api/books" with the following:
    """
    """
    Then the response status should be "400"

  Scenario: getting a list of books
    When I send a GET request to "/api/books"
    Then the response status should be "200"

  Scenario: getting a non-existant book
    When I send a GET request to "/api/books/9999"
    When the response status should be "404"
	
  Scenario: attempting to create a new book with invalid isbn
	When I send a POST request to "/api/books" with the following:
	"""
	{
	"Title":"Title",
	"Author":"Author",
	"Isbn":"12"
	}
	"""
	Then the response status should be "417"
	
  Scenario: attempting to create a new book with isbn empty
	When I send a POST request to "/api/books" with the following:
	"""
	{
	"Title":"Title",
	"Author":"Author",
	"Isbn":""
	}
	"""
	Then the response status should be "417"
	
  Scenario: Duplicate ISBN is not allowed
	When I send a POST request to "/api/books" with the following:
	"""
	{
	"Title":"Title",
	"Author":"Author",
	"Isbn":"A1234"
	}
	"""
	When I send a POST request to "/api/books" with the following:
	"""
	{
	"Title":"Title",
	"Author":"Author",
	"Isbn":"A1234"
	}
	"""
	Then the response status should be "417"