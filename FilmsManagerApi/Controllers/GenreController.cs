﻿using FilmsManagerApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.Classes;

namespace FilmsManagerApi.Controllers
{
    [Route("api/[controller]")]
    public class GenreController : Controller
    {
        private readonly IRepository<GenreModel, GenreResponse> _genreRepository;

        public GenreController(IRepository<GenreModel, GenreResponse> genreRepository)
        {
            _genreRepository = genreRepository;
        }

        [HttpGet]
        public IActionResult List() => Ok(_genreRepository.All);

        //[HttpPost]
        //public IActionResult Create([FromBody] MovieModel item)
        //{
        //    if (item == null || !ModelState.IsValid)
        //        return BadRequest(ErrorCodeEnum.ModelNameAndDescriptionRequired.ToString());

        //    try
        //    {
        //        if (_genreRepository.DoesItemExist(item.Id))
        //            return StatusCode(StatusCodes.Status409Conflict, ErrorCodeEnum.ModelIdInUse.ToString());

        //        item.Id = GenerateId();
        //        _genreRepository.Insert(item);
        //    }
        //    catch (Exception)
        //    {
        //        // TODO add logger
        //        return BadRequest(ErrorCodeEnum.CouldNotCreateItem.ToString());
        //    }

        //    return Ok(item);
        //}

        //[HttpPut]
        //public IActionResult Modify([FromBody] GenreModel item)
        //{
        //    if (item == null || !ModelState.IsValid)
        //        return BadRequest(ErrorCodeEnum.ModelNameAndDescriptionRequired.ToString());

        //    try
        //    {
        //        var foundItem = _genreRepository.Find(item.Id);

        //        if (foundItem == null)
        //            return NotFound(ErrorCodeEnum.RecordNotFound.ToString());

        //        _genreRepository.Update(item);
        //    }
        //    catch (Exception)
        //    {
        //        // TODO add logger
        //        return BadRequest(ErrorCodeEnum.CouldNotUpdateItem.ToString());
        //    }

        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        //public IActionResult Delete([FromRoute] string id)
        //{
        //    try
        //    {
        //        var item = _genreRepository.Find(id);

        //        if (item == null)
        //            return NotFound(ErrorCodeEnum.RecordNotFound.ToString());

        //        _genreRepository.Delete(id);
        //    }
        //    catch (Exception)
        //    {
        //        // TODO add logger
        //        return BadRequest(ErrorCodeEnum.CouldNotDeleteItem.ToString());
        //    }

        //    return NoContent();
        //}

        //private string GenerateId()
        //{
        //    return Thread.CurrentThread.ManagedThreadId.ToString() + System.DateTime.Now.ToString();
        //}

    }
}
