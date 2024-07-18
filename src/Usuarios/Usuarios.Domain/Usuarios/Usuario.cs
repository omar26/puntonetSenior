using Usuarios.Domain.Abstractions;
using Usuarios.Domain.Roles;
using Usuarios.Domain.Usuarios.Events;

namespace Usuarios.Domain.Usuarios;

public sealed class Usuario : Entity
{
    private Usuario() { }
    private Usuario(
          Guid id
        , NombreUsuario nombreUsuario
        , Contrasenia contrasenia
        , Guid rolId
        , NombresPersona nombresPersona
        , ApellidoPaterno apellidoPaterno
        , ApellidoMaterno apellidoMaterno
        , Direccion direccion
        , DateTime fechaNacimiento
        , CorreoElectronico correoElectronico
        , UsuarioEstado estado
        , DateTime fechaUltimoCambio
        ) : base(id)
    {
        NombreUsuario = nombreUsuario;
        Contrasenia = contrasenia;
        RolId = rolId;
        NombresPersona = nombresPersona;
        ApellidoPaterno = apellidoPaterno;
        ApellidoMaterno = apellidoMaterno;
        Direccion = direccion;
        FechaNacimiento = fechaNacimiento;
        CorreoElectronico = correoElectronico;
        Estado = estado;
        FechaUltimoCambio = fechaUltimoCambio;
    }
    public NombreUsuario? NombreUsuario { get; private set; }
    public Contrasenia? Contrasenia { get; private set; }
    public Rol? Rol { get; private set; }
    public Guid? RolId { get; private set; }
    public NombresPersona? NombresPersona { get; private set; }
    public ApellidoPaterno? ApellidoPaterno { get; private set; }
    public ApellidoMaterno? ApellidoMaterno { get; private set; }
    public Direccion? Direccion { get; private set; }
    public DateTime? FechaNacimiento { get; private set; }
    public CorreoElectronico? CorreoElectronico { get; private set; }
    public UsuarioEstado Estado { get; private set; }
    public DateTime? FechaUltimoCambio { get; private set; }

    public static Usuario Create(
          Contrasenia contrasenia
        , Guid rolId
        , NombresPersona nombresPersona
        , ApellidoPaterno apellidoPaterno
        , ApellidoMaterno apellidoMaterno
        , Direccion direccion
        , DateTime fechaNacimiento
        , DateTime fechaCreacion
        , CorreoElectronico correoElectronico
        , NombreUsuarioService nombreUsuarioService
    )
    {

        var nombreUsuario = nombreUsuarioService.GenerarNombreUsuario(apellidoPaterno, nombresPersona);

        var usuario = new Usuario(
            Guid.NewGuid(),
            nombreUsuario,
            contrasenia,
            rolId,
            nombresPersona,
            apellidoPaterno,
            apellidoMaterno,
            direccion,
            fechaNacimiento,
            correoElectronico,
            UsuarioEstado.Activo,
            fechaCreacion
        );

        usuario.RaiseDomainEvent(new UserCreateDomainEvent(usuario.Id));

        return usuario;
    }

    public Result Activar(DateTime utcNow)
    {
        if (Estado == UsuarioEstado.Activo)
        {
            return Result.Failure(UsuarioErrores.YaSeEncuentraActivo);
        }
        Estado = UsuarioEstado.Activo;
        FechaUltimoCambio = utcNow;

        return Result.Success();
    }

    public Result Inactivar(DateTime utcNow)
    {
        if (Estado == UsuarioEstado.Inactivo)
        {
            return Result.Failure(UsuarioErrores.YaSeEncuentraInactivo);
        }
        Estado = UsuarioEstado.Inactivo;
        FechaUltimoCambio = utcNow;
        return Result.Success();
    }


}
