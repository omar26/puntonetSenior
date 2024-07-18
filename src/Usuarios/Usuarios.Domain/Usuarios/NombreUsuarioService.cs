namespace Usuarios.Domain.Usuarios;

public class NombreUsuarioService
{
    public NombreUsuario GenerarNombreUsuario(
        ApellidoPaterno apellidoPaterno,
        NombresPersona nombresPersona
    )
    {
        var inicialNombre =  nombresPersona.Value.Trim().Substring(0, 1);
        var restoNombreUsuario = apellidoPaterno.Value.Trim().Replace(" ", "");
        var nombreUsuario = inicialNombre + restoNombreUsuario;
        return NombreUsuario.Create(nombreUsuario);
    }
}