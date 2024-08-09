


namespace Football.Domain.Services.Players;

public interface IPlayersService
{
    Task<Result> GetAll(int page,int size);
    Task<Result> Get(int id);
    Task<Result> Create(CreatePlayerDTO model);
    Task<Result> Delete(int id);
    Task<Result> Update(UpdatePlayerDTO model);
}
