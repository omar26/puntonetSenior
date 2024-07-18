using Usuarios.Domain.Abstractions;

namespace Usuarios.Domain.Roles;

public class Rol : Entity
{
    private Rol(){}
    private Rol(
        Guid id,
        NombreRol nombreRol,
        Descripcion descripcion
    ) : base(id)
    {
        NombreRol = nombreRol;
        Descripcion = descripcion;
    }

    public NombreRol? NombreRol { get; set; }
    public Descripcion? Descripcion { get; set; }

    public static Rol Create(
        NombreRol nombreRol,
        Descripcion descripcion
    )
    {
        var rol = new Rol(Guid.NewGuid(),nombreRol,descripcion);
        return rol;
    }

}
