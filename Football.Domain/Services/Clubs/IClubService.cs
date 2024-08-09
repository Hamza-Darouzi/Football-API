
namespace Football.Domain.Services.Clubs;

public interface IClubService
{
    Task<Result> GetAll(int page,int size);
    Task<Result> Get(int id);
    Task<Result> Create(CreateClubDTO model);
    Task<Result> Delete(int id);
    Task<Result> Update(UpdateClubDTO model);
}
