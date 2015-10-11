Projects in Solution
=====================
Cashiers -		interface to command line input and output. Runs the logic using CashiersLib
CashiersLib -	this is where all the heavy lifting is done. Contains all the business logic. 
CashierTests -	unit tests for CashiersLib 

Assumptions
==================
Desirable attributes are readability/maintainability, object oriented design and testability. 
I would have done this differently if performance was one of the objectives.

Thread-safety is assumed not to be required since all my file reading is not multithreaded.
So synchronization, volatile, atomic operations which would have been used in a multithreaded scenario have 
not been used delibarately. 

Coding Style : naming class members with an underscore prefix is assumed okay, instead of using "this."
