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
        public DbSet<DetalleTurno> DetallesTurno { get; set; }
        public DbSet<PeriodoLaboral> PeriodosLaborales { get; set; }
        public DbSet<ExtraServicio> ExtrasServicio { get; set; }
        public DbSet<ServicioImagen> ServicioImagenes { get; set; }


        public EsteticaContext(DbContextOptions<EsteticaContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Servicio>()
             .Property(s => s.Precio)
            .HasPrecision(18, 2);

            modelBuilder.Entity<ExtraServicio>()
                .Property(e => e.Precio)
                .HasPrecision(18, 2);

            modelBuilder.Entity<ServicioImagen>()
            .HasOne(si => si.Servicio)
            .WithMany(s => s.Imagenes)
            .HasForeignKey(si => si.ServicioId)
            .OnDelete(DeleteBehavior.Cascade);

            // Herencia TPH para Usuario
            modelBuilder.Entity<Usuario>().HasKey(u => u.Id);
            modelBuilder.Entity<Usuario>()
                .HasDiscriminator<string>("TipoUsuario")
                .HasValue<Administrador>("Administrador")
                .HasValue<Empleado>("Empleado");

            // Cliente es tabla separada
            modelBuilder.Entity<Cliente>().ToTable("Clientes");

            modelBuilder.Entity<Cliente>()
                .HasIndex(c => c.Telefono)
                .IsUnique();

            // Empleado ↔ Habilidad
            modelBuilder.Entity<Empleado>()
                .HasMany(e => e.Habilidades)
                .WithMany()
                .UsingEntity(j => j.ToTable("EmpleadoHabilidad"));

            // Servicio ↔ Habilidad
            modelBuilder.Entity<Servicio>()
                .HasMany(s => s.Habilidades)
                .WithMany()
                .UsingEntity(j => j.ToTable("HabilidadServicio"));

            modelBuilder.Entity<Servicio>()
                .HasMany(s => s.Extras)
                .WithOne(e => e.Servicio)
                .HasForeignKey(e => e.ServicioId)
                .OnDelete(DeleteBehavior.Cascade);



            // Sector ↔ Servicio (unidireccional desde Sector)
            modelBuilder.Entity<Sector>()
                .HasMany(se => se.Servicios)
                .WithMany(s => s.Sectores)
                .UsingEntity(j => j.ToTable("ServicioSector"));

            // Empleado ↔ Sector
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

            // Relación Turno 1 - N DetalleTurno
            modelBuilder.Entity<Turno>()
                .HasMany(t => t.Detalles)
                .WithOne(d => d.Turno)
                .HasForeignKey(d => d.TurnoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación Servicio 1 - N DetalleTurno
            modelBuilder.Entity<Servicio>()
                .HasMany<DetalleTurno>()
                .WithOne(d => d.Servicio)
                .HasForeignKey(d => d.ServicioId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DetalleTurno>()
                .HasMany(d => d.Extras)
                .WithMany()
                .UsingEntity(j => j.ToTable("DetalleTurnoExtra"));
            // Relación Empleado 1 - N PeriodoLaboral
            modelBuilder.Entity<PeriodoLaboral>()
                .HasOne<Empleado>(p => p.Empleada)
                .WithMany(e => e.PeriodosLaborales)
                .HasForeignKey(p => p.EmpleadaId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
