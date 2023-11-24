using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Models.Domain;
using MovieStore.Repositories.Abstract;
using MovieStore.Repositories.Implement;

namespace MovieStore.Controllers
{
    [Authorize]
    public class GenreController : Controller
    {
        private readonly IGenreService GenreService;
        public GenreController( IGenreService GenreService)
        {
            this.GenreService = GenreService;
            
        }
        public IActionResult Add()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Add(Genre model)
        {

            if(!ModelState.IsValid) { return View(model); }
            var result=GenreService.Add(model);

            if(result)
            {
                TempData["msg"] = "Added Succefully";
                return RedirectToAction(nameof(Add));
            }
            else
            {
                TempData["msg"] = "error occured";

                return View();
            }

        }



        public IActionResult Edit(int id)
        {
            var data=GenreService.GetById(id);
            return View(data);
        }


        [HttpPost]
        public IActionResult Update(Genre model)
        {

            if (!ModelState.IsValid) { return View(model); }
            var result = GenreService.Update(model);

            if (result)
            {
                TempData["msg"] = "Added Succefully";
                return RedirectToAction(nameof(GenreList));
            }
            else
            {
                TempData["msg"] = "error occured";

                return View(model);
            }

        }


        public IActionResult GenreList()
        {
            var data=GenreService.List().ToList();
            return View (data);
        }



        public IActionResult Delete(int id) 
        {
            var result=GenreService.Delete(id);
            return RedirectToAction(nameof(GenreList));
        }



    }
}
