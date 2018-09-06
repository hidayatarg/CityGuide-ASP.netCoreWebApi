using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using SehirRehber.API.Data;
using SehirRehber.API.Dtos;
using SehirRehber.API.Helper;
using SehirRehber.API.Models;

namespace SehirRehber.API.Controllers
{
    [Produces("application/json")]
    [Route("api/cities/{cityId}/photos")]
    public class PhotosController : Controller
    {
        private IAppRepository _appRepository;
        private IMapper _mapper;
        private IOptions<CloudinarySettings> _cloudinaryConfig;

        // Set cloudinary
        private Cloudinary _cloudinary;

        public PhotosController(IAppRepository appRepository, IMapper mapper,
            IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _appRepository = appRepository;
            _mapper = mapper;
            _cloudinaryConfig = cloudinaryConfig;

            // Activate cloudinary account
            Account account = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret);

            // Start cloudinary according to the upper account
            _cloudinary = new Cloudinary(account);
        }

        // For sending file we need to use FromForm 

        [HttpPost]
        public ActionResult AddPhotoForCity(int cityId, [FromForm] PhotoForCreationDto photoForCreationDto)
        {
            var city = _appRepository.GetCityById(cityId);

            // Data Templete if city is null
            if (city == null)
            {
                return BadRequest("Could not find the city");
            }

            // Find the current signed In User
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            // ***
            // If the user want to add a photo to a different user city
            // Everyone can add photo to his city
            if (currentUserId != city.UserId)
            {
                return Unauthorized();
            }

            // Reading File
            var file = photoForCreationDto.File;

            var uploadResult = new ImageUploadResult();

            // there is a file
            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    // uploading informations
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(file.Name, stream)
                    };

                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }

            photoForCreationDto.Url = uploadResult.Uri.ToString();
            photoForCreationDto.PublicId = uploadResult.PublicId;

            // DataContext dbset we have Photo but we dont have photo type
            // Help>AutoMapper we create a mapping for photo to photoForCreationDto
            var photo = _mapper.Map<Photo>(photoForCreationDto);
            photo.City = city;

            // If there is no photo first photo is main
            if (!city.Photos.Any(p => p.IsMain))
            {
                photo.IsMain = true;
            }

            city.Photos.Add(photo);

            // If photo is added successfully
            if (_appRepository.SaveAll())
            {
                // Reverse mapping from PhotoForReturnDto to Photo
                var photoToReturn = _mapper.Map<PhotoForReturnDto>(photo);

                return CreatedAtRoute("GetPhoto", new {id = photo.Id}, photoToReturn);
            }

            return BadRequest("Could not add the photo");
        }

        [HttpGet("{id}",Name = "GetPhoto")]
        public ActionResult GetPhoto(int id)
        {
            var photoFromDb = _appRepository.GetPhoto(id);
            var photo = _mapper.Map<PhotoForReturnDto>(photoFromDb);

            return Ok(photo);
        }
    }
}