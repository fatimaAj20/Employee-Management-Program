using AutoMapper;
using Courseproject.Business.Exceptions;
using Courseproject.Business.Validation;
using Courseproject.Common.Dtos.Address;
using Courseproject.Common.Dtos.Job;
using Courseproject.Common.Interfaces;
using Courseproject.Common.Model;
using FluentValidation;

namespace Courseproject.Business.Services;

public class JobService : IJobService
{
    private IMapper Mapper { get; }

    private IGenericRepository<Job> JobRepository { get; }
    private JobCreateValidator JobCreateValidator { get; }

    private JobUpdateValidator JobUpdateValidator { get; }
    public JobService(IMapper mapper , IGenericRepository<Job> jobRepository, JobCreateValidator jobCreateValidator,
     JobUpdateValidator jobUpdateValidator)
    {
        Mapper = mapper;
        JobRepository = jobRepository;
        JobCreateValidator = jobCreateValidator;
        JobUpdateValidator = jobUpdateValidator;
    }

    

    public async Task<int> CreateJobAsync(JobCreate jobCreate)
    {
        await JobCreateValidator.ValidateAndThrowAsync(jobCreate);

        var job = Mapper.Map<Job>(jobCreate);
        await JobRepository.InsertAsync(job);
        await JobRepository.SaveChangesAsync();
        return job.Id;
    }

    public async Task DeleteJobAsync(JobDelete jobDelete)
    {
        var entity = await JobRepository.GetByIdAsync(jobDelete.Id,(job) => job.Employees);

      if (entity == null)
        {
            throw new JobNotFoudException(jobDelete.Id);
        }
        if (entity.Employees.Count > 0)
        {
            throw new DependentEmployeesExistException(entity.Employees);
        }

        JobRepository.Delete(entity);
        await JobRepository.SaveChangesAsync();
    }

    public async Task<JobGet> GetJobAsync(int id)
    {
        var job = await JobRepository.GetByIdAsync(id);
        if (job == null)
            throw new JobNotFoudException(id);
        var jobget=Mapper.Map<JobGet>(job);
        return jobget;
    }

    public async Task<List<JobGet>> GetJobsAsync()
    {
        var jobs = await JobRepository.GetAsync(null,null);
        return Mapper.Map<List<JobGet>>(jobs);
    }

    public async Task UpdateJobAsync(JobUpdate jobUpdate)
    {
        await JobUpdateValidator.ValidateAndThrowAsync(jobUpdate);

        var existingentity = await JobRepository.GetByIdAsync(jobUpdate.Id);

        if (existingentity == null)
            throw new JobNotFoudException(jobUpdate.Id);

        var job = Mapper.Map<Job>(jobUpdate);
        JobRepository.Update(job);
        await JobRepository.SaveChangesAsync();

    }
}
