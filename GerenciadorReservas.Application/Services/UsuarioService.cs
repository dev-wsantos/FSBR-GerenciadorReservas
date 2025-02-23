using AutoMapper;
using GerenciadorReservas.Application.DTOs;
using GerenciadorReservas.Application.Interfaces;
using GerenciadorReservas.Domain.Entities;
using GerenciadorReservas.Domain.Interfaces;

namespace GerenciadorReservas.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public UsuarioService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<UsuarioDTO>> GetUsuarios()
        {
            var usuarios = await _unitOfWork.UsuarioRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UsuarioDTO>>(usuarios);
        }

        public async Task<UsuarioDTO> GetUsuario(int? id)
        {
            var usuarioEntity = await _unitOfWork.UsuarioRepository.GetByIdAsync(id);
            return _mapper.Map<UsuarioDTO>(usuarioEntity);

        }


        public async Task Add(UsuarioDTO usuario)
        {
            var usuarioEntity = _mapper.Map<Usuario>(usuario);
            await _unitOfWork.UsuarioRepository.CreateAsync(usuarioEntity);
        }

        public async Task Update(UsuarioDTO usuario)
        {
           var usuarioEntity = _mapper.Map<Usuario>(usuario);
           await _unitOfWork.UsuarioRepository.UpdateAsync(usuarioEntity);
        }

        public async Task Remove(int? id)
        {
            var usuarioEntity = _unitOfWork.UsuarioRepository.GetByIdAsync(id).Result;
            await _unitOfWork.UsuarioRepository.RemoveAsync(usuarioEntity);
        }
    }
}
