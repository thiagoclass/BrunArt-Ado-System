using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaFotografa.DomainModel.Entities
{
    public class Album : EntityBase<Guid>
    {
        
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public Cliente Cliente { get; set; }
        public Album()
        {
            Id = Guid.NewGuid();
        }

    }
}
