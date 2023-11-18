﻿using MMASimulation.Shared.Dtos.Fights;
using MMASimulation.Shared.Models.Utils;

namespace MMASimulation.Server.Services.FightService
{
    public interface IFightService
    {

        Task<ServiceResponse<List<FightDto>>> GetAllFights();
        Task<ServiceResponse<FightDto>> GetFightById(int fightId);
        Task<ServiceResponse<FightDto>> CreateFight(FightDto fight);

    }
}
