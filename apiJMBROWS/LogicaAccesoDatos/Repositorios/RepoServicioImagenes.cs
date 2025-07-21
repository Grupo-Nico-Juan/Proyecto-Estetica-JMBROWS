using LogicaAccesoDatos.EF;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.Repositorios
{
    public class RepoServicioImagenes : IRepoServicioImagenes
    {
        private readonly EsteticaContext _ctx;
        public RepoServicioImagenes(EsteticaContext ctx) => _ctx = ctx;

        public void Add(ServicioImagen img) => _ctx.ServicioImagenes.Add(img);
        public void Remove(ServicioImagen img) => _ctx.ServicioImagenes.Remove(img);
        public ServicioImagen? Get(int id) => _ctx.ServicioImagenes.Find(id);
        public Task SaveAsync() => _ctx.SaveChangesAsync();
    }

}
