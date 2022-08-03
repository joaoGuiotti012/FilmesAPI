using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos.Filme;
using FilmesAPI.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FilmesAPI.Services
{
    public class FilmeService
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public FilmeService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ReadFilmeDto Add(CreateFilmeDto filmeDto)
        {
            Filme filme = _mapper.Map<Filme>(filmeDto);
            _context.Add(filme);
            _context.SaveChanges();
            return _mapper.Map<ReadFilmeDto>(filme);
        }

        public List<ReadFilmeDto> GetAll(int? classificacaoEtaria)
        {
            List<Filme> filmes;
            if (classificacaoEtaria == null)
                filmes = _context.Filmes.ToList();
            else
                filmes = _context.Filmes.Where(f => f.ClassificaoEtaria <= classificacaoEtaria).ToList();
            if (filmes != null)
            {
                List<ReadFilmeDto> readDto = _mapper.Map<List<ReadFilmeDto>>(filmes);
                return readDto;
            }
            return null;
        }

        public ReadFilmeDto GetById(int id)
        {
            var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme == null)
                return null;
            ReadFilmeDto filmeDto = _mapper.Map<ReadFilmeDto>(filme);
            filmeDto.HoraDaConsulta = DateTime.Now;
            return filmeDto;
        }

        public bool DeleteById(int id)
        {
            var filme = _context.Filmes.FirstOrDefault(f => f.Id == id);
            if (filme == null)
                return false;
            _context.Filmes.Remove(filme);
            _context.SaveChanges();
            return true;
        }

        public bool Update(int id, UpdateFilmeDto filmeDto)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme == null)
                return false;
            _mapper.Map(filmeDto, filme);
            _context.SaveChanges();
            return true;
        }
    }
}
