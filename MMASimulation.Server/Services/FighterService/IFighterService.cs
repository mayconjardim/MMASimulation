using MMASimulation.Shared.Dtos.Fighters;
using MMASimulation.Shared.Models.Utils;

namespace MMASimulation.Server.Services.FighterService
{
    public interface IFighterService
    {

        Task<ServiceResponse<List<FighterDto>>> GetAllFighters();

    }
}
