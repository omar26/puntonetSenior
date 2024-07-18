using Dapper;
using Usuarios.Application.Abstractions.Data;
using Usuarios.Application.Abstractions.Messaging;
using Usuarios.Domain.Abstractions;

namespace Usuarios.Application.Roles;

internal sealed class GetRolesQueryHandler : IQueryHandler<GetRolesQuery, List<RolResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetRolesQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<List<RolResponse>>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();
        var sql = "SELECT * FROM public.sp_obtener_roles()";
        var roles = await connection.QueryAsync<RolResponse>(sql, commandType : System.Data.CommandType.Text);
        return Result<List<RolResponse>>.Success(roles.ToList());
    }
}
