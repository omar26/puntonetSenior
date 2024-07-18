namespace Usuarios.Application.Usuarios.GetUsuarios;

public sealed class UsuarioResponse
{
    public Guid Id { get; init; }

    public string? Nombres { get; init; }
    public string? ApellidoPaterno { get; init; }
    public string? ApellidoMaterno { get; init; }

    public string? Rol { get; init; }
    public string? Pais { get; init; }
    public string? Departamento { get; init; }
    public string? Provincia { get; init; }
    public string? Ciudad { get; init; }
    public string? Calle { get; init; }
    public DateTime FechaNacimiento { get; init; }
    public string? CorreoElectronico { get; init; }
    public string? Estado { get; init; }
    public DateTime FechaUltimoCambio { get; init; }

}