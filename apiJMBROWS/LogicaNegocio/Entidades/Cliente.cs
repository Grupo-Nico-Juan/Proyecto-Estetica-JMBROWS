using LogicaNegocio.Entidades;

namespace Libreria.LogicaNegocio.Entidades
{
    public class Cliente : Usuario
    {
        public DateTime FechaNacimiento { get; set; }

        public override string Rol => "Cliente";
    }
}

