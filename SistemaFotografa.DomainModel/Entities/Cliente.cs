using SistemaFotografa.DomainModel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaFotografa.DomainModel.Entities
{
    public class Cliente:EntityBase<Guid>
    {
        
        public string Nome { get; set; }
        public Login Login { get; set; }

    }
}
