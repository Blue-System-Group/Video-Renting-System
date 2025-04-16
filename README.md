# Video Rental System - Console App

This is a **Video Rental System** built as a **C# Console Application**.  
The project demonstrates the use of **Data Structures** like Lists, Hash Tables, and Binary Search Trees, and includes a **database integration** for persistent data management.

The solution also includes a **unit testing project** using **MSTest**, covering the main data structures used in the application.

---

## 🧩 Project Structure

### Main Project: `VideoRentalSystem`

- **Language**: C#
- **Framework**: .NET (Console Application)
- **Database**: Integrated for storing customer, user, rental, and video data
- **Data Structures Used**:
  - `CustomerList` - Custom list implementation for managing customers
  - `UserList` - Manages users of the system
  - `RentalHashtable` - Hash table implementation for tracking rentals
  - `VideoBST` - Binary Search Tree for efficient video catalog management

---

### Test Project: `VideoRentalSystem.Tests` (MSTest)

Unit tests included for the core data structures:

- ✅ `CustomerListTest`
- ✅ `UserListTest`
- ✅ `RentalHashtableTest`
- ✅ `VideoBSTTest`

---

## 🚀 How to Run

1. **Clone the repository**

   ```bash
   git clone https://github.com/Blue-System-Group/Video-Renting-System.git
   cd Video-Renting-System
### Need SQL database setup Before run
### ▶️ Run with Visual Studio

1. Open the `.sln` file in Visual Studio.
2. Set `VideoRentalSystem` as the **Startup Project**.
3. Press **F5** or click **Start** to run the console app.
4. To run tests:
   - Open **Test Explorer**
   - Click **Run All Tests**
