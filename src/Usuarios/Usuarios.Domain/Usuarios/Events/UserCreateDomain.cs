using Usuarios.Domain.Abstractions;

namespace Usuarios.Domain.Usuarios.Events;
public sealed record UserCreateDomain(Guid UserId) : IDomainEvent;