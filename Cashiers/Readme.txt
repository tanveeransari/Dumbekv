Projects in Solution
=====================
Cashiers -		Interface to command line input and output for CashiersLib
CashiersLib -	Has all the code except an entry point.
CashierTests -	Unit tests for CashiersLib 

Assumptions
==================
Desirable attributes are readability/maintainability, object oriented design and testability. 
I would have programmed this quite differently if performance was important.

Thread-safety is assumed not to be required. 
No data or program synchronization has been done nor have atomic operations or locks been used. 
These would be required in multiple places in the code if thread-safety was a requirement.

Style : naming class members with an underscore prefix is assumed okay, instead of using "this."
