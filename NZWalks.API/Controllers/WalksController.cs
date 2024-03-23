using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWalkRepository _walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this._mapper = mapper;
            this._walkRepository = walkRepository;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalksRequestDto addWalksRequestDto)
        {
            var walkDomainModel = _mapper.Map<Walk>(addWalksRequestDto);

            await _walkRepository.CreateWalkAsync(walkDomainModel);

            return Ok(_mapper.Map<WalkDto>(walkDomainModel));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery, 
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending, 
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            var walksDomainModel = await _walkRepository.GetAllWalkAsync(filterOn, filterQuery, sortBy, 
                isAscending ?? true, pageNumber, pageSize);
            return Ok(_mapper.Map<List<WalkDto>>(walksDomainModel));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModel = await _walkRepository.GetWalkByIdAsync(id);

            if(walkDomainModel is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<WalkDto>(walkDomainModel));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalkRequestDto updateWalkRequestDto)
        {

                var walkDomainModel = _mapper.Map<Walk>(updateWalkRequestDto);

                walkDomainModel = await _walkRepository.UpdateWalkAsync(id, walkDomainModel);

                if (walkDomainModel is null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<WalkDto>(walkDomainModel));
            
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deleteWalkDomainModel = await _walkRepository.DeleteWalkAsync(id);

            if(deleteWalkDomainModel is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<WalkDto>(deleteWalkDomainModel));
        }
    }
}
