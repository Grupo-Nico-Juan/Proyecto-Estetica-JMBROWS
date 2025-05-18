using Libreria.LogicaNegocio.Entidades;
using LogicaNegocio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace LogicaAccesoDatos.EF
{
    public class EsteticaContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"SERVER=(localDb)\MsSqlLocalDb;Database=EsteticaDB;Integrated Security=true;Encrypt=false");

        }
        public EsteticaContext(DbContextOptions<EsteticaContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>()
                .HasDiscriminator<string>("TipoUsuario")
                .HasValue<Administrador>("Administrador")
                .HasValue<Empleado>("Empleado")
                .HasValue<Cliente>("Cliente");
        }


    }
}
