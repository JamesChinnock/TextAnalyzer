Feature: TextFileAcceptanceCriteria
	As an author
	I want to know the number of times each word appears in a sentence
	So that I can make sure I'm not repeating myself

@WordAggregation
Scenario: Aggregate words from file
	Given A text file
	When the program is run against the contents of the file
	Then the correct list of distinct words in the sentence, and the number of times they occurred is returned
	| Key	| Value |
	| the	| 64924 |
	| and	| 52164 |
    | of	| 35310 |
    | to	| 14046 |
    | that	| 13223 |
	| in    | 12891 |
