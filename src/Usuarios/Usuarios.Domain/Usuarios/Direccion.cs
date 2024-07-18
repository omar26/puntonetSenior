namespace Usuarios.Domain.Usuarios;

public record Direccion
(
    string Pais,
    string Departamento,
    string Provincia,
    string Ciudad,
    string Calle
);