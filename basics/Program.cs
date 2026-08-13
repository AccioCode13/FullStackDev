/*
This project is specifically for learning the basics of C#.
Topics covered:
- Variables
- Data types
- If-else statements - if else if and else
- Loops - for, while, foreach
- Arrays
- Lists
- Hashsets
- Dictionaries
- Enums
- Switch statements
*/

/*
C# DATA TYPES

Value Types:
- int
- long
- float
- double
- decimal
- bool
- char
- struct
- enum

Reference Types:
- string
- array
- class
- object
- List<T>
- Dictionary<TKey, TValue>
- HashSet<T>
*/


using System.Data.Common;

string employeeName = "Shreya Sharan";
int employeeId = 351221;
decimal salary = 66000m;
bool isActive = true;
DateTime joiningDate = new DateTime(2026, 6, 15);
char bloodGroup='B';

string department = "Digital Engineering";

string[] skills = { "Python", "SQL", "C#", "React" };

List<string> projects = new List<string>
{
    "CWS",
    "Claims Dashboard"
};

Dictionary<int, string> employees = new Dictionary<int, string>
{
    { 351221, "Shreya" },
    { 351132, "Divyansh" }
};


// Print employee information

Console.WriteLine($"Employee Name: {employeeName}");
Console.WriteLine($"Employee ID: {employeeId}");
Console.WriteLine($"Employee Department: {department}");
Console.WriteLine($"Joining Date: {joiningDate.ToShortDateString()}");
Console.WriteLine($"Employee bloodGroup: {bloodGroup}");



// If-else

if (isActive)
{
    Console.WriteLine("The employee is Active");

    if (salary >= 80000)
    {
        Console.WriteLine("Senior Salary Band");
    }
    else
    {
        Console.WriteLine("Standard Salary Band");
    }
}
else
{
    Console.WriteLine("The employee is not active");
}


// Array + foreach

Console.WriteLine("\nSkills:");

foreach (string skill in skills)
{
    Console.WriteLine(skill);
}


// List + foreach

Console.WriteLine("\nProjects:");

foreach (string project in projects)
{
    Console.WriteLine($"Project assigned: {project}");
}


// For loop + if-else

Console.WriteLine("\nNumbers:");

for (int i = 0; i <= 10; i++)
{
    if (i % 2 == 0)
    {
        Console.WriteLine($"Even number = {i}");
    }
    else if (i % 3 == 0)
    {
        Console.WriteLine($"Divisible by 3 = {i}");
    }
    else
    {
        Console.WriteLine($"Odd number = {i}");
    }
}


// Dictionary + foreach

Console.WriteLine("\nEmployees:");

foreach (KeyValuePair<int, string> employee in employees)
{
    Console.WriteLine($"ID: {employee.Key}, Name: {employee.Value}");
}


// HashSets
HashSet<int> IDs=new HashSet<int>
{
    352123,
    123453,
    247823,
    351251,
    351251
};

foreach (var item in IDs)
{
 Console.WriteLine(item); 
}

// A small prog

Console.WriteLine("CHOICES ARE GIVEN BELOW");
Console.WriteLine(" 1. Digital Engineering");
Console.WriteLine(" 2. HR");
Console.WriteLine(" 3. Finance");
Console.WriteLine(" 4. Marketing");
Console.WriteLine("Enter your choice: ");

int choice=Convert.ToInt32(Console.ReadLine());
Departments dept;
switch (choice)
{
    case 1: 
        dept=Departments.DigitalEngineering;
    break;
    case 2:
        dept = Departments.HR;
        break;

    case 3:
        dept = Departments.Finance;
        break;

    case 4:
        dept = Departments.Marketing;
        break;

    default:
        Console.WriteLine("Invalid choice.");
        return;
}

// Switch

switch (dept)
{
    case Departments.DigitalEngineering:
        Console.WriteLine("You work with technology!");
        break;

    case Departments.HR:
        Console.WriteLine("You work with people!");
        break;

    case Departments.Finance:
        Console.WriteLine("You work with money!");
        break;

    case Departments.Marketing:
        Console.WriteLine("You work with marketing!");
        break;

    default:
        Console.WriteLine("Department not recognized.");
        break;
}
enum Departments
{
    DigitalEngineering,
    HR,
    Finance,
    Marketing
}