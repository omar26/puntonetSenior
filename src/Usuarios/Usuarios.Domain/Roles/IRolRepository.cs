namespace Usuarios.Domain.Roles;

public interface IRolRepository
{
    Task<Rol?> GeByNameAsync(string rol, CancellationToken cancellationToken= default);
}
