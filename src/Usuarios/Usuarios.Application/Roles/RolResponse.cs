namespace Usuarios.Application.Roles;

public class RolResponse
{
    public Guid Id { get; init; }
    public string? Nombre { get; set; }
    public string? Descripcion { get; set; }
};
