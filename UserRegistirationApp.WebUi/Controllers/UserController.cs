using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RabbitMq.Common;
using System.Collections.Generic;
using UserRegistirationApp.Data.Abstract;
using UserRegistirationApp.Entity.Entities;
using UserRegistirationApp.WebUi.IntegrationEvents;
using UserRegistirationApp.WebUi.Models.DTOs;

namespace UserRegistirationApp.WebUi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IRabbitMqProducer<UserIntegrationEvent> _mqProducer;
        public UserController(IUserRepository userRepository,
                                IMapper mapper,
                                IRabbitMqProducer<UserIntegrationEvent> mqProducer)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _mqProducer = mqProducer;
        }

        /// <summary>
        /// Get All Users 
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet]
        public IActionResult GetUsers()
        {

            var users = _userRepository.GetAll();

            var usersDTO = new List<UserDTO>();

            foreach (var user in users)
                usersDTO.Add(_mapper.Map<UserDTO>(user));

            _mqProducer.Publish(new UserIntegrationEvent() { Message = "WebApi get all users" });

            return Ok(usersDTO);
        }

        /// <summary>
        /// Get Individual User
        /// </summary>
        /// <param name="userId">The Id of User</param>
        /// <returns></returns>
        ///         
        [HttpGet("{userId:int}", Name = "GetUser")]
        public IActionResult GetUser(int userId)
        {
            User user = _userRepository.GetById(userId);

            if (user == null)
            {
                return NotFound();
            }

            _mqProducer.Publish(new UserIntegrationEvent()
            {
                Message = $"WebApi get user with id:{userId} and name:{user.Name}"
            });

            return Ok(_mapper.Map<UserDTO>(user));
        }

        /// <summary>
        /// Create User
        /// </summary>
        /// <param name="userDTO">User</param>
        /// <returns></returns>
        ///    
        [HttpPost]
        public IActionResult Create([FromBody] UserDTO userDTO)
        {
            if (userDTO == null)
            {
                return BadRequest();
            }

            if (_userRepository.ExsistsByUserName(userDTO.Name))
            {
                ModelState.AddModelError("", "Username already exsists");
                return StatusCode(404, ModelState);
            }

            User user = _mapper.Map<User>(userDTO);

            if (!_userRepository.Create(user))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {user.Name} ");
                return StatusCode(500, ModelState);
            }
            _mqProducer.Publish(new UserIntegrationEvent()
            {
                Message = $"WebApi created {user.Name}"
            });
            return CreatedAtRoute("GetUser", new { userId = user.Id }, user);

        }


        /// <summary>
        /// Update User
        /// </summary>
        /// <returns></returns>
        ///  
        [HttpPatch]
        public IActionResult Update(UserDTO userDTO)
        {
            if (userDTO == null || userDTO.Id == 0)
            {
                return BadRequest(ModelState);
            }

            User user = _mapper.Map<User>(userDTO);

            user.Id = userDTO.Id;

            if (!_userRepository.Update(user))
            {
                ModelState.AddModelError("", $"Something went wrong saving the record {user.Name}");
                return StatusCode(500, ModelState);
            }

            _mqProducer.Publish(new UserIntegrationEvent()
            {
                Message = $"WebApi updated {user.Name}"
            });

            return NoContent();
        }


        /// <summary>
        /// Delete User
        /// </summary>
        /// <param name="userId">The Id of User</param>
        /// <returns></returns>
        ///   
        [HttpDelete("{userId:int}", Name = "DeleteUser")]
        public IActionResult Delete(int userId)
        {
            if (!_userRepository.ExsistsById(userId))
            {
                return NotFound();
            }

            if (!_userRepository.Delete(userId))
            {
                ModelState.AddModelError("", $"Something went wrong deleting the record with Id: {userId}");
                return StatusCode(500, ModelState);
            }

            _mqProducer.Publish(new UserIntegrationEvent()
            {
                Message = $"WebApi deleted user with id: {userId}"
            });

            return NoContent();
        }
    }
}
