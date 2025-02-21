using AutoMapper;
using GerenciadorReservas.Application.DTOs;
using GerenciadorReservas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorReservas.Application.Mappings
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<Usuario, UsuarioDTO>().ReverseMap();
            CreateMap<Sala, SalaDTO>().ReverseMap();
            CreateMap<Reserva, ReservaDTO>().ReverseMap();
        }
    }
}
