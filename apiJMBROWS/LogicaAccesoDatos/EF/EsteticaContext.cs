using Libreria.LogicaNegocio.Entidades;
using LogicaNegocio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace LogicaAccesoDatos.EF
{
    public class EsteticaContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Sucursal> Sucursales { get; set; }
        public DbSet<Sector> Sectores { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<Habilidad> Habilidades { get; set; }
        public DbSet<Turno> Turnos { get; set; }
        public DbSet<DetalleTurno> DetalleTurnos { get; set; }

        public EsteticaContext(DbContextOptions<EsteticaContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=tcp:jmbrows.database.windows.net,1433;Initial Catalog=JMBRowsDB;Persist Security Info=False;User ID=jmbrows;Password=Pestañas123.;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            // TODO: Mover al appsettings.json
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Servicio>()
                .Property(s => s.Precio)
                .HasPrecision(10, 2);

            modelBuilder.Entity<DetalleTurno>()
                .Property(dt => dt.Precio)
                .HasPrecision(10, 2);

            // Herencia TPH para Usuario
            modelBuilder.Entity<Usuario>().HasKey(u => u.Id);
            modelBuilder.Entity<Usuario>()
                .HasDiscriminator<string>("TipoUsuario")
                .HasValue<Administrador>("Administrador")
                .HasValue<Empleado>("Empleado");

            // Cliente es tabla separada (no hereda de Usuario)
            modelBuilder.Entity<Cliente>().ToTable("Clientes");

            // Empleado ↔ Habilidad (muchos a muchos)
            modelBuilder.Entity<Empleado>()
                .HasMany(e => e.Habilidades)
                .WithMany()
                .UsingEntity(j => j.ToTable("EmpleadoHabilidad"));

            // Servicio ↔ Habilidad (muchos a muchos)
            modelBuilder.Entity<Servicio>()
                .HasMany(s => s.Habilidades)
                .WithMany()
                .UsingEntity(j => j.ToTable("HabilidadServicio"));

            // Servicio ↔ Sector (muchos a muchos)
            modelBuilder.Entity<Servicio>()
                .HasMany(s => s.Sectores)
                .WithMany(se => se.Servicios)
                .UsingEntity(j => j.ToTable("ServicioSector"));

            // Empleado ↔ Sector (muchos a muchos)
            modelBuilder.Entity<Empleado>()
                .HasMany(e => e.SectoresAsignados)
                .WithMany()
                .UsingEntity(j => j.ToTable("EmpleadoSector"));

            // Turno ↔ Empleada
            modelBuilder.Entity<Turno>()
                .HasOne(t => t.Empleada)
                .WithMany(e => e.TurnosAsignados)
                .HasForeignKey(t => t.EmpleadaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Turno ↔ Cliente
            modelBuilder.Entity<Turno>()
                .HasOne(t => t.Cliente)
                .WithMany(c => c.Turnos)
                .HasForeignKey(t => t.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
