require 'rest_client'

@url = "http://localhost:4230"

Given /^I send and accept JSON$/ do
  #header 'Accept', 'application/json'
  #header 'Content-Type', 'application/json'
end

When /^I send a POST request to "([^"]*)" with the following:$/ do |path, body|
  RestClient.post("http://localhost:4230" + path, body, :content_type => :json, :accept => :json) { | response, request, result |
      @last_response = response
    }
end

When /^I send a GET request to "([^"]*)"$/ do |path|
  RestClient.get("http://localhost:4230" + path, :accept => :json){ |response, request, result, &block|
    @last_response = response
  }
end

When /^I get the created book$/ do
  location = @last_response.headers[:location]

  RestClient.get(location, :accept => :json){ |response, request, result, &block|
    @last_response = response
  }

  @book = @last_response.body
  #puts @book
end

Then /^the response status should be "([^"]*)"$/ do |status|
  begin
    @last_response.code.should eq(status.to_i)
  rescue RSpec::Expectations::ExpectationNotMetError => e
    puts "Response body:"
    puts last_response.body
    raise e
  end
end

Then /^the response should contain the regex "([^"]*)"$/ do |location_url|
  location = @last_response.headers[:location]
  location.should match(/#{location_url}/)
end