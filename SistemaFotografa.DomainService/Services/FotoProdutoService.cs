﻿using SistemaFotografa.DomainModel.Entities;
using SistemaFotografa.DomainModel.Interfaces.Repositories;
using SistemaFotografa.DomainModel.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaFotografa.DomainService.Services
{
    public class FotoProdutoService : IFotoProdutoService
    {
        private readonly IFotoProdutoRepository _fotoProdutoRepository;
        public FotoProdutoService(IFotoProdutoRepository fotoProdutoRepository)
        {
            _fotoProdutoRepository = fotoProdutoRepository;
        }
        public void Criar(FotoProduto entity)
        {
            _fotoProdutoRepository.Create(entity);
        }

        public void Deletar(Guid id)
        {
            _fotoProdutoRepository.Delete(id);
        }

        public FotoProduto Buscar(Guid id)
        {
            return _fotoProdutoRepository.Read(id);
        }

        public IEnumerable<FotoProduto> BuscarTodos()
        {
            return _fotoProdutoRepository.ReadAll();
        }

        public void Atualizar(FotoProduto entity)
        {
            _fotoProdutoRepository.Update(entity);
        }
    }
}
