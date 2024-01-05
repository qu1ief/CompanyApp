using CompanyApp.Business.Services;
using CompanyApp.Domain.Entities;
using CompanyAppHelper;

DepartmentService _departmentService = new();

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


    case "15":
        Helper.ChangeTextColor(ConsoleColor.Magenta, "Goodbye" );
        break;
    default:
        Helper.ChangeTextColor(ConsoleColor.DarkRed, "Please enter valid number");
        goto restart;
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

static void _printMenu()
{

    Helper.ChangeTextColor(ConsoleColor.Yellow, "\n1- CreateDepartment");
    Helper.ChangeTextColor(ConsoleColor.Yellow, "2- UpdateDepartment");
    Helper.ChangeTextColor(ConsoleColor.Yellow, "3- DeleteDepartment");
    Helper.ChangeTextColor(ConsoleColor.Yellow, "4- GetDepartmentById");
    Helper.ChangeTextColor(ConsoleColor.Yellow, "5- Search Department");
    Helper.ChangeTextColor(ConsoleColor.Yellow, "6- GetEmployeeByDepartment");
    Helper.ChangeTextColor(ConsoleColor.Yellow, "7- GetAllDepartments \n");

 

    Helper.ChangeTextColor(ConsoleColor.Red, "\n15- ExitProgram \n");

}