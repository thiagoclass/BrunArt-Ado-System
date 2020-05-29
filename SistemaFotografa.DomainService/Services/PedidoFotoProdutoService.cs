using SistemaFotografa.DomainModel.Entities;
using SistemaFotografa.DomainModel.Interfaces.Repositories;
using SistemaFotografa.DomainModel.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistemaFotografa.DomainService.Services
{
    public class PedidoFotoProdutoService : IPedidoFotoProdutoService
    {

        private readonly IPedidoFotoProdutoRepository _pedidoFotoProdutoRepository;
        public PedidoFotoProdutoService(IPedidoFotoProdutoRepository pedidoFotoProdutoRepository)
        {
            _pedidoFotoProdutoRepository = pedidoFotoProdutoRepository;
        }

        public void Criar(PedidoFotoProduto entity)
        {
            _pedidoFotoProdutoRepository.Create(entity);
        }

        public void Deletar(Guid id)
        {
            _pedidoFotoProdutoRepository.Delete(id);
        }

        public PedidoFotoProduto Buscar(Guid id)
        {
            return _pedidoFotoProdutoRepository.Read(id);
        }

        public IEnumerable<PedidoFotoProduto> BuscarTodos()
        {
            return _pedidoFotoProdutoRepository.ReadAll();
        }

        public void Atualizar(PedidoFotoProduto entity)
        {
            _pedidoFotoProdutoRepository.Update(entity);
        }
        public IEnumerable<PedidoFotoProduto> BuscarTodosDoPedido(Guid pedido)
        {
            return _pedidoFotoProdutoRepository.ReadAll()
                .Where(p=>p.Pedido.Id ==pedido);
        }
    }
}
