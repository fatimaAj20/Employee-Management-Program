using AutoMapper;
using Courseproject.Business.Exceptions;
using Courseproject.Business.Validation;
using Courseproject.Common.Dtos.Address;
using Courseproject.Common.Dtos.Team;
using Courseproject.Common.Interfaces;
using Courseproject.Common.Model;
using FluentValidation;
using System.Linq.Expressions;

namespace Courseproject.Business.Services;

public class TeamService : ITeamService
{
    private IMapper Mapper { get;}
    private IGenericRepository<Team > TeamRepository { get; }
    private IGenericRepository<Employee> EmployeeRepository { get; }
    private TeamCreateValidator TeamCreateValidator { get; }
    private TeamUpdateValidator TeamUpdateValidator { get; }

    public TeamService(IMapper mapper, IGenericRepository<Team> teamRepository, IGenericRepository<Employee> employeeRepository,
        TeamCreateValidator teamCreateValidator, TeamUpdateValidator teamUpdateValidator)
    {
        Mapper = mapper;
        TeamRepository = teamRepository;
        EmployeeRepository = employeeRepository;
        TeamCreateValidator =teamCreateValidator;
        TeamUpdateValidator = teamUpdateValidator;
    }
    public async Task<int> CreateTeamAsync(TeamCreate teamCreate)
    {
        await TeamCreateValidator.ValidateAndThrowAsync(teamCreate);

        Expression<Func<Employee, bool>> employeefilter = (employee) => teamCreate.Employees.Contains(employee.Id);
        var employees = await EmployeeRepository.GetFilterAsync(new Expression<Func<Employee, bool>>[] { employeefilter }, null, null);
        var missingEmployees = teamCreate.Employees.Where((id) => !employees.Any(existing => existing.Id == id));
        if(missingEmployees.Any())
        {
            throw new EmployeesNotFoundException(missingEmployees.ToArray());
        }
        var team=Mapper.Map<Team>(teamCreate);
        team.Employees=employees;
        int id = await TeamRepository.InsertAsync(team);
        await TeamRepository.SaveChangesAsync();
        return id;
    }

    public async Task DeleteTeamAsync(TeamDelete teamDelete)
    {
        var team = await TeamRepository.GetByIdAsync(teamDelete.Id);

        if (team == null)
            throw new TeamNotFoundException(teamDelete.Id);

        TeamRepository.Delete(team);
        await TeamRepository.SaveChangesAsync();
    }

    public async Task<TeamGet> GetTeamAsync(int id)
    {
        var team = await TeamRepository.GetByIdAsync(id,(team)=> team.Employees);
        if (team == null)
            throw new TeamNotFoundException(id);
        return Mapper.Map<TeamGet>(team);

    }

    public async Task<List<TeamGet>> GetTeamsAsync()
    {
        var teams = await TeamRepository.GetAsync(null,null,(team) => team.Employees);
        return Mapper.Map<List<TeamGet>>(teams);
    }

    public async Task UpdateTeamAsync(TeamUpdate teamUpdate)
    {
        await TeamUpdateValidator.ValidateAndThrowAsync(teamUpdate);


        Expression<Func<Employee, bool>> employeefilter = (employee) => teamUpdate.Employees.Contains(employee.Id);
        var employees = await EmployeeRepository.GetFilterAsync(new Expression<Func<Employee, bool>>[] { employeefilter }, null, null);
        var missingEmployees = teamUpdate.Employees.Where((id) => !employees.Any(existing => existing.Id == id));
        if (missingEmployees.Any())
        {
            throw new EmployeesNotFoundException(missingEmployees.ToArray());
        }
        var existingteam= await TeamRepository.GetByIdAsync(teamUpdate.Id,(team)=>team.Employees);
        if (existingteam == null)
            throw new TeamNotFoundException(teamUpdate.Id);
        Mapper.Map(teamUpdate,existingteam);
        existingteam.Employees = employees;
        TeamRepository.Update(existingteam);
        await TeamRepository.SaveChangesAsync();
    }
}
