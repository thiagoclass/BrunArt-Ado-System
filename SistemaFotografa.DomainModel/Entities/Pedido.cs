using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaFotografa.DomainModel.Entities
{
    public class Pedido:EntityBase<Guid>
       {
        public Cliente Cliente { get; set; }
        public DateTime DataPedido { get; set; }
        public Pedido()
        {
            Id = Guid.NewGuid();
            Cliente = new Cliente();
        }
    }
}
