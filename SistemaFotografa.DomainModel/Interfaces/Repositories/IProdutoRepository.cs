using SistemaFotografa.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaFotografa.DomainModel.Interfaces.Repositories
{
    public interface IProdutoRepository : IRepository<Produto, Guid>
    {
        
    }
}
