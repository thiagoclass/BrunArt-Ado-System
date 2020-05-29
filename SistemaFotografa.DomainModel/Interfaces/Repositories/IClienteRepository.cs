using SistemaFotografa.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaFotografa.DomainModel.Interfaces.Repositories
{
    public interface IClienteRepository:IRepository<Cliente,Guid>
    {
    }
}
