﻿using Microsoft.AspNetCore.Mvc;
using MMASimulation.Server.Services.FighterService;
using MMASimulation.Shared.Dtos.Fighters;
using MMASimulation.Shared.Models.Utils;

namespace MMASimulation.Server.Controllers
{
    [Route("api/fighters")]
    [ApiController]
    public class FighterController : ControllerBase
    {

        private readonly IFighterService _fighterService;

        public FighterController(IFighterService fighterService)
        {
            _fighterService = fighterService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<FighterDto>>>> GetAllFighters()
        {
            var result = await _fighterService.GetAllFighters();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<FighterDto>>> CreateFighter(FighterCreateDto fighter)
        {
            ServiceResponse<FighterDto> response = await _fighterService.CreateFighter(fighter);

            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

    }
}
