using Microsoft.AspNetCore.Http;
using SistemaFotografa.DomainModel.Entities;
using SistemaFotografa.DomainModel.Interfaces.Repositories;
using SistemaFotografa.DomainModel.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistemaFotografa.DomainService.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
            
        }

        public void Criar(Cliente entity)
        {
            _clienteRepository.Create(entity);
        }

        public void Deletar(Guid id)
        {
            _clienteRepository.Delete(id);
        }

        public Cliente Buscar(Guid id)
        {
            return _clienteRepository.Read(id);
        }

        public IEnumerable<Cliente> BuscarTodos()
        {
            return _clienteRepository.ReadAll();
        }

        public void Atualizar(Cliente entity)
        {
            _clienteRepository.Update(entity);
        }

        public Cliente BuscarPeloUsuario(String Username)
        {
            return (Cliente)_clienteRepository.ReadAll().Where(c => c.Login.Username == Username).FirstOrDefault();
        }

    }
}
