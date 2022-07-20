using GrowthTracker.BackEnd.Dto;
using GrowthTracker.BackEnd.Integration.Excel;
using GrowthTracker.BackEnd.Model;
using IngenuityNow.Common.Data;
using IngenuityNow.Common.Data.DataService;
using IngenuityNow.Common.Result;

namespace GrowthTracker.BackEnd.Orchestrator;

public interface ICompetencyOrchestrator
{
    Task<ItemResult<Competency>> AddCompetencyAsync(AddCompetencyDto dto);
    Task<Result> ClearAllCompetencies();
    Task<Result> LoadFromExcel();
}

public class CompetencyOrchestrator : ICompetencyOrchestrator
{
    private readonly IDataService<Competency> _competencyDataService;
    private readonly IUnitOfWork<IGrowthTrackerContext> _unitOfWork;

    public CompetencyOrchestrator(IDataService<Competency> competencyDataService, IUnitOfWork<IGrowthTrackerContext> unitOfWork)
    {
        _competencyDataService = competencyDataService;
        _unitOfWork = unitOfWork;
    }

    public async Task<ItemResult<Competency>> AddCompetencyAsync(AddCompetencyDto dto)
    {
        var competency = new Competency { KeyArea = dto.KeyArea, Attribute = dto.Attribute, Title = dto.Title };
        competency.Level1Description = dto.Level1Description;
        competency.Level2Description = dto.Level2Description;
        competency.Level3Description = dto.Level3Description;
        competency.Level4Description = dto.Level4Description;
        competency.Level5Description = dto.Level5Description;
        competency.Level6Description = dto.Level6Description;
        _competencyDataService.Add(competency);
        await _unitOfWork.SaveAllAsync();
        return ItemResult<Competency>.Success(competency);
    }

    public async Task<Result> ClearAllCompetencies()
    {
        var all = await _competencyDataService.ListAsync();
        foreach(var competency in all)
        {
            await _competencyDataService.DeleteAsync(competency.Id);
        }
        return Result.Success();
    }

    public async Task<Result> LoadFromExcel()
    {
        ExcelReader reader = new ExcelReader();
        var results = reader.Read();

        foreach(var result in results)
        {
            var competency = new Competency { KeyArea = result.KeyArea, Attribute = result.Attribute, Title = result.Title };
            competency.Level1Description = result.Level1Description;
            competency.Level2Description = result.Level2Description;
            competency.Level3Description = result.Level3Description;
            competency.Level4Description = result.Level4Description;
            competency.Level5Description = result.Level5Description;
            competency.Level6Description = result.Level6Description;

            _competencyDataService.Add(competency);
        }
        await _unitOfWork.SaveAllAsync();

        return Result.Success();

    }
}