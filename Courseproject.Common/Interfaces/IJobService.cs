using Courseproject.Common.Dtos.Job;

namespace Courseproject.Common.Interfaces;

public interface IJobService
{
    Task<int> CreateJobAsync(JobCreate jobCreate);
    Task UpdateJobAsync(JobUpdate jobUpdate);
    Task DeleteJobAsync(JobDelete jobDelete);
    Task<JobGet> GetJobAsync(int id);
    Task<List<JobGet>> GetJobsAsync();
}
