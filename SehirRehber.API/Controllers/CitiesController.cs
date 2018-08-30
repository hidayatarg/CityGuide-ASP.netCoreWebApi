using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SehirRehber.API.Data;
using SehirRehber.API.Dtos;
using SehirRehber.API.Models;

namespace SehirRehber.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Cities")]
    public class CitiesController : Controller
    {
        //will use
        private IAppRepository _appRepository;
        private IMapper _mapper;

        public CitiesController(IAppRepository appRepository, IMapper mapper)
        {
            _appRepository = appRepository;
            _mapper = mapper;
        }

        public ActionResult GetCities()
        {
            //return all cities
            // var cities = _appRepository.GetCities();

            //return only city names
            // var cities = _appRepository.GetCities().Select(c=>c.Name);

            /*return information from DTOs
            var cities = _appRepository.GetCities().Select(c => new CityForListDto
            {
                Description = c.Description,
                Name = c.Name,
                Id = c.Id,
                PhotoUrl = c.Photos.SingleOrDefault(p => p.IsMain == true).Url

            }).ToList();*/

            //in place we use automappers
            var cities = _appRepository.GetCities();
            var citiesToReturn = _mapper.Map<List<CityForListDto>>(cities);
            return Ok(citiesToReturn);
        }

        [HttpPost]
        [Route("add")]
        public ActionResult Add([FromBody] City city)
        {
            _appRepository.Add(city);
            
            //Unit of Work Desing Pattern
            _appRepository.SaveAll();

            return Ok(city);

        }

        [HttpGet]
        [Route("detail")]
        public ActionResult GetCityById(int id)
        {
            var city = _appRepository.GetCityById(id);
            var cityToReturn = _mapper.Map<CityForDto>(city);
            return Ok(cityToReturn);
        }
    }
}