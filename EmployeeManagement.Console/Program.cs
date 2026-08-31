List<Employee> employees=new List<Employee>();

employees.Add(new Employee(
    351261,
    "Shreya",
    "Sharan",
    "shreyasharan@gmial.com",
    "HR",
    66000,
    true
));
employees.Add(new Employee(
    351262,
    "Divyansh",
    "Goyal",
    "Divyanshgoyal12@gmial.com",
    "IT",
    90000,
    true
));
employees.Add(new Employee(
    351263,
    "Aarav",
    "Chadha",
    "achadha@gmial.com",
    "Marketing",
    6000,
    false
));
employees.Add(new Employee(
    351264,
    "Venkat",
    "Palliram",
    "venkatpalli1@gmial.com",
    "Finance",
    96000,
    true
));
employees.Add(new Employee(
    351265,
    "Ratna",
    "Mehandi",
    "crazyratna@gmial.com",
    "BPO",
    36000,
    true
));


while (true)
{
    Console.WriteLine("\nWelcome to Employee Management!");
    Console.WriteLine("1. Add an Employee");
    Console.WriteLine("2. View Employees");
    Console.WriteLine("3. Search an Employee");
    Console.WriteLine("4. Update Employee details");
    Console.WriteLine("5. Delete Employee");
    Console.WriteLine("6. Exit");
    Console.Write("Enter your choice: ");

    if(!int.TryParse(Console.ReadLine(), out int choice))
    {
        Console.WriteLine("Please enter a number from 1 to 6.");
        continue;
    }

    switch (choice)
    {
        case 1:
            // Add Employee code goes here
            Console.WriteLine("Enter employee ID: ");
            if (!int.TryParse(Console.ReadLine(), out int newId) || newId<=0)
            {
                Console.WriteLine("Employee ID must be a valid positive whole number.");
                break;
            }

            bool idExists=employees.Any(e => e.Id==newId);

            if (idExists)
            {
                Console.WriteLine("An employee with this ID already exists.");
                break;
            }
            Console.WriteLine("Enter first name: ");
            string firstName=Console.ReadLine()??"";
            if(string.IsNullOrWhiteSpace(firstName))
            {
                Console.WriteLine("First name cannot be empty.");
                break;
            }

            Console.Write("Enter last name: ");
            string lastName = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(lastName))
            {
                Console.WriteLine("Last name cannot be empty.");
                break;
            }

            Console.Write("Enter email: ");
            string email = Console.ReadLine() ?? "";

            if (!email.Contains("@"))
            {
                Console.WriteLine("Email must contain @.");
                break;
            }

            Console.Write("Enter department (IT, HR, Marketing, Finance, BPO): ");
            String department=Console.ReadLine()??"";
            if(department != "IT" &&
                department != "HR" &&
                department != "Marketing" &&
                department != "Finance" &&
                department != "BPO")
            {
                Console.Write("Department not Listed");
                break;

            }

            Console.Write("Enter salary: ");
            if(!int.TryParse(Console.ReadLine(),out int salary)|| salary <= 0)
            {
                Console.WriteLine("Salary must be a valid positive whole number.");
                break;
            }

            Console.WriteLine("Is the employee active? Enter Y or N: ");
            string activeInput=Console.ReadLine()??"";
            if(activeInput.ToUpper() !="Y" && activeInput.ToUpper() != "N")
            {
                Console.WriteLine("Please enter only Y or N.");
                break;
            }

            bool isActive=activeInput.ToUpper()=="Y";

            Employee newEmployee=new Employee(
                newId,
                firstName,
                lastName,
                email,
                department,
                salary,
                isActive
            );
            employees.Add(newEmployee);
             Console.WriteLine(
                $"Employee {newEmployee.FirstName} {newEmployee.LastName} added successfully!"
            );

            break;

        case 2:
            // View Employees code goes here
            foreach (Employee em in employees){
                Console.WriteLine(
                    $"Employee Id: {em.Id} | " +
                    $"Employee Name: {em.FirstName} {em.LastName} | " +
                    $"Email: {em.Email} | " +
                    $"Department: {em.Department} | " +
                    $"Salary: {em.Salary} | " +
                    $"Active: {em.IsActive}"
                    );
                }
            break;

        case 3:
            // Search Employee code goes here
            Console.WriteLine("Enter an Id: ");
                string? IdInput=Console.ReadLine();
                if(int.TryParse(IdInput,out int enteredId))
                {
                    bool found = false;
                    foreach(Employee em in employees)
                    {
                        if (em.Id == enteredId)
                        {
                            Console.WriteLine($"{em.FirstName} {em.LastName} works in {em.Department}");
                            found=true;

                        }
                    }
                    if (!found)
                    {
                        Console.WriteLine("No employee with this ID");
                    }
                }
                else
                    {
                        Console.WriteLine("Please enter a valid whole-number ID.");

                    }
            break;

case 4:
    Console.Write("Enter the Employee ID to update: ");

    if (!int.TryParse(Console.ReadLine(), out int empId) || empId <= 0)
    {
        Console.WriteLine("Please enter a valid positive employee ID.");
        break;
    }

    Employee? employeeToUpdate = employees.FirstOrDefault(e => e.Id == empId);

    if (employeeToUpdate == null)
    {
        Console.WriteLine("No employee found with this ID.");
        break;
    }

    Console.WriteLine(
        $"Updating {employeeToUpdate.FirstName} {employeeToUpdate.LastName}"
    );

    Console.Write("Enter new salary: ");

    if (!int.TryParse(Console.ReadLine(), out int newSalary) || newSalary <= 0)
    {
        Console.WriteLine("Salary must be a valid positive number.");
        break;
    }

    Console.Write("Enter new department (IT, HR, Marketing, Finance, BPO): ");
    string newDepartment = Console.ReadLine() ?? "";

    if (newDepartment != "IT" &&
        newDepartment != "HR" &&
        newDepartment != "Marketing" &&
        newDepartment != "Finance" &&
        newDepartment != "BPO")
    {
        Console.WriteLine("Department not listed.");
        break;
    }

    employeeToUpdate.Salary = newSalary;
    employeeToUpdate.Department = newDepartment;

    Console.WriteLine("Employee details updated successfully!");
    break;

        case 5:
            // Delete Employee code goes here
            Console.WriteLine("Enter the EmployeeID to delete: ");
            if(!int.TryParse(Console.ReadLine(),out int empID) || empID <= 0)
            {
                Console.WriteLine("Enter a valid positive employeeID.");
                break;
            }
            Employee? employeeToDelete=employees.FirstOrDefault(e => e.Id==empID);
            if (employeeToDelete == null)
            {
                Console.WriteLine("No employee with this Id found.");
                break;
            }
            Console.WriteLine(
                $"Employee Found: {employeeToDelete.FirstName} "+
                $"{employeeToDelete.LastName} ({employeeToDelete.Department})"
            );

            Console.WriteLine("Are you sure you want to delete this employee? Enter Y or N");
            string confirmation=Console.ReadLine()??"";
            if (confirmation.ToUpper() == "Y")
            {
                employees.Remove(employeeToDelete);
                Console.WriteLine("Employee deleted successfully");
            }
            else if (confirmation.ToUpper() == "N")
            {
                Console.WriteLine("Delete operation cancelled");
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }
            break;

        case 6:
            Console.WriteLine("Closing Employee Management. Goodbye!");
            return;

        default:
            Console.WriteLine("Invalid choice. Enter a number from 1 to 6.");
            break;
    }
}

// TO CHECK HOW MANY EMPLOYEES EARN ABOVE THAN THE INPUT SALARY
/* Console.WriteLine("Enter a salary: ");
string? salaryInput=Console.ReadLine();
if(int.TryParse(salaryInput, out int enteredSalary))
{
    foreach( Employee em in employees)
    {
        if (em.Salary > enteredSalary)
        {
            Console.WriteLine($"{em.FirstName} earns {em.Salary}");
        }
    }
}
else
{
    Console.WriteLine("Please enter a valid salary");
} */





