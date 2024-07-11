# Car Auction System

This is the README file for the BCA's Coding Test.
This is a Car Auction System built in .Net 8.0, using EF Core and SQLServer.

Design decisions made:
I have structured the solution as to support an inventory of vehicles, and a further relation to auction,
giving me flexibility as to being able to register a new vehicle without necessarily associate it with an auction.

I have not set up any type of cryptography or authentication in order not to increase the complexity. As a Proof of Concept I didn't consider it necessary.

To ensure that vehicles don't have duplicate Unique Identifiers, I have set up this property at the base vehicle class, and as a unique index.
At the same time, i have added some logical validations when adding a vehicle to the database.

This repository also includes Unit Tests developed using NUnit and AutoFixture.AutoMoq.

=======================EXTRAS==================
- When Closing an auction, return the highest bidders for each Auctioned Vehicle, the bidder's User Id + Value of the Highest Bid + Auctioned Vehicle Information
