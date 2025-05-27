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
using WebService.API.Services.Contracts;
using WebService.API.Services.IServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebService.API.Controllers
{
    public class Dates
    {
        public Guid Id { get; set; }
        public int years { get; set; }
        public int months { get; set; }
        public int days { get; set; }
        public int deger { get; set; }
    }
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class AllocationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<AllocationController> _logger;
        private ApiResponse _response;

        public AllocationController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<AllocationController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _response = new ApiResponse();
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Permissions.Allocation_View)]
        public async Task<IActionResult> GetAllocation() 
        {
            List<Dates> dates = new List<Dates>();
            
            //throw new Exception("Failed to retrieve data"); //for trying purpose
            var AllAllocation = await _unitOfWork.Allocation.GetAllAsync();
           

            if (AllAllocation is null)
            {
                _response.IsSuccess = false;
                _response.Result = null;
                _response.StatusCode = HttpStatusCode.NotFound;
            }
            else
            {
                
                _response.IsSuccess = true;
                _response.Result = AllAllocation;
            }
            return Ok(_response);
        }
        [HttpGet("Dates")]
        [Authorize(Permissions.Allocation_View)]
        public async Task<IActionResult> GetDates()
        {
            List<Dates> dates = new List<Dates>();

            //throw new Exception("Failed to retrieve data"); //for trying purpose
            var AllAllocation = await _unitOfWork.Allocation.GetAllAsync();
            var orderAllocation = AllAllocation.OrderBy(x => x.EntryTime);
            foreach (var allocation in orderAllocation)
            {
                Dates date = new Dates();
                var id = allocation.PersonId;


                var deger = 0;
                var kararDate = new DateTime(2022, 01, 11, 00, 00, 00);

                var date1 = allocation.EntryTime;
                var date2 = new DateTime?();


                if (allocation.ExitTime.ToString() == "1.01.1900 00:00:00" || allocation.ExitTime.ToString() == "1900-01-01 00:00:00.0000000" || allocation.ExitTime == null)
                {
                    date2 = DateTime.Today;
                }
                else
                {
                    date2 = allocation.ExitTime;
                }

                var difference = date2 - date1;
                foreach (var item in dates)
                {
                    if (item.Id == id)
                    {
                        deger = item.deger;
                        dates.Remove(item);
                        break;
                    }

                }
                if (allocation.AllocationMission == "Başkan" ||
                  allocation.AllocationMission == "2. Başkan" ||
                  allocation.AllocationMission == "Kurul Üyesi" ||
                  allocation.AllocationMission == "Başkan Yardımcısı" ||
                  allocation.AllocationMission == "Kurum Baş Danışmanı" ||
                  allocation.AllocationMission == "Baş Hukuk Müşaviri" ||
                  allocation.AllocationMission == "Daire Başkanı" ||
                  allocation.AllocationMission == "Daire Başkan Yardımcısı" ||
                  allocation.AllocationMission == "Özel Kalem Müdürü" ||
                  allocation.AllocationMission == "Hukuk Müşaviri" ||
                  allocation.AllocationMission == "Basın ve Halkla İlişkiler Müşaviri" || 
                  (allocation.AllocationMission == "Meslek Personeli - Başuzman" && allocation.EntryTime < kararDate ) ||
                  (allocation.AllocationMission == "Meslek Personeli - Uzman" && allocation.EntryTime < kararDate) || 
                  (allocation.AllocationMission == "Meslek Personeli - Uzman Yardımcısı" && allocation.EntryTime < kararDate))
                {
                    continue;
                }
                else
                {
                    deger += int.Parse(difference.Value.ToString("dd"));
                    date.Id = id;
                    date.deger = deger;
                    date.years = deger / 365;
                    date.months = (deger % 365) / 30;

                    if (date.months / 2 == 1)
                    {
                        date.days = ((deger % 365) % 30);
                        if (date.days > 3)
                        {
                            date.days = ((deger % 365) % 30) - 3;
                        }
                        else
                        {
                            date.days = 0;
                        }

                    }
                    else if (date.months / 2 == 2)
                    {
                        date.days = ((deger % 365) % 30);
                        if (date.days > 4)
                        {
                            date.days = ((deger % 365) % 30) - 4;
                        }
                        else
                        {
                            date.days = 0;
                        }
                    }
                    else if (date.months / 2 == 3)
                    {
                        date.days = ((deger % 365) % 30);
                        if (date.days > 5)
                        {
                            date.days = ((deger % 365) % 30) - 5;
                        }
                        else
                        {
                            date.days = 0;
                        }
                    }
                    else if (date.months / 2 == 4)
                    {
                        date.days = ((deger % 365) % 30);
                        if (date.days > 6)
                        {
                            date.days = ((deger % 365) % 30) - 6;
                        }
                        else
                        {
                            date.days = 0;
                        }
                    }
                    else if (date.months / 2 >= 5)
                    {
                        date.days = ((deger % 365) % 30);
                        if (date.days > 7)
                        {
                            date.days = ((deger % 365) % 30) - 4;
                        }
                        else
                        {
                            date.days = 0;
                        }
                    }
                    else if (date.months == 0 || date.months == 1)
                    {
                        date.days = ((deger % 365) % 30);

                    }
                    else
                    {
                        date.days = ((deger % 365) % 30);
                        if (date.days != 0)
                        {
                            date.days = ((deger % 365) % 30) - 2;
                        }
                        else
                        {
                            date.days = 0;
                        }
                    }
                    dates.Add(date);
                }
            }

            if (AllAllocation is null)
            {
                _response.IsSuccess = false;
                _response.Result = null;
                _response.StatusCode = HttpStatusCode.NotFound;
            }
            else
            {

                _response.IsSuccess = true;
                _response.Result = dates;
            }
            return Ok(_response);
        }


        [HttpGet("{id}")]
        [Authorize(Permissions.Allocation_ViewById)]
        public async Task<IActionResult> GetAllocationbyId(string id)
        {
            var AllocationById = await _unitOfWork.Allocation.GetByUserIdAsync(new Guid(id));
          

            if (AllocationById is null)
            {
                _response.IsSuccess = false;
                _response.Result = null;
                _response.StatusCode = HttpStatusCode.NotFound;
            }
            else
            {
                _response.IsSuccess = true;
                _response.Result = AllocationById;
            }
            return Ok(_response);
        }

        [Authorize(Permissions.Allocation_Edit)]
        [HttpPut]
        public async Task<IActionResult> PutAllocation([FromBody] UpdateAllocation Allocation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(TextResources.BadRequestNotValidParams);
            }
            var put = _mapper.Map<Allocation>(Allocation);

            var updatedAllocation = await _unitOfWork.Allocation.UpdateAsync(put);
            await _unitOfWork.SaveAsync();

            if (updatedAllocation is null)
            {
                _response.IsSuccess = false;
                _response.Result = null;
            }
            else
            {
                _response.IsSuccess = true;
                _response.Result = updatedAllocation;
            }
            _logger.LogDebug($"The response for the PutAllocation {JsonConvert.SerializeObject(_response)}");
            return Ok(_response);
        }

        [Authorize(Permissions.Allocation_Create)]
        [HttpPost]
        public async Task<IActionResult> PostAllocation([FromBody] CreateAllocation Allocation)
        {
            //_logger.LogDebug("Inside PostAllocation endpoint"); //logging
            if (!ModelState.IsValid)
            {
                return BadRequest(TextResources.BadRequestNotValidParams);
            }

            var post = _mapper.Map<Allocation>(Allocation);
            var person = await _unitOfWork.Person.GetByIdAsync(Allocation.PersonId);
            post.AllocationMission = person.Mission;

            var addedAllocation = await _unitOfWork.Allocation.AddAsync(post);
            var saveResult = await _unitOfWork.SaveAsync();

            if (saveResult == 0)
            {
                _response.IsSuccess = false;
                _response.Result = null;
            }
            else 
            {
                _response.IsSuccess = true;
                _response.Result = addedAllocation;
            }
            //_logger.LogDebug($"The response for the PostAllocation {JsonConvert.SerializeObject(_response)}");
            return Ok(_response);
        }
        [Authorize(Permissions.Allocation_Create)]
        [HttpPost("UpdateAndPostAllocation")]
        public async Task<IActionResult> UpdateAndPostAllocation([FromBody] UpdateAndPostAllocation Allocation)
        {
            //_logger.LogDebug("Inside PostAllocation endpoint"); //logging
            if (!ModelState.IsValid)
            {
                return BadRequest(TextResources.BadRequestNotValidParams);
            }

            var post = new Allocation { EntryTime = Allocation.EntryTime2, PersonId = Allocation.PersonId2, ExitTime = Allocation.ExitTime2, AllocationCount = Allocation.AllocationCount2, IsActive=Allocation.IsActive2};
            var put = new Allocation { EntryTime = Allocation.EntryTime, PersonId = Allocation.PersonId, ExitTime = Allocation.ExitTime, AllocationCount = Allocation.AllocationCount, Id = Allocation.Id, IsActive=Allocation.IsActive };

            var person = await _unitOfWork.Person.GetByIdAsync(Allocation.PersonId);
            post.AllocationMission = person.Mission;
            put.AllocationMission = person.Mission;
            var updatedAllocation = await _unitOfWork.Allocation.UpdateAsync(put);
            var addedAllocation = await _unitOfWork.Allocation.AddAsync(post);


            var saveResult = await _unitOfWork.SaveAsync();

            if (saveResult == 0)
            {
                _response.IsSuccess = false;
                _response.Result = null;
            }
            else
            {
                _response.IsSuccess = true;
                _response.Result = true;
            }
            _logger.LogDebug($"The response for the UpdateAndPostAllocation {JsonConvert.SerializeObject(_response)}");
            return Ok(_response);
        }
        [Authorize(Permissions.Allocation_Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAllocation(string id)
        {
            var success = await _unitOfWork.Allocation.DeleteAsync(new Guid(id));
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
            _logger.LogDebug($"The response for the DeleteAllocation {JsonConvert.SerializeObject(_response)}");
            return Ok(_response);
        }
        [Authorize(Permissions.Allocation_Exists)]
        private async Task<bool> IsAllocationExists(string id)
        {
            return await _unitOfWork.Allocation.Exists(new Guid(id));
        }
    }
}
