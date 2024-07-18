namespace Usuarios.Api.Controllers.Usuarios;

public record CrearUsuarioRequest(
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
);