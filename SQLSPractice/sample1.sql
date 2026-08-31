CREATE DATABASE SQL

CREATE TABLE Employees(
    EmployeeID INT,
    Name VARCHAR(50),
    Age INT,
    Department VARCHAR(50),
    Salary INT
);

INSERT INTO Employees(EmployeeID,Name,Age,Department,Salary) VALUES
(351261,'Shreya',21,'Digital Engineering',66000),
(351262,'Divyansh',22,'Human Resource',35000),
(351263,'Harshit',23,'Marketing',29000),
(351267, 'Rahul', 25, 'IT', 50000),
(351278, 'Priya', 28, 'HR', 45000),
(335183, 'Aman', 24, 'IT', 55000),
(4131, 'Sneha', 30, 'Finance', 60000),
(351234, 'Rohan', 26, 'IT', 48000);

SELECT * FROM Employees

SELECT Name, Age 
FROM Employees

SELECT Name,Age 
FROM Employees
WHERE Age>23

