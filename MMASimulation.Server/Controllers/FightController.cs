using Microsoft.AspNetCore.Mvc;
using MMASimulation.Server.Services.FightService;
using MMASimulation.Shared.Dtos.Fights;
using MMASimulation.Shared.Models.Utils;

namespace MMASimulation.Server.Controllers
{
    [Route("api/fights")]
    [ApiController]
    public class FightController : ControllerBase
    {

        private readonly IFightService _fightService;

        public FightController(IFightService fightService)
        {
            _fightService = fightService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<FightDto>>>> GetAllFights()
        {
            var result = await _fightService.GetAllFights();
            return Ok(result);
        }


        [HttpGet("{fightId}")]
        public async Task<ActionResult<ServiceResponse<FightDto>>> GetFightById(int fightId)
        {

            ServiceResponse<FightDto> response = await _fightService.GetFightById(fightId);

            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return NotFound(response.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<FightDto>>> CreateFight(FightCreateDto fight)
        {
            ServiceResponse<FightDto> response = await _fightService.CreateFight(fight);

            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        [HttpPut("update/{fightId}")]
        public async Task<IActionResult> UpdateFight(int fightId)
        {
            ServiceResponse<bool> response = await _fightService.UpdateFight(fightId);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

    }
}
