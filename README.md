# Video Rental System - Console Application

This is a **Video Rental System** developed as a **C# Console Application**.  
The project showcases the use of custom **data structures** such as Lists, Hash Tables, and Binary Search Trees, with integrated **database support** for data persistence.

A separate **unit testing project** is included using **MSTest**, focusing on validating the behavior of the main data structures.

---

## Project Structure

### Main Project: `VideoRentalSystem`

- **Language**: C#
- **Framework**: .NET (Console App)
- **Database**: Used to store customer, user, rental, and video data
- **Core Data Structures**:
  - `CustomerList`: Custom list implementation for managing customers
  - `UserList`: Handles system users
  - `RentalHashtable`: Hash table for tracking rentals
  - `VideoBST`: Binary Search Tree for efficient video catalog operations

---

### Test Project: `VideoRentalSystem.Tests`

Unit tests cover the main custom data structures:

- `CustomerListTest`
- `UserListTest`
- `RentalHashtableTest`
- `VideoBSTTest`

---

## Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/Blue-System-Group/Video-Renting-System.git
cd Video-Renting-System
