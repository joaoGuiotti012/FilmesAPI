using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos.Endereco;
using FilmesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FilmesAPI.Services
{
    public class EnderecoService
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public EnderecoService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<ReadEnderecoDto> GetAll()
        {
            var enderecos = _context.Enderecos;
            return _mapper.Map<List<ReadEnderecoDto>>(enderecos);
        }

        public ReadEnderecoDto GetById(int id)
        {
            var endereco = _context.Enderecos.FirstOrDefault(end => end.Id == id);
            if (endereco == null)
                return null;
            return _mapper.Map<ReadEnderecoDto>(endereco);
        }

        public Endereco Add(CreateEnderecoDto createDto)
        {
            Endereco endereco = _mapper.Map<Endereco>(createDto);
            _context.Add(endereco);
            _context.SaveChanges();
            return endereco;
        }

        public bool Update(int id, UpdateEnderecoDto enderecoDto)
        {
            Endereco endereco = _context.Enderecos.FirstOrDefault(e => e.Id == id);
            _mapper.Map(enderecoDto, endereco);
            if (endereco == null)
                return false;
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            Endereco endereco = _context.Enderecos.FirstOrDefault(e => e.Id == id);
            if (endereco == null)
                return false;
            _context.Remove(endereco);
            _context.SaveChanges();
            return true;
        }
    }
}
