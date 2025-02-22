using AutoMapper;
using GerenciadorReservas.Application.DTOs;
using GerenciadorReservas.Application.Interfaces;
using GerenciadorReservas.Domain.Entities;
using GerenciadorReservas.Domain.Interfaces;

namespace GerenciadorReservas.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        private readonly IMapper _mapper;

        public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<UsuarioDTO>> GetUsuarios()
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UsuarioDTO>>(usuarios);
        }

        public async Task<UsuarioDTO> GetUsuario(int? id)
        {
            var usuarioEntity = await _usuarioRepository.GetByIdAsync(id);
            return _mapper.Map<UsuarioDTO>(usuarioEntity);

        }


        public async Task Add(UsuarioDTO usuario)
        {
            var usuarioEntity = _mapper.Map<Usuario>(usuario);
            await _usuarioRepository.CreateAsync(usuarioEntity);
        }

        public async Task Update(UsuarioDTO usuario)
        {
           var usuarioEntity = _mapper.Map<Usuario>(usuario);
           await _usuarioRepository.UpdateAsync(usuarioEntity);
        }

        public async Task Remove(int? id)
        {
            var usuarioEntity = _usuarioRepository.GetByIdAsync(id).Result;
            await _usuarioRepository.RemoveAsync(usuarioEntity);
        }
    }
}
