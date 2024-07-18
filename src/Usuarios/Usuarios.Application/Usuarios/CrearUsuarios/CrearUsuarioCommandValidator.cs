using FluentValidation;

namespace Usuarios.Application.Usuarios.CrearUsuarios;

public class CrearUsuarioCommandValidator : AbstractValidator<CrearUsuarioCommand>
{
    public CrearUsuarioCommandValidator()
    {
        RuleFor(c => c.ApellidoPaterno).NotEmpty();
        RuleFor(c => c.Nombres).NotEmpty();
        RuleFor(c => c.Rol).NotEmpty();
        RuleFor(c => c.FechaNacimiento).LessThan(DateTime.UtcNow);
    }
}