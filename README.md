# NLTDSimpleInventory

# LibraryExercise2025
OJT Library Exercise 2025

Library
 - Create a Library app that can track the books being bowrrowed and the borrower of the book.
 - Follow the below database structure

User story:
 - The application should have the ability to List the books in the library and have the ability add or remove books
 - Each book should be listed with Name and Author of the books
 - Anyone can add a book on the library
 - when someone borrows a book, the borrowers detail should be recorded as well as the book borrowed
 - The system should have the ability to select the previous borrowers so that the borrower will not always enter the details
 - There should be a page to show borrowed books and its borrower
 - There should be a page to show the borrowers with and a history of the books borrowed

<br>
Database

Library
 <div>Book</div>
  <div>- Id</div>
  <div>- Name</div>
  <div>- Author</div>
 <div>Borrower</div>
  <div>- Id</div>
  <div>- Name</div>
  <div>- Address</div>
 <div>BorrowedBook</div>
  <div>- Id</div>
  <div>- BookId</div>
  <div>- BorrowerId</div>
  <div>- DateBorrowed</div>
  <div>- DateReturned</div>
