# Urban-Farm-SQL
Urban Farm is a desktop application developed in C# using WPF (.NET 8) designed to help manage and track urban farming operations. The app provides an intuitive interface for monitoring monthly production, plant growth, harvested volume, and resource usage.

✨ Features
📅 Monthly production tracking (select a month and input relevant data)
🌿 Records for:

Quantity produced

Plant growth

Harvested volume

Resource usage
📊 Visual summary of production data
💾 Data persistence using JSON and SQL Server
🌗 Light and Dark Mode themes
🎨 UI design inspired by Figma mockups
🔒 Simple login system (optional Google login available)

🛠️ Technologies Used

C# with WPF (.NET 8)

XAML for UI design

Local data storage via JSON

Microsoft SQL Server for structured and centralized data management

📁 File & Data Storage
Urban-Farm adopts a hybrid data persistence approach:

JSON Files: Used for quick access to local user preferences, UI state, and offline-first operations. Ensures lightweight persistence without needing a database connection.

SQL Server: Used to store structured data such as user accounts, production records, monthly statistics, and historical logs. This provides a more scalable and secure way to manage data, especially in multi-user or networked environments.

Users can continue using the app without internet access, relying on local JSON files. When connected to the SQL Server, data is synchronized or retrieved as needed, enabling powerful querying, reporting, and future expansion.

🌤️ This app uses the Open-Meteo API, which is free and requires no authentication key.
