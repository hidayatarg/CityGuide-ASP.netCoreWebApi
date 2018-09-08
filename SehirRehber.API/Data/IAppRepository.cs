using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SehirRehber.API.Models;

namespace SehirRehber.API.Data
{
    public interface IAppRepository
    {
        //Constrain Add and Delete method should be classes
        void Add<T>(T entity) where T:class;

        void Delete<T>(T entity) where T : class;

        //deleting city
        void DeleteCityById(int cityId);

        //Pattern of Unity
        bool SaveAll();

        //list all cities
        List<City> GetCities();

        //list all photos
        List<Photo> GetPhotosByCity(int id);
       
        //single city
        City GetCityById(int cityId);

        //single photo
        Photo GetPhoto(int id);



    }
}
