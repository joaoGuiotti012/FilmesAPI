using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos.Sessao;
using FilmesAPI.Models;
using System;
using System.Linq;

namespace FilmesAPI.Services
{
    public class SessaoService
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public SessaoService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Sessao Add(CreateSessaoDto sessaoDto)
        {
            Sessao sessao = _mapper.Map<Sessao>(sessaoDto);
            _context.Add(sessao);
            _context.SaveChanges();
            return sessao;
        }

        public ReadSessaoDto GetById(int id)
        {
           Sessao sessao = _context.Sessoes.FirstOrDefault(c => c.Id == id);
            if (sessao == null)
                return null;
            return _mapper.Map<ReadSessaoDto>(sessao);
        }
    }
}
