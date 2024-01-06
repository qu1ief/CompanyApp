using CompanyApp.Business.Services;
using CompanyApp.Domain.Entities;
using CompanyAppHelper;

DepartmentService _departmentService = new();
EmployeeService _employeeService = new();

Department department = new()
{

    Name = "Default",
    Capacity = 100

};
_departmentService.Create(department);
Console.Clear();

Helper.ChangeTextColor(ConsoleColor.Green, "Welcome \n");
restart:
_printMenu();
string menu = Console.ReadLine();
switch (menu)
{
    case "1":
        Console.Clear();
        var deparment = _createDepartment();
        if (deparment is not null)
        {
            _departmentService.Create(deparment);
        }
        goto restart;
    case "2":
        Console.Clear();
        var updatedDepartment = _updateDepartment(_departmentService);
        if (updatedDepartment is not null)
        {
            _departmentService.Update(updatedDepartment);

        }
        goto restart;

    case "3":
        Console.Clear();
        var deletedDepartment = _deleteDepartment(_departmentService);
        if (deletedDepartment is not null)
        {
            _departmentService.Delete(deletedDepartment.Id);

        }
        goto restart;
    case "4":
        Console.Clear();
        Console.WriteLine(_departmentService.Get(_getById())); ;
        goto restart;

    case "5":
        Console.Clear();
        _printDepartments(_departmentService.GetAll(_search()));
        goto restart;

    case "6":
        Console.Clear();
        Helper.ChangeTextColor(ConsoleColor.Yellow, "Department secin");
        var departmentE = _selectDepartment(_departmentService);
        if (departmentE is not null)
        {
            _printEmployees(_departmentService.GetAllDepartmentsEmployees(departmentE.Id));
        }
        goto restart;

    case "7":
        Console.Clear();
        _printDepartments(_departmentService.GetAll());
        goto restart;

    case "8":
        Console.Clear();
        var createEmployee = _createEmployee(_departmentService);
        if (createEmployee is not null)
        {
            _employeeService.Create(createEmployee);
        }
        goto restart;

    case "9":
        Console.Clear();
        var updatedEmployee = _updateEmployee(_departmentService, _employeeService);
        if (updatedEmployee is not null)
        {
            _employeeService.Update(updatedEmployee);
        }
        goto restart;

    case "10":
        Console.Clear();
        var deletedEmployee = _deleteEmployee(_employeeService);
        if (deletedEmployee is not null)
        {
            _employeeService.Delete(deletedEmployee.Id);
        }
        goto restart;


    case "11":
        Console.Clear();
        int id = _getById();
        var employee = _employeeService.Get(id);
        if (employee is not null)
        {
            Helper.ChangeTextColor(ConsoleColor.White, $"{employee.Id}. {employee.Name} {employee.Surname} {employee.Age} email:{employee.Address} -Department:{employee.Department.Name} ");
        }
        goto restart;

    case "12":
        Console.Clear();
        var adressEmployee = _employeeService.GetByAddress(_search());
        if (adressEmployee is not null)
        {
            Helper.ChangeTextColor(ConsoleColor.Yellow, adressEmployee.ToString());
        }
        goto restart;
    case "13":
        Console.Clear();
        _printEmployees(_employeeService.GetAll(_search()));
        goto restart; ;

    case "14":
        _printEmployees(_employeeService.GetAll());
        goto restart;

    case "15":
        Helper.ChangeTextColor(ConsoleColor.Magenta, "Goodbye");
        break;
    default:
        Helper.ChangeTextColor(ConsoleColor.DarkRed, "Please enter valid number");
        goto restart;
}






static Employee _createEmployee(DepartmentService departmentService)
{
    Helper.ChangeTextColor(ConsoleColor.Yellow, "Employee adi daxil edin");
    string name = Console.ReadLine();
    Helper.ChangeTextColor(ConsoleColor.Yellow, "Employee soyadi daxil edin");
    string surname = Console.ReadLine();
    Helper.ChangeTextColor(ConsoleColor.Yellow, "Employee yashi daxil edin");
    byte age;
    bool isParse = byte.TryParse(Console.ReadLine(), out age);
    if (!isParse)
    {
        Helper.ChangeTextColor(ConsoleColor.Red, "Duzgun eded daxil edin");
        return null;
    }
    Helper.ChangeTextColor(ConsoleColor.Yellow, "Employee addressi daxil edin");
    string address = Console.ReadLine();
    Helper.ChangeTextColor(ConsoleColor.Yellow, "Department secin");
_selectDepartment:
    var department = _selectDepartment(departmentService);
    if (department == null)
    {
        Helper.ChangeTextColor(ConsoleColor.Red, "duzgun secim edin");
        goto _selectDepartment;
    }

    Employee employee = new()
    {
        Name = name,
        Surname = surname,
        Address = address,
        Age = age,
        Department = department,
        DepartmentId = department.Id,
    };
    return employee;
}
static Employee _updateEmployee(DepartmentService departmentService, EmployeeService employeeService)
{
    var employee = _selectEmployee(employeeService);
    if (employee is null)
    {
        return null;
    }
    Helper.ChangeTextColor(ConsoleColor.Yellow, "Employee adi daxil edin");
    string name = Console.ReadLine();
    Helper.ChangeTextColor(ConsoleColor.Yellow, "Employee soyadi daxil edin");
    string surname = Console.ReadLine();
    Helper.ChangeTextColor(ConsoleColor.Yellow, "Employee yashi daxil edin");
    byte age;
    bool isParse = byte.TryParse(Console.ReadLine(), out age);
    if (!isParse)
    {
        Helper.ChangeTextColor(ConsoleColor.Red, "Duzgun eded daxil edin");
        return null;
    }
    Helper.ChangeTextColor(ConsoleColor.Yellow, "Employee addressi daxil edin");
    string address = Console.ReadLine();
    Helper.ChangeTextColor(ConsoleColor.Yellow, "Department secin");
_selectDepartment:
    var department = _selectDepartment(departmentService);
    if (department == null)
    {
        Helper.ChangeTextColor(ConsoleColor.Red, "duzgun secim edin");
        goto _selectDepartment;
    }

    employee.Name = name;
    employee.Surname = surname;
    employee.Age = age;
    employee.Address = address;
    employee.Department = department;
    employee.DepartmentId = department.Id;
    return employee;
}
static Employee _deleteEmployee(EmployeeService employeeService)
{
    Helper.ChangeTextColor(ConsoleColor.Red, "Sileceyiniz Employeeni secin");
    var employee = _selectEmployee(employeeService);
    return employee;
}
static Department _createDepartment()
{
    Helper.ChangeTextColor(ConsoleColor.Yellow, "Department adi daxil edin");
    string name = Console.ReadLine();

    Helper.ChangeTextColor(ConsoleColor.Yellow, "Department capacitysi daxil edin");
    int capacity;
    bool tryParse = int.TryParse(Console.ReadLine(), out capacity);
    if (!tryParse)
    {
        Helper.ChangeTextColor(ConsoleColor.Red, "Duzgun eded daxil edin");
        return null;

    }


    Department department = new()
    {
        Name = name,
        Capacity = capacity

    }
    ;
    return department;
}
static Department _updateDepartment(DepartmentService departmentService)
{
    Helper.ChangeTextColor(ConsoleColor.Yellow, "Department secin");

    var existed = _selectDepartment(departmentService);
    if (existed is null)
        return null;

    Helper.ChangeTextColor(ConsoleColor.Yellow, "Department adi daxil edin");
    string name = Console.ReadLine();
    Helper.ChangeTextColor(ConsoleColor.Yellow, "Department capacitysi daxil edin");
    int capacity;
    bool tryParse = int.TryParse(Console.ReadLine(), out capacity);
    if (!tryParse)
    {
        Helper.ChangeTextColor(ConsoleColor.Red, "Duzgun eded daxil edin");
        return null;

    }
    existed.Capacity = capacity;
    existed.Name = name;
    return existed;

}
static Department _deleteDepartment(DepartmentService departmentService)
{
    Helper.ChangeTextColor(ConsoleColor.Red, "Sileceyiniz Departmenti secin");
    var department = _selectDepartment(departmentService);
    if (department is null)
        return null;
    return department;
}
static int _getById()
{

    Helper.ChangeTextColor(ConsoleColor.Yellow, "Id daxil edin:");
restartId:
    int Id;
    bool tryParse = int.TryParse(Console.ReadLine(), out Id);
    if (!tryParse)
    {
        Helper.ChangeTextColor(ConsoleColor.Red, "Duzgun eded daxil edin:");
        goto restartId;
    }
    return Id;
}
static string _search()
{
    Helper.ChangeTextColor(ConsoleColor.Yellow, "Searching...");
    return Console.ReadLine();
}

static void _printDepartments(List<Department> departments)
{
    foreach (Department department in departments)
    {
        Console.WriteLine(department);
    }
}
static void _printEmployees(List<Employee> employees)
{
    foreach (Employee employee in employees)
    {
        Console.WriteLine(employee);
    }
}
static Department _selectDepartment(DepartmentService departmentService)
{
    var departments = departmentService.GetAll();
    for (int i = 0; i < departments.Count; i++)
    {
        Helper.ChangeTextColor(ConsoleColor.White, $"{departments[i].Id}.{departments[i]}");

    }
    int departmentId;
    bool tryParse = int.TryParse(Console.ReadLine(), out departmentId);
    if (!tryParse)
    {
        Helper.ChangeTextColor(ConsoleColor.Red, "Duzgun eded daxil edin");
        return null;

    }
    var existed = departmentService.Get(departmentId);

    return existed;

}


static Employee _selectEmployee(EmployeeService employeeService)
{
    var employees = employeeService.GetAll();
    for (int i = 0; i < employees.Count; i++)
    {
        Helper.ChangeTextColor(ConsoleColor.White, $"{employees[i].Id}.{employees[i]}");

    }
    int employeeId;
    bool tryParse = int.TryParse(Console.ReadLine(), out employeeId);
    if (!tryParse)
    {
        Helper.ChangeTextColor(ConsoleColor.Red, "Duzgun eded daxil edin");
        return null;

    }
    var existed = employeeService.Get(employeeId);

    return existed;

}

static void _printMenu()
{

    Helper.ChangeTextColor(ConsoleColor.Yellow, "\n1- CreateDepartment");
    Helper.ChangeTextColor(ConsoleColor.Yellow, "2- UpdateDepartment");
    Helper.ChangeTextColor(ConsoleColor.Yellow, "3- DeleteDepartment");
    Helper.ChangeTextColor(ConsoleColor.Yellow, "4- GetDepartmentById");
    Helper.ChangeTextColor(ConsoleColor.Yellow, "5- Search Department");
    Helper.ChangeTextColor(ConsoleColor.Yellow, "6- GetEmployeeByDepartment");
    Helper.ChangeTextColor(ConsoleColor.Yellow, "7- GetAllDepartments \n");

    Helper.ChangeTextColor(ConsoleColor.Cyan, "8- CreateEmployee");
    Helper.ChangeTextColor(ConsoleColor.Cyan, "9- UpdateEmployee");
    Helper.ChangeTextColor(ConsoleColor.Cyan, "10- DeleteEmployee");
    Helper.ChangeTextColor(ConsoleColor.Cyan, "11- GetEmployeeById");
    Helper.ChangeTextColor(ConsoleColor.Cyan, "12- GetEmployeeByAdress");
    Helper.ChangeTextColor(ConsoleColor.Cyan, "13- SearchEmployee");
    Helper.ChangeTextColor(ConsoleColor.Cyan, "14- GetAllEmployee");

    Helper.ChangeTextColor(ConsoleColor.Red, "\n15- ExitProgram \n");

}