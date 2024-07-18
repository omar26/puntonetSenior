using Usuarios.Domain.Abstractions;

namespace Usuarios.Domain.Roles;

public static class RolErrors
{
    public static Error NoEncontrado = new(
        "Rol.NoEncontrado",
        "No existe un rol con ese  nombre"
    );
}