using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SehirRehber.API.Models;

namespace SehirRehber.API.Data
{
    public class AppRepository:IAppRepository
    {
        //injection
        private DataContext _context;

        public AppRepository(DataContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
            
        }


        public void DeleteCityById(int cityId)
        {
            var cityInDb = _context.Cities.FirstOrDefault(c => c.Id == cityId);
            if (cityInDb == null)
            {
                throw new InvalidOperationException();
            }
            _context.Cities.Remove(cityInDb);
            _context.SaveChanges();
        }


        public bool SaveAll()
        {
            //if there is a record save all
            return _context.SaveChanges() > 0;
        }

        public List<City> GetCities()
        {
            var cities = _context.Cities
                .Include(c=>c.Photos).ToList();
            return cities;
        }

        public List<Photo> GetPhotosByCity(int id)
        {
            var photos = _context.Photos.Where(p => p.CityId == id).ToList();
            return photos;
        }

        public City GetCityById(int cityId)
        {
            var city = _context.Cities.Include(c => c.Photos)
                 .FirstOrDefault(c => c.Id == cityId);
            return city;
        }

        public Photo GetPhoto(int id)
        {
            var photo = _context.Photos.FirstOrDefault(p => p.Id == id);
            return photo;
        }

        
    }
}
