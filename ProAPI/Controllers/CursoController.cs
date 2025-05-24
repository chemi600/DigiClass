using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestAPI.Controllers.RestAPI.Controllers;
using RestAPI.Models.DTOs;
using RestAPI.Models.DTOs.CursoDTO;
using RestAPI.Models.Entity;
using RestAPI.Repository;
using RestAPI.Repository.IRepository;
using System.Security.Claims;

namespace RestAPI.Controllers
{
        
        [Route("api/[controller]")]
        [ApiController]
    public  class CursoController : ControllerBase
        {
            private readonly ICursoRepository _cursoRepository;
            private readonly IMapper _mapper;
            //protected readonly ILogger _logger;

        public CursoController(ICursoRepository repository, IMapper mapper /*ILogger logger*/)
            {
                _cursoRepository = repository;
                _mapper = mapper;
                //_logger = logger;
            }

        [HttpGet]
        //[Authorize(Roles = "profesor,estudiante")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var cursos = await _cursoRepository.GetAllAsync();
                var entities = _mapper.Map<List<CursoDTO>>(cursos);
                for (int i = 0; i < entities.Count; i++)
                {
                    entities[i].NombreProfesor = cursos.ElementAt(i).Profesor.Name;
                }
                return Ok(entities);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Error fetching data");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("Nuevos")]
        [Authorize(Roles = "profesor,estudiante")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllUnsubscribe()
        {
            try
            {
                string userId=User.FindFirstValue(ClaimTypes.NameIdentifier);
                var entities = _mapper.Map<List<CursoDTO>>(await _cursoRepository.GetAllUnsubscribeAsync(userId));
                return Ok(entities);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Error fetching data");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        




        [AllowAnonymous]
        [HttpGet("{id:int}", Name = "[controller]_GeCursoEntity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var entity = await _cursoRepository.GetAsync(id);
                if (entity == null) return NotFound();
                var mappedEntity = _mapper.Map<CursoDTO>(entity);
                mappedEntity.NombreProfesor = entity.Profesor.Name;
                return Ok(mappedEntity);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Error fetching data");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = "profesor")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateCurso createDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var entity = _mapper.Map<CursoEntity>(createDto);
                entity.IdProfesor = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _cursoRepository.CreateAsync(entity);

                var dto = _mapper.Map<CursoDTO>(entity);
                return CreatedAtRoute($"{ControllerContext.ActionDescriptor.ControllerName}_GeCursoEntity", new { id = entity.GetHashCode() }, dto);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Error creating data");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [Authorize(Roles = "profesor,estudiante")]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] CursoDTO dto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var entity = await _cursoRepository.GetAsync(id);
                if (entity == null) return NotFound();

                _mapper.Map(dto, entity);
                await _cursoRepository.UpdateAsync(entity);

                return Ok(_mapper.Map<CursoDTO>(entity));
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Error updating data");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [Authorize(Roles = "profesor,admin")]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var entity = await _cursoRepository.GetAsync(id);
                if (entity == null) return NotFound();

                await _cursoRepository.DeleteAsync(id);
                return Ok(true);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Error deleting data");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = "profesor,admin,estudiante")]
        [HttpDelete("Participantes/{id:int}/{userId:guid?}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteParticipante(int id, string? userId)
        {
            try
            {
                if(User.IsInRole("estudiante")){
                    userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                }
                var entity = await _cursoRepository.GetAsync(id);
                if (entity == null) return NotFound();

                await _cursoRepository.DeleteParticipanteAsync(id,userId);
                return Ok();
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Error deleting data");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
