using Usuarios.Domain.Usuarios;

namespace Usuarios.Application.Abstractions.Email;

    public interface IEmailService
    {
        Task SendAsnyc(CorreoElectronico correoElectronico, string subject, string body);
    }
