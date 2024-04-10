# CaseBankdata
My submission for the code-case includes a .net application serving three operations CREATE, LIST and TRANSFER.

## Prerequisites
* The application was developed in Visual Studio 17.9.3, with the latest .NET SDK
* I use dotnet secrets to manage my development defaultConnection string with 'dotnet user-secrets set "ConnectionStrings:DefaultConnection" "your_string"', if you want to test on a local db
* For your local db, migrate with 'dotnet ef database update'
* I use docker desktop to run the container
* The application will be hosted at 89.150.141.148:5000 if you prefer to use postman or alike. (although I am setting it up as we speak, so it might take a few minutes)


# Solution
## Architechture
* I opted for a layered architechture with Presentation, Service and Data layers
* Each controller has an associated Service responsible for performing business logic operations. These are injected when needed
## Considerations: Security, Error handling, Auditing and Observability
* I included some general examples of handling errors - including exception handling and deviations from business rules (insufficient funds etc)
* I included the out-of-the box ILogger in ASP.Net core, to demonstrate how I usually approach logging
* I did not include authentication or alike

# Testing
* I did not have sufficient time to implement tests. Whilst I typically write tests beforehand, for this assignment I opted to finish the solution itself first.
* I typically write unit and integration tests, but I do not have much experience testing beyond that.

# API
## Features:
* ## CREATE:
* Creates an account in the db when given a customer ID and an initial deposit of at least 500. Returns an accountDto with accountId, AccountNumber and balance. 
* AccountsController.cs: Performs high level validation to ensure the data is formatted correctly and it meets the business rules
* IAccountService: performs the operation, where an Account entity is created and inserted using EF core and the dbcontext. It returns a new accountDto to the controller.

* ## LIST:
* Returns a list of all associated accounts to the input customer's id 
* AccountsController.cs: Performs high level validation to ensure the data is formatted correctly and it meets the business rules
* IAccountService: performs the operation, where the account entities are retrieved and mapped to accountDto's before returning them to the caller

* ## TRANSFER:
* Transfers an amount (above 0) between two input accounts.
* TransferController: Performs high level validation to ensure the data is formatted correctly and it meets the business rules
* ITransferService: Peforms the neccessary checks to ensure the operation meets the business rules (funds, do both accounts exist) before attempting the transfer. It records the transaction twice, one for the reciever and one for the sender.
* Account-entity contains domain-specific operations (withdraw, deposit) that are called before they are inserted in the database

# Feedback
* This is my first code-case outside of my studies and I found it challenging to approach. Please include any critical feedback that might seem redundant or benign, as believe it can help me improve.
* I understand you do not have time to thorougly review and assess all applicants.
  


