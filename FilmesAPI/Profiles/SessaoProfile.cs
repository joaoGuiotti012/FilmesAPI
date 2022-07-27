﻿using AutoMapper;
using FilmesAPI.Data.Dtos.Sessao;
using FilmesAPI.Models;

namespace FilmesAPI.Profiles
{
    public class SessaoProfile : Profile
    {

        public SessaoProfile()
        {
            CreateMap<CreateSessaoDto, Sessao>();
            CreateMap<Sessao, ReadSessaoDto>()
                .ForMember(dto => dto.HorarioInicio, opts => opts
                    .MapFrom( dto => dto.HorarioEncerramento.AddMinutes(dto.Filme.Duracao * (-1)))
                );
        }
    }
}
