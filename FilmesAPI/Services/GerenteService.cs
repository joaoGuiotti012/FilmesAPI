using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos.Gerente;
using FilmesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FilmesAPI.Services
{
    public class GerenteService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public GerenteService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<ReadGerenteDto> GetAll()
        {
            List<Gerente> gerentes = _context.Gerentes.ToList();
            return _mapper.Map<List<ReadGerenteDto>>(gerentes);
        }

        public ReadGerenteDto GetById(int id)
        {
            Gerente gerente = _context.Gerentes.FirstOrDefault(g => g.Id == id);
            return gerente != null ? _mapper.Map<ReadGerenteDto>(gerente) : null;
        }

        public Gerente Add(CreateGerenteDto gerenteDto)
        {
            Gerente gerente = _mapper.Map<Gerente>(gerenteDto);
            _context.Gerentes.Add(gerente);
            _context.SaveChanges();
            return gerente;
        }

        public bool DeleteById(int id)
        {
            var gerente = _context.Gerentes.FirstOrDefault(g => g.Id == id);
            if (gerente == null)
                return false;
            _context.Gerentes.Remove(gerente);
            _context.SaveChanges();
            return true;
        }
    }
}
