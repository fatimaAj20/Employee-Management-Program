using Courseproject.Common.Dtos.Employee;

namespace Courseproject.Common.Dtos.Team;

public record TeamGet(int Id,string Name, List<EmployeeList> Employees);