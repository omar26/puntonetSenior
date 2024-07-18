using Usuarios.Application.Abstractions.Messaging;

namespace Usuarios.Application.Roles;

public sealed record GetRolesQuery(): IQuery<List<RolResponse>>;
