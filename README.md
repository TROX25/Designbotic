# Element properties reader

**Element properties reader** is a custom Revit plugin designed to streamline the process of inspecting model elements by extracting essential metadata such as ID, name, category, and material. The plugin provides a user-friendly ribbon interface with intuitive functions and exports data in both visual and structured JSON formats.

## 🚀 Features

- 🧩 Ribbon panel with clearly labeled buttons
- 🖱️ One-click selection of multiple Revit elements
- 📋 Displays element ID, name, category, and material in a pop-up window
- 📦 Option to export data to a JSON file
- 🔁 Lightweight and efficient — built directly with the Revit API (no Dynamo)

## 📸 Screenshots

### 1. Ribbon Interface

The plugin adds a custom tab and ribbon panel with two main function buttons:

![fot1](https://github.com/user-attachments/assets/9f805a0e-a5ff-45e3-b8a9-11c5c1c534bb)

---

### 2. Selecting Elements

Use the first button to manually select elements in the Revit model:

![foto2](https://github.com/user-attachments/assets/1cda3ffd-d435-4fc7-9db1-2b9ecc44d35f)

---

### 3. Output Dialog

After selection, a dialog shows key properties (ID, Name, Category, Material):

![fot3](https://github.com/user-attachments/assets/e000b7d1-d5f8-4101-9276-ac00a52a6102)

---

### 4. JSON Output

The same data can be exported to a JSON file for further analysis:

![fot4](https://github.com/user-attachments/assets/0a99deff-c9ec-40a4-b0ff-1c5b37d48b38)

---

## 🛠️ Technologies Used

- C#
- .NET Framework
- Revit API
- WPF for UI dialogs
- JSON.NET for data serialization

## 📂 Repository Structure

Element_reader/
├── Commands/ # External command classes
├── UI/ # Windows and dialogs
├── Resources/ # Icons and XAML resources
├── Utils/ # Helper methods and JSON handlers
├── App.cs # IExternalApplication setup
├── Manifest files # For Revit integration

## ❗ Issues (to be resolved)

- Incomplete material recognition for objects with multiple material layers.
- Plugin currently handles only manually selected elements — needs option for category-based automatic selection.

---

## 📌 Future Improvements

- Add automatic element selection by category or level
- Enable material property editing directly from the UI
- Export CSV alongside JSON
- Better handling for Revit materials in complex elements (e.g. walls with compound layers)

---
