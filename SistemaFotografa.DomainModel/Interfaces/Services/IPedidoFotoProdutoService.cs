using SistemaFotografa.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaFotografa.DomainModel.Interfaces.Services
{
    public interface IPedidoFotoProdutoService : IServiceBase<PedidoFotoProduto, Guid>
    {
        IEnumerable<PedidoFotoProduto> BuscarTodosDoPedido(Guid pedido);
    }
}
