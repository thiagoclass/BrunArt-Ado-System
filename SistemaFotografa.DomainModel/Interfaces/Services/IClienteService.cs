using SistemaFotografa.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaFotografa.DomainModel.Interfaces.Services
{
    public interface IClienteService:IServiceBase<Cliente,Guid>
    {
        Cliente BuscarPeloUsuario(String Username);
    }
}
