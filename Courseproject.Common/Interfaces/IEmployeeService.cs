
using Courseproject.Common.Dtos.Employee;
using Courseproject.Common.Dtos.Job;

namespace Courseproject.Common.Interfaces;

public interface IEmployeeService
{
    Task<int> CreateEmployeeAsync(EmployeeCreate employeeCreate);
    Task UpdateEmployeeAsync(EmployeeUpdate employeeUpdate);
    Task UpdateProfilePhotoAsync(ProfilePhotoUpdate profilePhoto);
    Task DeleteEmployeeAsync(EmployeeDelete employeeDelete);
    Task<EmployeeDetails> GetEmployeeAsync(int id);
    Task<List<EmployeeList>> GetEmployeesAsync(EmployeeFilter employeeFilter);
}
