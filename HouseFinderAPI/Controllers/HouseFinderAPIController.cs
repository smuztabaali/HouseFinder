using AutoMapper;
using HouseFinderAPI.Models.Dto;
using HouseFinderAPI.Models;
using HouseFinderAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Asp.Versioning;

namespace HouseFinderAPI.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion(1.0)]
    [ApiVersion(2.0)]
    [ApiController]
    public class HouseFinderAPIController : ControllerBase
    {
        private APIResponse response;
        private readonly IHouseRepository dbHouse;
        private readonly IMapper mapper;

        public HouseFinderAPIController(IHouseRepository dbHouse, IMapper mapper)
        {

            this.dbHouse = dbHouse;
            this.mapper = mapper;
            this.response = new APIResponse();
        }
        [HttpGet]
        [ResponseCache(CacheProfileName ="Default30")]
        [AllowAnonymous]
        [MapToApiVersion("1.0")]
        [Authorize(Roles = "admin,user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetHouses([FromQuery(Name ="Number of Bedroom")]int? bedRoomNo,
            [FromQuery(Name ="City,District or Division")]string? search)
        {
           

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                IEnumerable<House> houses;

                if (bedRoomNo>0)
                {
                    houses = await dbHouse.GetAll(x=>x.BedNumber == bedRoomNo);
                }
                else
                {
                    houses = await dbHouse.GetAll();
                }

                if (!string.IsNullOrEmpty(search))
                {
                    houses = houses.Where(x => x.City.ToLower().Contains(search.ToLower()) ||
                        x.District.ToLower().Contains(search.ToLower()) ||
                        x.Division.ToLower().Contains(search.ToLower()));
                    
                }

                if (houses == null)
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.ErrorMessage = new List<string>
                    {
                        "No data found."
                    };
                    return NotFound(response);
                }
                response.Result = mapper.Map<List<HouseGetValuesDto>>(houses);
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string> { ex.ToString() };
            }
            

            return response;
        }
        [HttpGet]
        [MapToApiVersion("2.0")]
        public IEnumerable<string> Get()
        {
            return new string[] {"List1","List2" };
        }

        [HttpGet("{id:int}", Name = "GetHouse")]
        [AllowAnonymous]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetHouse(int id)
        {
           
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (id == 0)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.IsSuccess = false;
                    response.ErrorMessage = new List<string> { "Cannot be 0" };
                    return BadRequest(response);

                }
                var house = await dbHouse.Get(x => x.HouseId == id);
                if (house == null)
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.IsSuccess = false;
                    response.ErrorMessage = new List<string>
                    {
                        "No data found."
                    };
                    return NotFound(response);
                }
                response.StatusCode = HttpStatusCode.OK;
                response.Result = mapper.Map<HouseGetValuesDto>(house);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = new List<string>
                {
                    ex.ToString()
                };

            }
            return Ok(response);
        }

        [HttpPost]
        [AllowAnonymous]
        [Authorize(Roles = "admin,user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]

        public async Task<ActionResult<APIResponse>> CreateHouse([FromBody] HouseDto house)
        {
           
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (house == null)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.IsSuccess = false;
                    return BadRequest(response);
                }


                House model = mapper.Map<House>(house);
                await dbHouse.Create(model);

                response.StatusCode = HttpStatusCode.Created;
                response.Result = model;
                return CreatedAtRoute("GetHouse", new { id = model.HouseId }, response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = new List<string>
                {
                    ex.ToString()
                };
            }

            return BadRequest(response);


        }


        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [AllowAnonymous]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public async Task<ActionResult<APIResponse>> DeleteHouse(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (id == 0)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.IsSuccess = false;
                    return BadRequest(response);
                }
                var house = await dbHouse.Get(x => x.HouseId == id);
                if (house == null)
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.IsSuccess = false;
                    response.ErrorMessage = new List<string>
                    {
                        "Data doesn't exist."
                    };
                    return NotFound(response);
                }
                await dbHouse.Remove(house);
                response.StatusCode = HttpStatusCode.NoContent;
                response.IsSuccess = true;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = new List<string>(){
                    ex.ToString()
                };
            }
            return Ok(response);
        }

        [HttpPut("{id:int}", Name = "UpdateHouse")]
        [AllowAnonymous]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<APIResponse>> UpdateHouse(int id, [FromBody] HouseDto house)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (house.HouseId != id || house == null)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.IsSuccess = false;
                    return BadRequest(response);
                }
                var list = await dbHouse.Get(x => x.HouseId == id);
                if (list != null)
                {

                    list.HouseName = house.HouseName;
                    list.HouseNoOrRoad = house.HouseNoOrRoad;
                    list.Division = house.Division;
                    list.District = house.District;
                    list.City = house.City;
                    list.BedNumber = house.BedNumber;
                    list.BalconyNumber = house.BalconyNumber;
                    list.Rent = house.Rent;
                    list.FloorNumber = house.FloorNumber;
                    list.HasLift = house.HasLift;
                    list.IsAvailable = house.IsAvailable;
                    list.ContactNumber = house.ContactNumber;
                    list.LastUpdatedAt = DateTime.Now.Date;
                    await dbHouse.Save();

                }
                //context.Entry(model).State = EntityState.Modified;

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string> { ex.ToString() };
            }
            return NoContent();
        }

    }
}
