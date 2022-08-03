using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos.Cinema;
using FilmesAPI.Models;
using FilmesAPI.Services.Contratos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FilmesAPI.Services
{
    public class CinemaService
    {
        private AppDbContext _context;
        private IMapper _mapper;
        private IGeralService _geralService;
        public CinemaService(AppDbContext context, IMapper mapper, IGeralService geralService)
        {
            _context = context;
            _mapper = mapper;
            _geralService = geralService;
        }

        public List<ReadCinemaDto> GetAll(string nomeFilme)
        {
            List<Cinema> cinemas = _context.Cinemas.ToList();
            if (cinemas == null)
                return null;
            if (!string.IsNullOrEmpty(nomeFilme))
            { 
                IEnumerable<Cinema> query = from cinema in cinemas
                                            where cinema.Sessoes.Any(sessao => sessao.Filme.Titulo.ToLower().Contains(nomeFilme.ToLower()))
                                            select cinema;
                cinemas = query.ToList();
            }
            List<ReadCinemaDto> readDto = _mapper.Map<List<ReadCinemaDto>>(cinemas);
            return readDto;
        }

        public ReadCinemaDto GetById(int id)
        {
            Cinema cinema = _context.Cinemas.FirstOrDefault(c => c.Id == id);
            if (cinema == null)
                return null;
            ReadCinemaDto cinemaDto = _mapper.Map<ReadCinemaDto>(cinema);
            return cinemaDto;
        }

        public bool Update(int id, UpdateCinemaDto cinemaDto)
        {
            Cinema cinema = _context.Cinemas.FirstOrDefault(c => c.Id == id);
            if (cinema == null)
                return false;
            _mapper.Map(cinemaDto, cinema);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteById(int id)
        {
            Cinema cinema = _context.Cinemas.FirstOrDefault(c => c.Id == id);
            if (cinema == null)
                return false;
            _context.Cinemas.Remove(cinema);
            _context.SaveChanges();
            return true;
        }

        public Cinema Add(CreateCinemaDto cinemaDto)
        {
            Cinema cinema = _mapper.Map<Cinema>(cinemaDto);
            _context.Add(cinema);
            _context.SaveChanges();
            return cinema;
        }
    }
}
