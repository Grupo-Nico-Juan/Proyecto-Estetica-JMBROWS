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
            optionsBuilder.UseSqlServer(@"Server=tcp:jmbrowsdbserver.database.windows.net,1433;Initial Catalog=JMBRowsDB;Persist Security Info=False;User ID=jmbrows;Password=Pestañas123.;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // TPH: Administrador y Empleado en Usuarios
            modelBuilder.Entity<Usuario>().HasKey(u => u.Id);

            modelBuilder.Entity<Usuario>()
                .HasDiscriminator<string>("TipoUsuario")
                .HasValue<Administrador>("Administrador")
                .HasValue<Empleado>("Empleado");

            // Cliente es una clase separada (sin herencia)
            modelBuilder.Entity<Cliente>().ToTable("Clientes");

            // Relación Empleado ↔ Habilidad
            modelBuilder.Entity<Empleado>()
                .HasMany(e => e.Habilidades)
                .WithMany(h => h.Empleadas)
                .UsingEntity(j => j.ToTable("EmpleadoHabilidad"));

            // Relación Habilidad ↔ Servicio
            modelBuilder.Entity<Habilidad>()
                .HasMany(h => h.Servicios)
                .WithMany(s => s.Habilidades)
                .UsingEntity(j => j.ToTable("HabilidadServicio"));

            // Relación Servicio ↔ Sector
            modelBuilder.Entity<Servicio>()
                .HasMany(s => s.Sectores)
                .WithMany(se => se.Servicios)
                .UsingEntity(j => j.ToTable("ServicioSector"));

            // Relación Empleado ↔ Sector (manual)
            modelBuilder.Entity<Empleado>()
                .HasMany(e => e.SectoresAsignados)
                .WithMany(se => se.Empleadas)
                .UsingEntity(j => j.ToTable("EmpleadoSector"));

            // Relación Turno ↔ Empleada
            modelBuilder.Entity<Turno>()
                .HasOne(t => t.Empleada)
                .WithMany(e => e.TurnosAsignados)
                .HasForeignKey(t => t.EmpleadaId)
                .OnDelete(DeleteBehavior.Restrict); // evita cascadas múltiples

            // Relación Turno ↔ Cliente
            modelBuilder.Entity<Turno>()
                .HasOne(t => t.Cliente)
                .WithMany(c => c.Turnos)
                .HasForeignKey(t => t.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}



