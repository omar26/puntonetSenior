using Usuarios.Domain.Abstractions;

namespace Usuarios.Domain.Usuarios;

    public class UsuarioErrores
    {
        public static Error YaSeEncuentraActivo = new (
            "Usuario.YaSeEncuentraActivo",
            "El usuario ya se encuentra activo, no se puede activar."
            );
        public static Error YaSeEncuentraInactivo = new (
            "Usuario.YaSeEncuentraInactivo",
            "El usuario ya se encuentra activo, no se puede inactivar."
            );

        public static Error ElCorreoYaExiste = new (
            "Usuario.ElCorreoYaExiste",
            "El correo electronico ya existe"
            );
    }
