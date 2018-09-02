using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SehirRehber.API.Dtos;
using SehirRehber.API.Models;

namespace SehirRehber.API.Helper
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            //making city to cityforListDto
            CreateMap<City, CityForListDto>()
                .ForMember(dest => dest.PhotoUrl, opt =>
                {
                    opt.MapFrom(src=>src.Photos.FirstOrDefault(p=>p.IsMain).Url);
                });

            CreateMap<City, CityForDto>();
            CreateMap<Photo, PhotoForCreationDto>();
            CreateMap<PhotoForCreationDto, Photo>();
        }
    }
}
