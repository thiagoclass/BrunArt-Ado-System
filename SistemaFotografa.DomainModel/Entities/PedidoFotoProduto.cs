using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaFotografa.DomainModel.Entities
{
    public class PedidoFotoProduto:EntityBase<Guid>
    {
        
        public Pedido Pedido { get; set; }

        public FotoProduto FotoProduto { get; set; }
        public PedidoFotoProduto()
        {
            Id = Guid.NewGuid();
            FotoProduto = new FotoProduto();
        }
    }
}
