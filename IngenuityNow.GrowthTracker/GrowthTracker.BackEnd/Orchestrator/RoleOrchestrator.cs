using GrowthTracker.BackEnd.Dto;
using GrowthTracker.BackEnd.Model;
using IngenuityNow.Common.Data;
using IngenuityNow.Common.Data.DataService;
using IngenuityNow.Common.Result;

namespace GrowthTracker.BackEnd.Orchestrator;

public interface IRoleOrchestrator
{
    Task<Result> AddRoleAsync(AddRoleDto dto);
}

public class RoleOrchestrator : IRoleOrchestrator
{
    private readonly IDataService<Role> _roleDataService;
    private readonly IUnitOfWork<IGrowthTrackerContext> _unitOfWork;

    public RoleOrchestrator(IDataService<Role> roleDataService, IUnitOfWork<IGrowthTrackerContext> unitOfWork)
    {
        _roleDataService = roleDataService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> AddRoleAsync(AddRoleDto dto)
    {
        Role role = new Role { Name = dto.Name, RoleGroup = dto.RoleGroup, Level1Title = dto.Level1Title, Level2Title = dto.Level2Title, Level3Title = dto.Level3Title, Level4Title = dto.Level4Title, Level5Title = dto.Level5Title, Level6Title = dto.Level6Title };

        _roleDataService.Add(role);
        await _unitOfWork.SaveAllAsync();
        return Result.Success();
    }
}
