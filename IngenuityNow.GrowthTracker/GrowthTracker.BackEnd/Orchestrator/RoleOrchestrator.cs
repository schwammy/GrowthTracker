using GrowthTracker.BackEnd.Dto;
using GrowthTracker.BackEnd.Integration.Excel;
using GrowthTracker.BackEnd.Model;
using IngenuityNow.Common.Data;
using IngenuityNow.Common.Data.DataService;
using IngenuityNow.Common.Result;

namespace GrowthTracker.BackEnd.Orchestrator;

public interface IRoleOrchestrator
{
    Task<Result> AddRoleAsync(AddRoleDto dto);
    Task<Result> AddRoleCompetencyAsync(AddRoleCompetencyDto dto);
    Task<Result> LoadAllCompetencies(int roleId);
}

public class RoleOrchestrator : IRoleOrchestrator
{
    private readonly IDataService<Role> _roleDataService;
    private readonly IDataService<Competency> _competencyDataService;
    private readonly IDataService<RoleCompetency> _roleCompetencyDataService;
    private readonly IUnitOfWork<IGrowthTrackerContext> _unitOfWork;
    private readonly IExcelReader _excelReader;

    public RoleOrchestrator(IDataService<Role> roleDataService, IUnitOfWork<IGrowthTrackerContext> unitOfWork, IDataService<RoleCompetency> roleCompetencyDataService, IExcelReader excelReader, IDataService<Competency> competencyDataService)
    {
        _roleDataService = roleDataService;
        _unitOfWork = unitOfWork;
        _roleCompetencyDataService = roleCompetencyDataService;
        _excelReader = excelReader;
        _competencyDataService = competencyDataService;
    }

    public async Task<Result> AddRoleAsync(AddRoleDto dto)
    {
        Role role = new Role { Name = dto.Name, RoleGroup = dto.RoleGroup, Level1Title = dto.Level1Title, Level2Title = dto.Level2Title, Level3Title = dto.Level3Title, Level4Title = dto.Level4Title, Level5Title = dto.Level5Title, Level6Title = dto.Level6Title };

        _roleDataService.Add(role);
        await _unitOfWork.SaveAllAsync();
        return Result.Success();
    }

    public async Task<Result> AddRoleCompetencyAsync(AddRoleCompetencyDto dto)
    {
        RoleCompetency rc = new RoleCompetency();
        rc.RoleId = dto.RoleId;
        rc.CompetencyId = dto.CompetencyId;
        rc.EffectiveDate = dto.EffectiveDate;
        rc.ExpectedLevel1 = dto.ExpectedLevel1;
        rc.ExpectedLevel2 = dto.ExpectedLevel2;
        rc.ExpectedLevel3 = dto.ExpectedLevel3;
        rc.ExpectedLevel4 = dto.ExpectedLevel4;
        rc.ExpectedLevel5 = dto.ExpectedLevel5;
        rc.ExpectedLevel6 = dto.ExpectedLevel6;


        _roleCompetencyDataService.Add(rc);
        await _unitOfWork.SaveAllAsync();
        return Result.Success();

    }

    public async Task<Result> LoadAllCompetencies(int roleId)
    {
        var role = await _roleDataService.GetAsync(roleId);

        //NOTE not the most effecient way, but the quickest for now...
        // get the competencies for Id lookup
        var allCompetencies = await _competencyDataService.ListAsync();



        var results = _excelReader.ReadAllRoleCompetencies(role.Name);

        
        foreach (var result in results)
        {
            var competency = allCompetencies.SingleOrDefault(c => c.KeyArea == result.KeyArea && c.Attribute == result.Attribute && c.Title == result.Title);
            var rc = new RoleCompetency();
            rc.RoleId = roleId;
            rc.CompetencyId = competency.Id;
            rc.EffectiveDate = DateTime.Now;
            rc.ExpectedLevel1 = 1;
            rc.ExpectedLevel2 = 2;
            rc.ExpectedLevel3 = 3;
            rc.ExpectedLevel4 = 4;
            rc.ExpectedLevel5 = 5;
            rc.ExpectedLevel6 = 6;


            _roleCompetencyDataService.Add(rc);
        }
        await _unitOfWork.SaveAllAsync();

        return Result.Success();
    }
}
