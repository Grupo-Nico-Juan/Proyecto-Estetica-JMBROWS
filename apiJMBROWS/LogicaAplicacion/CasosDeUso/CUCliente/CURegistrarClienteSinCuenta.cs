using Libreria.LogicaNegocio.Excepciones;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using LogicaAplicacion.Dtos.ClienteDTO;
using LogicaAplicacion.Infraestructura.Helpers;
using LogicaAplicacion.InterfacesCasosDeUso.ICUCliente;
using LogicaNegocio.Entidades;

public class CURegistrarClienteSinCuenta : ICURegistrarClienteSinCuenta
{
    private readonly IRepositorioClientes _repo;

    public CURegistrarClienteSinCuenta(IRepositorioClientes repo) => _repo = repo;

    public ReservaClienteDTO Ejecutar(ReservaClienteDTO dto)
    {
        // ───────────── Validación mínima ─────────────
        if (string.IsNullOrWhiteSpace(dto.Nombre) ||
            string.IsNullOrWhiteSpace(dto.Apellido) ||
            string.IsNullOrWhiteSpace(dto.Telefono))
            throw new ArgumentException("Nombre, apellido y teléfono son obligatorios.");

        // ───────────── Normalizar / validar teléfono ─────────────
        string tel;
        try
        {
            tel = UruguayPhoneHelper.Normalizar(dto.Telefono);
        }
        catch (ArgumentException ex)
        {
            throw new UsuarioException(ex.Message);
        }

        // ───────────── Buscar cliente existente ─────────────
        var existente = _repo.GetByTelefono(tel);
        if (existente != null)
        {
            return new ReservaClienteDTO
            {
                Id = existente.Id,
                Telefono = existente.Telefono,
                Email = existente.Email,
                Nombre = existente.Nombre,
                Apellido = existente.Apellido
            };
        }

        // ───────────── Crear nuevo cliente ocasional ─────────────
        var nuevo = new Cliente
        {
            Nombre = dto.Nombre,
            Apellido = dto.Apellido,
            Telefono = tel,          // siempre +598XXXXXXXX
            Email = dto.Email,
            EsRegistrado = false         // marca que no tiene cuenta
        };

        _repo.Add(nuevo);

        return new ReservaClienteDTO
        {
            Id = nuevo.Id,
            Telefono = nuevo.Telefono,
            Email = nuevo.Email,
            Nombre = nuevo.Nombre,
            Apellido = nuevo.Apellido
        };
    }
}
