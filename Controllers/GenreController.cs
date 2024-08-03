﻿using AutoMapper;
using C_BookStoreBackEndAPI.Data;
using C_BookStoreBackEndAPI.Dtos.Genre;
using C_BookStoreBackEndAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace C_BookStoreBackEndAPI.Controllers
{
    [Route("api/genre")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly BookStoreDBContext _context;
        private readonly IMapper _mapper;
        public GenreController(BookStoreDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        { 
            var genres = _context.Genres.ToList();

           // return Ok(genres);
           return Ok(_mapper.Map<IEnumerable<GenreDto>>(genres));
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id) 
        {
            var genre = _context.Genres.Find(id);

            if (genre == null)
            {
                return NotFound();
            }
            return Ok(genre);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateGenreDto createGenreDto)
        {
            var genre = _mapper.Map<Genre>(createGenreDto);
            _context.Genres.Add(genre);
            _context.SaveChanges();

            var genreDto = _mapper.Map<GenreDto>(genre);

            return CreatedAtAction(nameof(GetById), new { id = genre.Id }, genreDto);

        }
    }
}
