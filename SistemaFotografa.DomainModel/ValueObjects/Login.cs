using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaFotografa.DomainModel.ValueObjects
{
    public class Login
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Administrador { get; set; }
    }
}
