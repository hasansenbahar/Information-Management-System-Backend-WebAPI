using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.Net;
using WebService.API.Authorization;
using WebService.API.Constants;
using WebService.API.Data.Entity;
using WebService.API.Models;
using WebService.API.Models.AllocationModels;
using WebService.API.Models.PersonModels;
using WebService.API.Services.Contracts;
using WebService.API.Services.IServices;

namespace WebService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class PersonController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<PersonController> _logger;
        private ApiResponse _response;

        public PersonController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<PersonController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _response = new ApiResponse();
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Permissions.Person_View)]
        public async Task<IActionResult> GetPerson() 
        {
            //throw new Exception("Failed to retrieve data"); //for trying purpose
            var AllPerson = await _unitOfWork.Person.GetAllAsync();
            var persons = AllPerson.OrderBy(x => x.Name);
        
            if (AllPerson is null)
            {
                _response.IsSuccess = false;
                _response.Result = null;
                _response.StatusCode = HttpStatusCode.NotFound;
            }
            else
            {
                _response.IsSuccess = true;
                _response.Result = persons;
            }
            return Ok(_response);
        }

        [HttpGet("{id}")]
        [Authorize(Permissions.Person_ViewById)]
        public async Task<IActionResult> GetPersonbyId(string id)
        {
            var PersonById = await _unitOfWork.Person.GetByIdAsync(new Guid(id));

            if (PersonById is null)
            {
                _response.IsSuccess = false;
                _response.Result = null;
                _response.StatusCode = HttpStatusCode.NotFound;
            }
            else
            {
                _response.IsSuccess = true;
                _response.Result = PersonById;
            }
            return Ok(_response);
        }

        [Authorize(Permissions.Person_Edit)]
        [HttpPut()]
        public async Task<IActionResult> PutPerson(UpdatePerson Person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(TextResources.BadRequestNotValidParams);
            }
            var put = _mapper.Map<Person>(Person);

            var prePerson = await _unitOfWork.Person.GetByIdAsync(Person.Id);
            var personsAllocations =  _unitOfWork.Allocation.GetItems(x => x.PersonId == Person.Id);
          
            var lastAllocation = personsAllocations.OrderByDescending(x => x.AllocationCount)?.FirstOrDefault();
         
            if (lastAllocation != null && Person.MissionChangeDate != null) //unvan görev tahsise mi döndü o da kontrol edilecek
            {
                if (
                       TextResources.ReignMissionList.Contains(Person.Mission)  && TextResources.StandartMissionList.Contains(prePerson.Mission)
                    || TextResources.ReignMissionList.Contains(Person.Mission) && TextResources.PrivateMissionList.Contains(prePerson.Mission)
                    || TextResources.StandartMissionList.Contains(Person.Mission) && TextResources.ReignMissionList.Contains(prePerson.Mission)
                    || TextResources.StandartMissionList.Contains(Person.Mission) && TextResources.PrivateMissionList.Contains(prePerson.Mission)
                    || TextResources.PrivateMissionList.Contains(Person.Mission) && TextResources.StandartMissionList.Contains(prePerson.Mission)
                    || TextResources.PrivateMissionList.Contains(Person.Mission) && TextResources.ReignMissionList.Contains(prePerson.Mission)
                    )
                {
                    Allocation al = new Allocation { EntryTime = Person.MissionChangeDate, AllocationCount = lastAllocation.AllocationCount + 1, PersonId = lastAllocation.PersonId, AllocationMission = Person.Mission, ExitTime= DateTime.Now };
                    var newAddedAllocation = _unitOfWork.Allocation.AddAsync(al);
                    lastAllocation.ExitTime = Person.MissionChangeDate;
                    await _unitOfWork.Allocation.UpdateAsync(lastAllocation);
                }
            }

            var updatedPerson = await _unitOfWork.Person.UpdateAsync(put);

            await _unitOfWork.SaveAsync();

            if (updatedPerson is null)
            {
                _response.IsSuccess = false;
                _response.Result = null;
            }
            else
            {
                _response.IsSuccess = true;
                _response.Result = updatedPerson;
            }
            _logger.LogDebug($"The response for the PutPerson {JsonConvert.SerializeObject(_response)}");
            return Ok(_response);
        }

        [Authorize(Permissions.Person_Create)]
        [HttpPost]
        public async Task<IActionResult> PostPerson([FromBody] CreatePerson Person)
        {
            //_logger.LogDebug("Inside PostPerson endpoint"); //logging
            if (!ModelState.IsValid)
            {
                return BadRequest(TextResources.BadRequestNotValidParams);
            }

            var post = _mapper.Map<Person>(Person);

            var addedPerson = await _unitOfWork.Person.AddAsync(post);
            var saveResult = await _unitOfWork.SaveAsync();

            if (saveResult == 0)
            {
                _response.IsSuccess = false;
                _response.Result = null;
            }
            else
            {
                _response.IsSuccess = true;
                _response.Result = addedPerson;
            }
            _logger.LogDebug($"The response for the PostPerson {JsonConvert.SerializeObject(_response)}");
            return Ok(_response);
        }

        [Authorize(Permissions.Person_Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(string id)
        {
            var success = await _unitOfWork.Person.DeleteAsync(new Guid(id));
            var saveResult = await _unitOfWork.SaveAsync();
            if (!success)
            {
                _response.IsSuccess = false;
                _response.Result = null;
            }
            else
            {
                _response.IsSuccess = true;
                _response.Result = true;
            }
            _logger.LogDebug($"The response for the DeletePerson {JsonConvert.SerializeObject(_response)}");
            return Ok(_response);
        }
        [Authorize(Permissions.Person_Exists)]
        private async Task<bool> IsPersonExists(string id)
        {
            return await _unitOfWork.Person.Exists(new Guid(id));
        }
    }
}
