public class Employee
{
  public int Id {get;set;}
  public string FirstName {get;set;} = string.Empty;
  public string LastName {get; set;}=string.Empty;

  private string _email=string.Empty;
  private string _department=string.Empty;
  private int _salary;

  public string Email
    {
        get{return _email;}
        set
        {
            if (value.Contains("@"))
            {
                _email=value;
            }
            else
            {
                Console.WriteLine("Email must contain @. ");
            }
        }
    }
    public string Department
    {
        get{return _department;}
        set
        {
            if (value == "IT" || value == "HR" || value == "Marketing" || value == "Finance" || value == "BPO")
            {
                _department=value;
            }
            else
            {
                Console.WriteLine("Department not listed");
            }
        } 
    }

    public int Salary
    {
        get{ return _salary;}
        set
        {
            if (value > 0)
            {
                _salary=value;
            }
            else
            {
                Console.WriteLine("Salary must be greater than 0.");
            }
        }
    }

    public bool IsActive{get; set;}

    public Employee(
        int id,
        string firstName,
        string lastName,
        string email,
        string department,
        int salary,
        bool isActive)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Department = department;
        Salary = salary;
        IsActive = isActive;
    }





}