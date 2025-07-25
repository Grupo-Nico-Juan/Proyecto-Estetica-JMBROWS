﻿// <auto-generated />
using System;
using LogicaAccesoDatos.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LogicaAccesoDatos.Migrations
{
    [DbContext(typeof(EsteticaContext))]
    [Migration("20250620182414_altaServicioUpdate")]
    partial class altaServicioUpdate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EmpleadoHabilidad", b =>
                {
                    b.Property<int>("EmpleadoId")
                        .HasColumnType("int");

                    b.Property<int>("HabilidadesId")
                        .HasColumnType("int");

                    b.HasKey("EmpleadoId", "HabilidadesId");

                    b.HasIndex("HabilidadesId");

                    b.ToTable("EmpleadoHabilidad", (string)null);
                });

            modelBuilder.Entity("EmpleadoSector", b =>
                {
                    b.Property<int>("EmpleadoId")
                        .HasColumnType("int");

                    b.Property<int>("SectoresAsignadosId")
                        .HasColumnType("int");

                    b.HasKey("EmpleadoId", "SectoresAsignadosId");

                    b.HasIndex("SectoresAsignadosId");

                    b.ToTable("EmpleadoSector", (string)null);
                });

            modelBuilder.Entity("HabilidadServicio", b =>
                {
                    b.Property<int>("HabilidadesId")
                        .HasColumnType("int");

                    b.Property<int>("ServicioId")
                        .HasColumnType("int");

                    b.HasKey("HabilidadesId", "ServicioId");

                    b.HasIndex("ServicioId");

                    b.ToTable("HabilidadServicio", (string)null);
                });

            modelBuilder.Entity("LogicaNegocio.Entidades.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EsRegistrado")
                        .HasColumnType("bit");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("Telefono")
                        .IsUnique();

                    b.ToTable("Clientes", (string)null);
                });

            modelBuilder.Entity("LogicaNegocio.Entidades.DetalleTurno", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ServicioId")
                        .HasColumnType("int");

                    b.Property<int>("TurnoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ServicioId");

                    b.HasIndex("TurnoId");

                    b.ToTable("DetallesTurno");
                });

            modelBuilder.Entity("LogicaNegocio.Entidades.Habilidad", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Eliminada")
                        .HasColumnType("bit");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Habilidades");
                });

            modelBuilder.Entity("LogicaNegocio.Entidades.Notificacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ClienteId")
                        .HasColumnType("int");

                    b.Property<string>("Destinatario")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Enviada")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaEnvio")
                        .HasColumnType("datetime2");

                    b.Property<string>("Medio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mensaje")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PromocionId")
                        .HasColumnType("int");

                    b.Property<int?>("TurnoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.HasIndex("PromocionId");

                    b.HasIndex("TurnoId");

                    b.ToTable("Notificacion");
                });

            modelBuilder.Entity("LogicaNegocio.Entidades.PeriodoLaboral", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("Desde")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DiaSemana")
                        .HasColumnType("int");

                    b.Property<int>("EmpleadaId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Hasta")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan?>("HoraFin")
                        .HasColumnType("time");

                    b.Property<TimeSpan?>("HoraInicio")
                        .HasColumnType("time");

                    b.Property<string>("Motivo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Tipo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmpleadaId");

                    b.ToTable("PeriodosLaborales");
                });

            modelBuilder.Entity("LogicaNegocio.Entidades.Promocion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Activa")
                        .HasColumnType("bit");

                    b.Property<int?>("ClienteId")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Eliminada")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaFin")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PorcentajeDescuento")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.ToTable("Promocion");
                });

            modelBuilder.Entity("LogicaNegocio.Entidades.Sector", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SucursalId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SucursalId");

                    b.ToTable("Sectores");
                });

            modelBuilder.Entity("LogicaNegocio.Entidades.Sucursal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Sucursales");
                });

            modelBuilder.Entity("LogicaNegocio.Entidades.Turno", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Cancelado")
                        .HasColumnType("bit");

                    b.Property<int>("ClienteId")
                        .HasColumnType("int");

                    b.Property<int>("EmpleadaId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaHora")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Realizado")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.HasIndex("EmpleadaId");

                    b.ToTable("Turnos");
                });

            modelBuilder.Entity("LogicaNegocio.Entidades.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoUsuario")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");

                    b.HasDiscriminator<string>("TipoUsuario").HasValue("Usuario");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("SectorServicio", b =>
                {
                    b.Property<int>("SectoresId")
                        .HasColumnType("int");

                    b.Property<int>("ServiciosId")
                        .HasColumnType("int");

                    b.HasKey("SectoresId", "ServiciosId");

                    b.HasIndex("ServiciosId");

                    b.ToTable("ServicioSector", (string)null);
                });

            modelBuilder.Entity("Servicio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DuracionMinutos")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Precio")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("PromocionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PromocionId");

                    b.ToTable("Servicios");
                });

            modelBuilder.Entity("Libreria.LogicaNegocio.Entidades.Administrador", b =>
                {
                    b.HasBaseType("LogicaNegocio.Entidades.Usuario");

                    b.HasDiscriminator().HasValue("Administrador");
                });

            modelBuilder.Entity("LogicaNegocio.Entidades.Empleado", b =>
                {
                    b.HasBaseType("LogicaNegocio.Entidades.Usuario");

                    b.Property<string>("Cargo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Empleado");
                });

            modelBuilder.Entity("EmpleadoHabilidad", b =>
                {
                    b.HasOne("LogicaNegocio.Entidades.Empleado", null)
                        .WithMany()
                        .HasForeignKey("EmpleadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LogicaNegocio.Entidades.Habilidad", null)
                        .WithMany()
                        .HasForeignKey("HabilidadesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EmpleadoSector", b =>
                {
                    b.HasOne("LogicaNegocio.Entidades.Empleado", null)
                        .WithMany()
                        .HasForeignKey("EmpleadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LogicaNegocio.Entidades.Sector", null)
                        .WithMany()
                        .HasForeignKey("SectoresAsignadosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HabilidadServicio", b =>
                {
                    b.HasOne("LogicaNegocio.Entidades.Habilidad", null)
                        .WithMany()
                        .HasForeignKey("HabilidadesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Servicio", null)
                        .WithMany()
                        .HasForeignKey("ServicioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LogicaNegocio.Entidades.DetalleTurno", b =>
                {
                    b.HasOne("Servicio", "Servicio")
                        .WithMany()
                        .HasForeignKey("ServicioId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LogicaNegocio.Entidades.Turno", "Turno")
                        .WithMany("Detalles")
                        .HasForeignKey("TurnoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Servicio");

                    b.Navigation("Turno");
                });

            modelBuilder.Entity("LogicaNegocio.Entidades.Notificacion", b =>
                {
                    b.HasOne("LogicaNegocio.Entidades.Cliente", null)
                        .WithMany("Notificaciones")
                        .HasForeignKey("ClienteId");

                    b.HasOne("LogicaNegocio.Entidades.Promocion", "Promocion")
                        .WithMany()
                        .HasForeignKey("PromocionId");

                    b.HasOne("LogicaNegocio.Entidades.Turno", "Turno")
                        .WithMany()
                        .HasForeignKey("TurnoId");

                    b.Navigation("Promocion");

                    b.Navigation("Turno");
                });

            modelBuilder.Entity("LogicaNegocio.Entidades.PeriodoLaboral", b =>
                {
                    b.HasOne("LogicaNegocio.Entidades.Empleado", "Empleada")
                        .WithMany("PeriodosLaborales")
                        .HasForeignKey("EmpleadaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Empleada");
                });

            modelBuilder.Entity("LogicaNegocio.Entidades.Promocion", b =>
                {
                    b.HasOne("LogicaNegocio.Entidades.Cliente", null)
                        .WithMany("Promociones")
                        .HasForeignKey("ClienteId");
                });

            modelBuilder.Entity("LogicaNegocio.Entidades.Sector", b =>
                {
                    b.HasOne("LogicaNegocio.Entidades.Sucursal", "Sucursal")
                        .WithMany("Sectores")
                        .HasForeignKey("SucursalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sucursal");
                });

            modelBuilder.Entity("LogicaNegocio.Entidades.Turno", b =>
                {
                    b.HasOne("LogicaNegocio.Entidades.Cliente", "Cliente")
                        .WithMany("Turnos")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LogicaNegocio.Entidades.Empleado", "Empleada")
                        .WithMany("TurnosAsignados")
                        .HasForeignKey("EmpleadaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Cliente");

                    b.Navigation("Empleada");
                });

            modelBuilder.Entity("SectorServicio", b =>
                {
                    b.HasOne("LogicaNegocio.Entidades.Sector", null)
                        .WithMany()
                        .HasForeignKey("SectoresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Servicio", null)
                        .WithMany()
                        .HasForeignKey("ServiciosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Servicio", b =>
                {
                    b.HasOne("LogicaNegocio.Entidades.Promocion", null)
                        .WithMany("ServiciosIncluidos")
                        .HasForeignKey("PromocionId");
                });

            modelBuilder.Entity("LogicaNegocio.Entidades.Cliente", b =>
                {
                    b.Navigation("Notificaciones");

                    b.Navigation("Promociones");

                    b.Navigation("Turnos");
                });

            modelBuilder.Entity("LogicaNegocio.Entidades.Promocion", b =>
                {
                    b.Navigation("ServiciosIncluidos");
                });

            modelBuilder.Entity("LogicaNegocio.Entidades.Sucursal", b =>
                {
                    b.Navigation("Sectores");
                });

            modelBuilder.Entity("LogicaNegocio.Entidades.Turno", b =>
                {
                    b.Navigation("Detalles");
                });

            modelBuilder.Entity("LogicaNegocio.Entidades.Empleado", b =>
                {
                    b.Navigation("PeriodosLaborales");

                    b.Navigation("TurnosAsignados");
                });
#pragma warning restore 612, 618
        }
    }
}
