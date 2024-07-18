using MediatR;
using Usuarios.Application.Abstractions.Email;
using Usuarios.Domain.Usuarios;
using Usuarios.Domain.Usuarios.Events;

namespace Usuarios.Application.Usuarios.CrearUsuarios;

internal sealed class CrearUsuarioDomainEventHandler : INotificationHandler<UserCreateDomainEvent>
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IEmailService _emailService;

    public CrearUsuarioDomainEventHandler(
        IUsuarioRepository usuarioRepository,
        IEmailService emailService
        )
    {
        _usuarioRepository = usuarioRepository;
        _emailService = emailService;
    }

    public async Task Handle(UserCreateDomainEvent notification, CancellationToken cancellationToken)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(notification.UserId,cancellationToken);
        if (usuario is null)
        {
            return;
        }
        await _emailService.SendAsnyc
        (
            usuario.CorreoElectronico!,
            "Bienvenido al sistema",
            $"Su usuario {usuario.NombreUsuario} fue creado a las {usuario.FechaUltimoCambio}."
        );

    }
}
