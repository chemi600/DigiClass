
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestAPI.Models.DTOs;
using RestAPI.Models.DTOs.CursoDTO;
using RestAPI.Models.DTOs.UserDto;
using RestAPI.Repository;
using RestAPI.Repository.IRepository;
using System.Net;
using System.Security.Claims;

namespace RestAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        protected ResponseApi _reponseApi;
        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _reponseApi = new ResponseApi();
            _mapper = mapper;
        }

        [Authorize(Roles = "estudiante,profesor")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult GetUsers()
        {
            var userList = _userRepository.GetUsers();
            var userListDto = new List<UserDto>();

            foreach (var user in userList)
            {
                userListDto.Add(_mapper.Map<UserDto>(user));
            }

            return Ok(userListDto);
        }

        [HttpGet("MisCursos")]
        [Authorize(Roles = "profesor,estudiante")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllMyCourse()
        {
            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var entities = _mapper.Map<List<CursoDTO>>(await _userRepository.GetAllMyCourseAsync(userId));
                return Ok(entities);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Error fetching data");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("Participantes/{id:int}")]
        [Authorize(Roles = "profesor,estudiante")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllEstudents(int id)
        {
            try
            {
                
                var entities = _mapper.Map<List<UserDto>>(await _userRepository.GetAllEstudents(id));
                return Ok(entities);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Error fetching data");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = "estudiante")]
        [HttpPost("Apuntarse/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult Apuntarse(int id)
        {
            _userRepository.Apuntarse(id, User.FindFirstValue(ClaimTypes.NameIdentifier));

            return Ok(true);
        }

        //[Authorize(Roles = "admin")]
        //[HttpGet("{userId:int}", Name = "GetUser")]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public IActionResult GetUser(string userId)
        //{
        //    var user = _userRepository.GetUser(userId);
        //    if (user == null) { return NotFound(); }

        //    return Ok(_mapper.Map<CategoryDto>(user));
        //}

        [Authorize(Roles = "profesor,admin")]
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var entity =  _userRepository.GetUser(id);
                if (entity == null) return NotFound();

                await _userRepository.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register(UserRegistrationDto userRegistrationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { error = "Incorrect Input", message = ModelState });
            }

            if (!_userRepository.IsUniqueUser(userRegistrationDto.UserName))
            {
                _reponseApi.StatusCode = HttpStatusCode.BadRequest;
                _reponseApi.IsSuccess = false;
                _reponseApi.ErrorMessages.Add("Username already exists");
                return BadRequest();
            }

            var newUser = await _userRepository.Register(userRegistrationDto);
            if (newUser == null)
            {
                _reponseApi.StatusCode = HttpStatusCode.BadRequest;
                _reponseApi.IsSuccess = false;
                _reponseApi.ErrorMessages.Add("Error registering the user");
                return BadRequest();
            }

            _reponseApi.StatusCode = HttpStatusCode.OK;
            _reponseApi.IsSuccess = true;
            return Ok(_reponseApi);
        }


        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            var responseLogin = await _userRepository.Login(userLoginDto);

            if (string.IsNullOrEmpty(responseLogin.Token))
            {
                _reponseApi.StatusCode = HttpStatusCode.BadRequest;
                _reponseApi.IsSuccess = false;
                _reponseApi.ErrorMessages.Add("Incorrect user and password");
                return BadRequest(_reponseApi);
            }

            _reponseApi.StatusCode = HttpStatusCode.OK;
            _reponseApi.IsSuccess = true;
            _reponseApi.Result = responseLogin;
            return Ok(_reponseApi);
        }
    }
}
