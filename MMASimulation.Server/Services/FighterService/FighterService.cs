using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MMASimulation.Server.Data;
using MMASimulation.Shared.Dtos.Fighters;
using MMASimulation.Shared.Models.Fighters;
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

            var mappedFighters = _mapper.Map<List<FighterDto>>(fighters);

            var response = new ServiceResponse<List<FighterDto>>
            {
                Data = mappedFighters,
                Message = mappedFighters.Count == 0
                    ? "Lista de lutadores vazia."
                    : $"Lista com {mappedFighters.Count} lutadores retornada com sucesso."
            };

            return response;
        }

        public async Task<ServiceResponse<FighterDto>> CreateFighter(FighterCreateDto fighter)
        {
            ServiceResponse<FighterDto> response = new();

            try
            {
                Fighter createdFighter = _mapper.Map<Fighter>(fighter);

                createdFighter.FighterStrategies = new FighterStrategies
                {
                    Fighter = createdFighter
                };

                createdFighter.FighterStyles = new FighterStyles
                {
                    Fighter = createdFighter
                };

                _context.Add(createdFighter);

                await _context.SaveChangesAsync();

                response.Success = true;
                response.Data = _mapper.Map<FighterDto>(createdFighter);
                response.Message = "Jogador criado com sucesso.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Ocorreu um erro ao criar o jogador. {ex.Message}";
                response.Error = ex.Message;
            }

            return response;
        }

    }
}
