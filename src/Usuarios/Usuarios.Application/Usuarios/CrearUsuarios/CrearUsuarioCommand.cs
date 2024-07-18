using Usuarios.Application.Abstractions.Messaging;

namespace Usuarios.Application.Usuarios.CrearUsuarios;

public record CrearUsuarioCommand(
    string Contrasenia
   ,string Rol
   ,string Nombres
   ,string ApellidoPaterno
   ,string ApellidoMaterno
   ,string Pais
   ,string Departamento
   ,string Provincia
   ,string Ciudad
   ,string Calle
   ,DateTime FechaNacimiento
   ,string CorreoElectronico
) : ICommand<Guid>;

