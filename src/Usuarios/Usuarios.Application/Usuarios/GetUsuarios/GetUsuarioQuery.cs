using Usuarios.Application.Abstractions.Messaging;

namespace Usuarios.Application.Usuarios.GetUsuarios;

public sealed record GetUsuarioQuery(Guid UsuarioId) : IQuery<UsuarioResponse>;