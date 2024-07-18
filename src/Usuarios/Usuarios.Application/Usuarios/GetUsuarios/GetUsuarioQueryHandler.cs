using Dapper;
using Usuarios.Application.Abstractions.Data;
using Usuarios.Application.Abstractions.Messaging;
using Usuarios.Domain.Abstractions;

namespace Usuarios.Application.Usuarios.GetUsuarios;

internal sealed class GetUsuarioQueryHandler : IQueryHandler<GetUsuarioQuery, UsuarioResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetUsuarioQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<UsuarioResponse>> Handle(
        GetUsuarioQuery request
        , CancellationToken cancellationToken)
    {
       using var connection = _sqlConnectionFactory.CreateConnection();

       var sql = """
       SELECT 
        u.id AS Id,
        u.nombre_usuario AS Nombres,
        u.apellido_paterno AS ApellidoPaterno,
        u.apellido_materno AS ApellidoMaterno,
        r.nombre_rol AS Rol,
        u.direccion_pais AS Pais,
        u.direccion_departamento AS Departamento,
        u.direccion_provincia AS Provincia,
        u.direccion_ciudad AS Ciudad,
        u.direccion_calle AS Calle,
        u.fecha_nacimiento AS FechaNacimiento,
        u.correo_electronico AS CorreoElectronico,
        u.estado AS Estado,
        u.fecha_ultimo_cambio AS FechaUltimoCambio
       FROM usuarios u
        INNER JOIN roles r ON u.rol_id = r.id
        WHERE u.id = @UsuarioId
       """;

       var usuario = await connection.QueryFirstOrDefaultAsync<UsuarioResponse>
       (
         sql,
         new {
            request.UsuarioId
         }
       );
        return usuario!;

    }
}