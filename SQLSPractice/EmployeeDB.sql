create DATABASE EmployeeDB;

GO
use EmployeeDB;
GO

CREATE TABLE Departments(
    Id INT PRIMARY KEY,
    deptName NVARCHAR(50)
)

create TABLE Employee(
    Id int PRIMARY KEY,
    FirstName VARCHAR(50),
    LastName VARCHAR(50),
    DOB DATE,
    Salary int,
    deptID int FOREIGN KEY REFERENCES Departments(Id),
    isActive BIT
);

INSERT INTO Departments (Id, deptName) VALUES
(101, 'Engineering'),
(102, 'Human Resources'),
(103, 'Sales'),
(104, 'Marketing'),
(105, 'Finance');
INSERT INTO Departments (Id, deptName) VALUES (106, 'Legal');

INSERT INTO Employee (Id, FirstName, LastName, DOB, Salary, deptID, isActive) VALUES
(351261, 'Shreya', 'Sharan', '2002-09-13', 66000, 101, 1),
(351262, 'Divyansh', 'Goyal', '2004-04-22', 60000, 102, 1),
(351263, 'Aarav', 'Mehta', '1998-01-15', 72000, 101, 1),
(351264, 'Priya', 'Nair', '1995-11-30', 85000, 105, 1),
(351265, 'Rohan', 'Kapoor', '2000-07-08', 58000, 103, 0),
(351266, 'Sneha', 'Iyer', '1997-03-25', 91000, 104, 1),
(351267, 'Karan', 'Malhotra', '1999-12-02', 64000, 101, 1),
(351268, 'Ananya', 'Reddy', '2001-06-19', 55000, 102, 0),
(351269, 'Vikram', 'Singh', '1996-09-09', 78000, 105, 1),
(351270, 'Ishita', 'Bose', '2003-02-14', 52000, 103, 1),
(351271, 'Aditya', 'Verma', '1994-08-27', 95000, 101, 1),
(351272, 'Meera', 'Pillai', '2002-05-05', 61000, 104, 0),
(351273, 'Rahul', 'Chopra', '1998-10-11', 69000, 102, 1),
(351274, 'Tanvi', 'Joshi', '2000-04-17', 73000, 105, 1),
(351275, 'Yash', 'Agarwal', '1999-01-23', 60000, 103, 1);
INSERT INTO Employee (Id, FirstName, LastName, DOB, Salary, deptID, isActive) VALUES
(351276, 'Kabir', 'Sinha', '2001-11-11', 58000, NULL, 1);

-- 1. Find all employees with a salary above 70,000.
SELECT * 
FROM Employee
WHERE Salary>70000;

-- 2. List all active employees, sorted by salary in descending order.
SELECT *
FROM Employee
WHERE isActive=1
ORDER BY Salary DESC;

-- 3. Get each employee's name along with their department name (only employees who have a matching department).
SELECT e.FirstName, e.Lastname, d.deptName
FROM Employee e
INNER JOIN Departments d ON e.deptID=d.Id;

-- 4. List all departments along with their employeees - inlcude departments that have no employees
SELECT e.FirstName, e.LastName, d.deptName
FROM Employee e
RIGHT JOIN Departments d ON e.deptID=d.Id;

-- 5. List all employees along withe department names- inlcude employees whose department doesnt match any records.
SELECT e.FirstName, e.LastName, d.deptName
FROM Employee e
LEFT JOIN Departments d ON e.deptID=d.Id;

-- 6. List all departments and all employees together, matched where possible, including unmatched rows on both sides.
SELECT e.FirstName, e.LastName, d.deptName
FROM Employee e
FULL OUTER JOIN Departments d
    ON e.deptID=d.Id;

-- 7. Find the average salary per department.
SELECT
    d.deptName,
    AVG(e.Salary) AS Average_Salary
FROM Departments d
LEFT JOIN Employee e
    ON e.deptID = d.Id
GROUP BY d.deptName;

-- 8. Find departments that have more than 2 employees
SELECT d.deptName, COUNT(e.Id) as CountEmp
FROM Employee e
RIGHT JOIN Departments d ON e.deptID=d.Id
GROUP BY deptName
HAVING COUNT(e.Id)>2;

-- 9. Find the total salary paid out per department, showing department names.
SELECT d.deptName, SUM(e.Salary) as SumSalary
FROM Employee e
RIGHT JOIN Departments d ON e.deptID=d.Id
GROUP BY deptName;

-- 10. Pair every employee with every department 
SELECT e.FirstName,e.LastName,d.deptName
FROM Employee e
CROSS JOIN Departments d;

-- CREATING AN INDEX --
CREATE INDEX IX_Employee_DeptId
ON employee(deptId);

SELECT * FROM Employee WHERE deptID=101;

-- CREATING A VIEW -- (basically a saved sql query)
CREATE VIEW vw_EmployeeDetails AS
SELECT 
    e.id,
    e.FirstName,
    e.LastName,
    e.Salary,
    e.isActive,
    d.deptName
FROM Employee e
LEFT JOIN Departments d
    ON e.deptID=d.Id;

SELECT * FROM vw_EmployeeDetails;

-- CREATING STORED PROCEDURE -- (A saved database sction with input)
CREATE OR ALTER PROCEDURE sp_GetEmployeeByDepartment
    @DepartmentId INT
AS
BEGIN
   SELECT 
    e.id,
    e.FirstName,
    e.LastName,
    e.Salary,
    e.isActive,
    d.deptName
FROM Employee e
INNER JOIN Departments d
    ON e.deptID=d.Id
WHERE e.deptID=@DepartmentId;
END;

EXEC sp_GetEmployeeByDepartment @DepartmentId=101;