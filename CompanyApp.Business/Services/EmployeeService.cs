using CompanyApp.Business.Interfaces;
using CompanyApp.DataContent.Repositories;
using CompanyApp.Domain.Entities;
using CompanyAppHelper;
using System.Text.RegularExpressions;

namespace CompanyApp.Business.Services;

public class EmployeeService : IEmployeeService
{
    private readonly DepartmentRepository _deparmtnetRepository;
    private readonly EmployeeRepository _employeeRepository;
    private static int _employeeCount;

    public EmployeeService()
    {
        _deparmtnetRepository = new();
        _employeeRepository = new();
        _employeeCount = 1;
    }

    public void Create(Employee employee)
    {
        if (employee.Name.Length < 3 || employee.Surname.Length < 3)
        {
            Helper.ChangeTextColor(ConsoleColor.Red, "Employee nin ad ve soyadi minimum 3 simvol olmalidir");
            return;
        }
        if (employee.Age < 0)
        {

            Helper.ChangeTextColor(ConsoleColor.Red, "Duzgun yash daxil edin");
            return;
        }
        Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        Match match = regex.Match(employee.Address);
        if (!match.Success)
        {
            Helper.ChangeTextColor(ConsoleColor.Red, "Duzgun email daxil edin");
            return;
        }
        var department = _deparmtnetRepository.Get(x => x.Id == employee.DepartmentId);
        if (department is null)
        {
            Helper.ChangeTextColor(ConsoleColor.Red, "Department movcud deyil!");
            return;
        }
        if (department.Capacity <= department.Employees.Count)
        {
            Helper.ChangeTextColor(ConsoleColor.Red, "Department artiq doludur!");
            return;
        }
        employee.Department = department;
        employee.Id = _employeeCount++;
        if (_employeeRepository.Create(employee))
        {
            Helper.ChangeTextColor(ConsoleColor.Green, "Employee ugurla yaradildi");
            department.Employees.Add(employee);
            return;
        }
        Helper.ChangeTextColor(ConsoleColor.Red, "Gozlenilmez xeta bash verdi yeniden cehd edin");

    }

    public void Delete(int id)
    {
        var employee = _employeeRepository.Get(x => x.Id == id);
        if (employee is null)
        {
            Helper.ChangeTextColor(ConsoleColor.Red, "Employee movcud deyil");
            return;

        }
        if (_employeeRepository.Delete(employee))
        {
            Helper.ChangeTextColor(ConsoleColor.Green, "Employee ugurla Silindi");
            return;
        }
        Helper.ChangeTextColor(ConsoleColor.Red, "Gozlenilmez xeta bash verdi yeniden cehd edin");

    }

    public Employee Get(int id)
    {
        var employee = _employeeRepository.Get(x => x.Id == id);
        if (employee is null)
        {
            Helper.ChangeTextColor(ConsoleColor.Red, "Employee movcud deyil");
            return null;

        }
        return employee;
    }

    public List<Employee> GetAll()
    {
        return _employeeRepository.GetAll();
    }

    public List<Employee> GetAll(string search)
    {
        var employees = _employeeRepository.GetAll(x => (x.Name + " " + x.Surname).ToLower().Contains(search.Trim().ToLower()));
        if (employees.Count == 0)
        {

            Helper.ChangeTextColor(ConsoleColor.Yellow, "Axtardiginiz employee tapilmadi");
        }

        return employees;
    }

    public void Update(Employee employee)
    {
        var existedEmployee = _employeeRepository.Get(x => x.Id == employee.Id);
        if (existedEmployee is null)
        {
            Helper.ChangeTextColor(ConsoleColor.Red, "Employee movcud deyil");
            return;

        }
        if (employee.Name.Length < 3 || employee.Surname.Length < 3)
        {
            Helper.ChangeTextColor(ConsoleColor.Red, "Employee nin ad ve soyadi minimum 3 simvol olmalidir");
            return;
        }
        if (employee.Age < 0)
        {

            Helper.ChangeTextColor(ConsoleColor.Red, "Duzgun yash daxil edin");
            return;
        }
        Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        Match match = regex.Match(employee.Address);
        if (!match.Success)
        {
            Helper.ChangeTextColor(ConsoleColor.Red, "Duzgun email daxil edin");
            return;
        }
        var department = _deparmtnetRepository.Get(x => x.Id == employee.DepartmentId);
        if (department is null)
        {
            Helper.ChangeTextColor(ConsoleColor.Red, "Department movcud deyil!");
            return;
        }
        employee.Department = department;
        if (department.Capacity <= department.Employees.Count)
        {
            Helper.ChangeTextColor(ConsoleColor.Red, "Department artiq doludur!");
            return;
        }
        var existedDepartment = existedEmployee.Department;
        existedEmployee = employee;
        if (_employeeRepository.Update(existedEmployee))
        {
            Helper.ChangeTextColor(ConsoleColor.Yellow, "Employee ugurla Update edildi");
            existedDepartment.Employees.Remove(existedEmployee);
            department.Employees.Add(existedEmployee);
            return;
        }
        Helper.ChangeTextColor(ConsoleColor.Red, "Gozlenilmez xeta bash verdi yeniden cehd edin");
    }

    public Employee GetByAddress(string adress)
    {
        var employee = _employeeRepository.Get(x => x.Address.ToLower()==adress.ToLower().Trim());
        if (employee is null)
        {
            Helper.ChangeTextColor(ConsoleColor.Red, "Employee movcud deyil");
            return null;

        }
        return employee;
    }
}
