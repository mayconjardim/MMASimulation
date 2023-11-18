using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MMASimulation.Server.Data;
using MMASimulation.Shared.Dtos.Fighters;
using MMASimulation.Shared.Dtos.Fights;
using MMASimulation.Shared.Models.Fighters;
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

        public async Task<ServiceResponse<FightDto>> CreateFight(FightDto fight)
        {

            ServiceResponse<FightDto> response = new();

            try
            {
                Fight createdFight = _mapper.Map<Fight>(fight);

                _context.Add(createdFight);

                await _context.SaveChangesAsync();

                response.Success = true;
                response.Data = _mapper.Map<FightDto>(fight);
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

    }
}
