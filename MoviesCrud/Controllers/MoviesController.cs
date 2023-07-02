using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesCrud.Models;
using MoviesCrud.ViewModels;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesCrud.Controllers
{
    public class MoviesController : Controller
    {
        private readonly Context _context;
        private readonly IToastNotification _toastNotification;
        public MoviesController(Context context , IToastNotification toastNotification)
        {
            _context = context;
            _toastNotification = toastNotification;
        }
        public IActionResult Index()
        {
            var Movies = _context?.Movies.ToList();
            return View(Movies);
        }

        public IActionResult Create()
        {
            var Data = new MovieVM 
            {
                Gunres=_context.Gunres.ToList()
            };
            return View(Data);
        }

        [HttpPost]

        public IActionResult Create(MovieVM Model)
        {
            var files = Request.Form.Files;
            if (!files.Any())
            {
                ModelState.AddModelError("Poster", "Poster is required!!");
                Model.Gunres = _context.Gunres.ToList();
                return View(Model);
            }
            using var dataStream = new MemoryStream();
            var poster = files.FirstOrDefault();
            poster.CopyTo(dataStream);
            Model.Poster = dataStream.ToArray();
            
           

            var Data = new Movie
            {
                GunreId=Model.GunreId,
                Title=Model.Title,
                Year=Model.Year,
                Rate=Model.Rate,
                StoryLine=Model.StoryLine,
                Poster= dataStream.ToArray()
                
            };
            _context.Movies.Add(Data);
            _context.SaveChanges();
            _toastNotification.AddSuccessToastMessage("Movie Added successful !");
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var movie = _context.Movies.Include(m=>m.Gunre).Select(a=>a).Where(a=>a.MovieId==id).ToList();
            
            return View(movie);
        }

        public IActionResult Edit(int id)
        {
            var movie = _context.Movies.Find(id);
            var Data = new MovieVM
            {
                Id=movie.MovieId,
                Title=movie.Title,
                GunreId=movie.GunreId,
                Poster=movie.Poster,
                Rate=movie.Rate,
                StoryLine=movie.StoryLine,
                Year=movie.Year,
                Gunres = _context.Gunres.ToList()
            };

            return View(Data);
        }
        [HttpPost]
        public IActionResult Edit(MovieVM Model)

        {
          
            var movie = _context.Movies.Find(Model.Id);

            var files = Request.Form.Files;

            if (files.Any())
            {
                using var dataStream = new MemoryStream();
                var poster = files.FirstOrDefault();
                poster.CopyTo(dataStream);
                movie.Poster = dataStream.ToArray();


            }
            

            movie.Title = Model.Title;
            movie.GunreId = Model.GunreId;
            movie.Rate = Model.Rate;
            movie.Year = Model.Year;
            movie.StoryLine = Model.StoryLine;
            _context.SaveChanges();
            _toastNotification.AddSuccessToastMessage("Movie Updated successful !");
            return RedirectToAction("Index");
           
        }

        public IActionResult Delete(int id)
        {
            var movie = _context.Movies.Find(id);
            _context.Movies.Remove(movie);
            _context.SaveChanges();

            return Ok();
        }

    }
}
