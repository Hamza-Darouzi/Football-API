namespace Football.Domain.Services.Leagues;

public interface ILeagueService
{
    Task<Result> GetAll(int page,int size);
    Task<Result> Get(int id);
    Task<Result> Create(CreateLeagueDTO model);
    Task<Result> Delete(int id);
    Task<Result> Update(UpdateLeagueDTO model);
}
