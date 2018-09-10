## Question 1

What the heck is this program doing?

### Answer

Program loops over 100 "post" objects and 10 "user" objects, which are downloaded from typicode.
All objects follow the same basic structure, which is used by the parsing algorithm.
For each post, the code processes the number of words in the "Body" string element using space separators.
Word counts are averaged for each users based on the number of posts associated with those users and individual user statistics are presented at the end.

## Question 2

What issues (if any?!?) do you see in that code? Order them from "most apocalyptic" to "little less insane".

### Answer

There are many issues with the code, too many to write a long paragraph. Hence, for the sake of clarity, the issues are listed below in sequence:

* Async http requests running on main thread.

* Downloaded Request Results are not checked for validity before being interpreted and exceptions are not expected/treated; e.g.: unavailable content will cause exceptions to be thrown.

* All code runs in main function.

* Process through which json is parsed/de-serialized is awful and even additional whitespace in the formatting will absolutely break the code. Changing double quotes to single quotes will break the code, pretty much anything will break the code formatting-wise.

* Code processes the post objects "body" element using the space (" ") character as a separator, meaning that other word separators such as line breaks ("\n") will not be counted properly.

* Code has many assumptions on the data formatting and content and will break, for example, if User ID is outside range (1-10), works "fine" given the API documentation, but is not really acceptable in the real world.

* Code mixes its loop/data structure indexing with user id indexes, which cause "-1" index changes to appear throughout the code when dealing with 0 indexed data structures like arrays.

* Variable naming is illegible and restricts readability/maintainability, short non-meaningful names, specially poor for string location indexes (j & k). The fact that a variable name "id" is used when it does not correspond to the JSON element "id" but to the "UserId" is also not great practice.

* The code doesn't actually save the statistics collected anywhere, but given the context its excusable.

## Question 3

Refactor the code to address most issues you identified in previous question (or, if there weren't any, you may just skip this question… and the next!)

The code uses a REST API with dummy data, located at http://jsonplaceholder.typicode.com/. You may want to have a look at that site to help you understand the API, as you may use any of its endpoints.
You can do anything with that code, as if it were a real project you were assigned to.
Hint: Keep question 4 in mind while working on this one.

### Answer

* See attached code *

## Question 4

Write just a few unit tests for your code, focusing on its essential aspects.

- You may use the *NUnit 3.9.0* NuGet package, already imported into the project, or any other framework.

### Answer

I implemented a very basic NUnit test suite for the programming challenge.
Following are notes on the test suite.

1) The code is extremely simple and the re-implementation solves the issues listed previously. That means that the resulting limitations are associated with how we interact with the JSON API. As we've only got 2 endpoints to test, there is not much to test in terms of that.

2) The NUnit installation in the project was outdated (current - 3.10.1) and imcomplete, missing the test adapter necessary for testing within VS2017. Having not worked with NUnit in the past I had to fix the installation and read its documentation. Due to the recommended time limitations, only basic tests were considered to be necessary/viable.

## Comments (optional)

Do you have any comments on this exam or anything else you would like to mention?

### Answer

The approach I chose for my implementation was not the most flexible one given the suggested time constraints (1h - 1h30m).
In the real world, it could have been interesting to increase the flexibility and robustness of the JSON processing portion of the code, checking for missing fields, additional type errors or other elements that might be affected by user input, however, given the description of the API this issue was not treated.
As is, the solution designed is meant to take advantage of existing frameworks, reducing the need for custom code when using well established structures like JSON objects.
Of course, if we were to focus on optimization and do some bit-scrubbing, then, it would be relevant to manipulate the JSON objects ourselves.
Also, using a dictionary (as I have) would not be a great approach if we had a very large user list each with very few posts.

## Thanks for participating! =)