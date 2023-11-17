﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MMASimulation.Server.Data;
using MMASimulation.Shared.Dtos.Fighters;
using MMASimulation.Shared.Models.Utils;

namespace MMASimulation.Server.Services.FighterService
{
    public class FighterService : IFighterService
    {

        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public FighterService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<FighterDto>>> GetAllFighters()
        {

            var fighters = await _context.Fighters.ToListAsync();

            var response = new ServiceResponse<List<FighterDto>>
            {
                Data = _mapper.Map<List<FighterDto>>(fighters)
            };

            return response;

        }
    }
}
