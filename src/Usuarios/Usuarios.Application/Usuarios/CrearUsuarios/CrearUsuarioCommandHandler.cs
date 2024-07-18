using Usuarios.Application.Abstractions.Clock;
using Usuarios.Application.Abstractions.Messaging;
using Usuarios.Domain.Abstractions;
using Usuarios.Domain.Roles;
using Usuarios.Domain.Usuarios;

namespace Usuarios.Application.Usuarios.CrearUsuarios;

internal sealed class CrearUsuarioCommandHandler :
ICommandHandler<CrearUsuarioCommand, Guid>
{

    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IRolRepository _rolRepository;
    private readonly NombreUsuarioService _nombreUsuarioService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;

    public CrearUsuarioCommandHandler(
        IUsuarioRepository usuarioRepository,
        IRolRepository rolRepository,
        NombreUsuarioService nombreUsuarioService,
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider
        )
    {
        _usuarioRepository = usuarioRepository;
        _rolRepository = rolRepository;
        _nombreUsuarioService = nombreUsuarioService;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;

    }

    public async Task<Result<Guid>> Handle(CrearUsuarioCommand request,
    CancellationToken cancellationToken)
    {
        var rol = await _rolRepository.GeByNameAsync(request.Rol,cancellationToken);

        if (rol is null)
        {
            return Result.Failure<Guid>(RolErrors.NoEncontrado);
        }
        var correoResult = CorreoElectronico.Create(request.CorreoElectronico);
        if (await _usuarioRepository.ExisteCorreoAsync(correoResult,cancellationToken))
        {
            return Result.Failure<Guid>(UsuarioErrores.ElCorreoYaExiste);
        }

        var usuario = Usuario.Create(
            Contrasenia.Create(request.Contrasenia)
            ,rol.Id
            ,new NombresPersona(request.Nombres)
            ,new ApellidoPaterno(request.ApellidoPaterno)
            ,new ApellidoMaterno(request.ApellidoMaterno)
            , new Direccion (
                request.Pais
                ,request.Departamento
                ,request.Provincia
                ,request.Ciudad
                ,request.Calle
                )
            ,request.FechaNacimiento.ToUniversalTime()
            ,_dateTimeProvider.CurrentTime
            ,CorreoElectronico.Create(request.CorreoElectronico)
            ,_nombreUsuarioService
        );

        _usuarioRepository.Add(usuario);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return usuario.Id;
    }
}