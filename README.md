# Sillystringz Factory

By: Stella Marie

## Technologies Used

- C# 12
- .Net 6.0
  - Entity Framework Core
  - MySql (Workbench)

## Description

Sillystringz Factory is an MVC app for managing engineers and machines, where an engineer can be licensed to repair many machines and a machine can have many engineers licensed to repair it.

**Requirements**
- Splash screen: list of all engineers and all machines
- Select engineer, see details and list of machines licensed to repair
- Select machine, see details and list of engineers licensed to repair
- Add new engineer (even if no machines)
- Add new machine (even if no engineers)
- Form validation: Do not add if fields are empty or contain invalid values
- Add or remove between engineers and machines
  - cannot add machine if no machines
  - cannot add engineers if no engineers

**Additional requirements**
- Implement CRUD for at least one entity
- View both sides of many-to-many

## Complete Setup

This app requires use of a database.

### Database Schemas

To setup the database, you can either follow the directions in [Database](#database) or first follow the instructions in [Connecting the Database](#connecting-the-database) and then implementing migrations as detailed in [Migrations](#migrations).

### Database

To setup your own database, you may need to download MySQL Server and MySQL Workbench. Using Workbench is not completely necessary, being a GUI (and it is slower than using CLI), but if you are starting out with databases, Workbench may make it easier. Note that if you use CLI, your tables and column names will need to be surrounded by backticks \`\` as in: \`restaurant_list\`.\'cuisines\'. The query writer in Workbench would not require use of the backticks, which would be added for you when it generates a SQL script.

1. Setup a database
2. Select the database
3. Add three tables:

```sql
CREATE DATABASE factory;
USE factory;
CREATE TABLE Engineers (
    EngineerId INT PRIMARY KEY NOT NULL AUTO_INCREMENT, 
    Name VARCHAR(255)
);
CREATE TABLE Machines (
    MachineId INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
    Name VARCHAR(255)
    Type VARCHAR(255)
);
CREATE TABLE EngineerMachines (
    EngineerMachineId INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
    MachineId INT,
    EngineerId INT
)
```

Note: Two formats for setting up a table and designating its primary key have been listed here. They work the same.

```sql
CREATE TABLE tablename (id INT PRIMARY KEY NOT NULL AUTO_INCREMENT);

CREATE TABLE tablename (id INT NOT NULL AUTO_INCREMENT, PRIMARY KEY (id))
```

### Connecting the Database

In your IDE:
- Create a file in the Factory assembly: appsettings.json
  - Do not remove the mention of this file from .gitignore
- Add this code:

```json
{
    "ConnectionStrings": {
        "DefaultConnection": "Server=[hostname];Port=[port_number];database=[database_name];uid=[username];pwd=[password]"
    }
}
```

Replace the values accordingly and brackets are not to be included.

### Migrations

- In the terminal, run ```dotnet build --project Factory```
  - Or navigate into the Factory subdirectory of the project and run ```dotnet build```

This command will install the necessary dependencies. For it to work though, appsettings.json should already be setup. As migrations are included in the clone or fork, you should only need to run:

```dotnet ef database update```

However, since the models are already set up, if update does not work, then do:

```bash
dotnet ef migrations add Initial
dotnet ef database update
```

### Run the App

Once you have a database setup and the connection string included in the appsettings.json, you can run the app:

- Navigate to main page of repo
- Either fork or clone project to local directory
- Bash or Terminal: ```dotnet run --project Factory```
  - If you navigate into the main assembly: Factory, use ```dotnet run```

If the app does not launch in the browser:
- Run the app
- Open a browser
- Enter the url: https://localhost:5001/

## Known Bugs

Please report any issues in using the app.

## License

[MIT](https://choosealicense.com/licenses/mit/)

Copyright (c) 2023 Sm Kou