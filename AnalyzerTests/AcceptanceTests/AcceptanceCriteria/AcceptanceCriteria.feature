Feature: AcceptanceCriteria
	As an author
	I want to know the number of times each word appears in a sentence
	So that I can make sure I'm not repeating myself

@WordAggregation
Scenario: Aggregate words
	Given A sentence
	When the program is run
	Then I am returned a distinct list of words in the sentence and the number of times they have occurred
	| Key		| Value	|
	| this		| 2		|
	| is		| 2		|
    | a			| 1		|
    | statement	| 1		|
    | and		| 1		|
	| so		| 1		|


