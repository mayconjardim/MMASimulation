using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MMASimulation.Server.Data;
using MMASimulation.Shared.Dtos.Fights;
using MMASimulation.Shared.Models.Fights;
using MMASimulation.Shared.Models.Utils;

namespace MMASimulation.Server.Services.FightService
{
    public class FightService : IFightService
    {

        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public FightService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<FightDto>>> GetAllFights()
        {

            var fight = await _context.Fights.ToListAsync();

            var mappedFights = _mapper.Map<List<FightDto>>(fight);

            var response = new ServiceResponse<List<FightDto>>
            {
                Data = mappedFights,
                Message = mappedFights.Count == 0
                    ? "Lista de lutas vazia."
                    : $"Lista com {mappedFights.Count} lutas retornada com sucesso."
            };

            return response;
        }

        public async Task<ServiceResponse<FightDto>> GetFightById(int id)
        {
            var response = new ServiceResponse<FightDto>();

            try
            {
                var fight = await _context.Fights.FindAsync(id);

                if (fight == null)
                {
                    response.Success = false;
                    response.Message = $"Luta com ID {id} não encontrada.";
                }
                else
                {
                    var mappedFight = _mapper.Map<FightDto>(fight);
                    response.Success = true;
                    response.Data = mappedFight;
                    response.Message = "Luta encontrada com sucesso.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Erro ao obter a luta com ID {id}.";
                response.Error = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<FightDto>> CreateFight(FightCreateDto fight)
        {
            ServiceResponse<FightDto> response = new();

            try
            {
                Fight createdFight = _mapper.Map<Fight>(fight);

                _context.Add(createdFight);

                await _context.SaveChangesAsync();

                response.Success = true;
                response.Data = _mapper.Map<FightDto>(createdFight);
                response.Message = "Luta criada com sucesso.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Ocorreu um erro ao criar a luta.";
                response.Error = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<bool>> UpdateFight(int fightId)
        {
            ServiceResponse<bool> response = new ServiceResponse<bool>();

            Fight fight = await _context.Fights
                .Include(f => f.Fighter1)
                .Include(f => f.Fighter1.FighterRatings)
                .Include(f => f.Fighter1.FighterStrategies)
                .Include(f => f.Fighter1.FighterStyles)
                .Include(f => f.Fighter2)
                .Include(f => f.Fighter2.FighterRatings)
                .Include(f => f.Fighter2.FighterStrategies)
                .Include(f => f.Fighter2.FighterStyles)
                .FirstOrDefaultAsync(p => p.Id == fightId);

            if (fight == null)
            {
                response.Success = false;
                response.Message = "Jogo não encontrado.";
                return response;
            }

            fight.FightSim();

            await _context.SaveChangesAsync();

            response.Success = true;
            response.Data = true;

            return response;
        }

    }
}
