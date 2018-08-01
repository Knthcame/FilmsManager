using System;
using System.Collections.Generic;
using System.Threading;
using FilmsManagerApi.Enums;
using FilmsManagerApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Classes;

namespace FilmsManagerApi.Controllers
{
	[Route("api/[controller]")]
	public class MovieController : Controller
	{
		private readonly IRepository<MovieModel, IEnumerable<MovieModel>> _movieRepository;
        private readonly IRepository<GenreModel, GenreResponse> _genreRepository;

        public MovieController(IRepository<MovieModel, IEnumerable<MovieModel>> movieRepository, IRepository<GenreModel, GenreResponse> genreRepository)
		{
			_movieRepository = movieRepository;
            _genreRepository = genreRepository;
		}

		[HttpGet]
		public IActionResult List() => Ok(_movieRepository.All);

		[HttpPost]
		public IActionResult Create([FromBody] MovieModel item)
		{
			if (item == null || !ModelState.IsValid)
				return BadRequest(ErrorCodeEnum.ToDoItemNameAndDescriptionRequired.ToString());

			try
			{
				if (_movieRepository.DoesItemExist(item.Id))
					return StatusCode(StatusCodes.Status409Conflict, ErrorCodeEnum.ToDoItemIdInUse.ToString());

				item.Id = GenerateId();
				_movieRepository.Insert(item);
			}
			catch (Exception)
			{
				// TODO add logger
				return BadRequest(ErrorCodeEnum.CouldNotCreateItem.ToString());
			}

			return Ok(item);
		}

		[HttpPut]
		public IActionResult Modify([FromBody] MovieModel item)
		{
			if (item == null || !ModelState.IsValid)
				return BadRequest(ErrorCodeEnum.ToDoItemNameAndDescriptionRequired.ToString());

			try
			{
				var foundItem = _movieRepository.Find(item.Id);

				if (foundItem == null)
					return NotFound(ErrorCodeEnum.RecordNotFound.ToString());

				_movieRepository.Update(item);
			}
			catch (Exception)
			{
				// TODO add logger
				return BadRequest(ErrorCodeEnum.CouldNotUpdateItem.ToString());
			}

			return NoContent();
		}

		[HttpDelete("{id}")]
		public IActionResult Delete([FromRoute] string id)
		{
			try
			{
				var item = _movieRepository.Find(id);

				if (item == null)
					return NotFound(ErrorCodeEnum.RecordNotFound.ToString());

				_movieRepository.Delete(id);
			}
			catch (Exception)
			{
				// TODO add logger
				return BadRequest(ErrorCodeEnum.CouldNotDeleteItem.ToString());
			}

			return NoContent();
		}

		private string GenerateId()
		{
			return Thread.CurrentThread.ManagedThreadId.ToString() + System.DateTime.Now.ToString();
		}
	}
}
