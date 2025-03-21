# NLTDSimpleInventory

OJT Inventory Exercise 2025

Inventory
 - Create a Inventory app that can track the items being bowrrowed and the borrower of the item.
 - Follow the below database structure

User story:
 - The application should have the ability to List the items in the inventory and have the ability add or remove items
 - Each item should be listed with Name and Detail of the item
 - Anyone can add a item on the library
 - when someone borrows a item, the borrowers detail should be recorded as well as the item borrowed
 - The system should have the ability to select the previous borrowers so that the borrower will not always enter the details
 - There should be a page to show borrowed item and its borrower
 - There should be a page to show the borrowers with and a history of the item borrowed

<br>
Database

Inventory
 <div>Item</div>
  <div>- Id</div>
  <div>- Name</div>
  <div>- ItemSKU</div>
 <div>Borrower</div>
  <div>- Id</div>
  <div>- Name</div>
  <div>- Address</div>
 <div>BorrowedItem</div>
  <div>- Id</div>
  <div>- ItemId</div>
  <div>- BorrowerId</div>
  <div>- DateBorrowed</div>
  <div>- DateReturned</div>
