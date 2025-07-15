using Libreria.LogicaNegocio.Excepciones;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using LogicaAplicacion.Dtos;
using LogicaAplicacion.Dtos.ClienteDTO;
using LogicaAplicacion.Infraestructura.Helpers;
using LogicaAplicacion.InterfacesCasosDeUso.ICUCliente;
using LogicaNegocio.Entidades;
using Microsoft.AspNetCore.Identity;

public class CUAltaCliente : ICUAltaCliente
{
    private readonly IRepositorioClientes _repo;
    private readonly PasswordHasher<Cliente> _hasher;

    public CUAltaCliente(IRepositorioClientes repo)
    {
        _repo = repo;
        _hasher = new PasswordHasher<Cliente>();
    }

    public void AltaCliente(RegistroClienteDTO dto)
    {
        // 1) Normalizar y validar el teléfono
        string telNormalizado;
        try
        {
            telNormalizado = UruguayPhoneHelper.Normalizar(dto.Telefono);
        }
        catch (ArgumentException ex)
        {
            throw new UsuarioException(ex.Message);
        }

        // 2) Verificar unicidad con el formato +598XXXXXXXX
        if (_repo.ExisteTelefono(telNormalizado))
            throw new UsuarioException("Ya existe un cliente con ese teléfono.");

        // 3) Crear la entidad
        var nuevo = new Cliente
        {
            Nombre = dto.Nombre,
            Apellido = dto.Apellido,
            Email = dto.Email,
            Telefono = telNormalizado,      // ← siempre el formato internacional
            PasswordPlano = dto.PasswordPlano,
            EsRegistrado = true,
            Password = ""                   // se completa luego del hash
        };

        // 4) Validaciones de dominio
        nuevo.EsValido();                       

        // 5) Hash de la contraseña (si la proveyeron)
        if (!string.IsNullOrWhiteSpace(dto.PasswordPlano))
            nuevo.Password = _hasher.HashPassword(nuevo, dto.PasswordPlano);

        // 6) Guardar
        _repo.Add(nuevo);
    }
}


