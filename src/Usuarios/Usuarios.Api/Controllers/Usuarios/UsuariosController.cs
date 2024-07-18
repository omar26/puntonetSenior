using MediatR;
using Microsoft.AspNetCore.Mvc;
using Usuarios.Application.Usuarios.CrearUsuarios;
using Usuarios.Application.Usuarios.GetUsuarios;

namespace Usuarios.Api.Controllers.Usuarios;

[ApiController]
[Route("api/usuarios")]
public class UsuariosController : ControllerBase
{
    private readonly ISender _sender;

    public UsuariosController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObtenerUsuario(
        Guid id,
        CancellationToken cancellationToken
    )
    {
        var query = new GetUsuarioQuery(id);
        var resultado = await _sender.Send(query,cancellationToken);
        return resultado.IsSuccess ? Ok(resultado) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> CrearUsuario(
        CrearUsuarioRequest request,
        CancellationToken cancellationToken
    )
    {
        var command = new CrearUsuarioCommand
        (
            request.Contrasenia,
            request.Rol,
            request.Nombres,
            request.ApellidoPaterno,
            request.ApellidoMaterno,
            request.Pais,
            request.Departamento,
            request.Provincia,
            request.Ciudad,
            request.Calle,
            request.FechaNacimiento,
            request.CorreoElectronico
        );

        var resultado = await _sender.Send(command,cancellationToken);

        if (resultado.IsSuccess)
        {
            return CreatedAtAction(nameof(ObtenerUsuario), new { id = resultado.Value } );
        }
        return BadRequest(resultado.Error);
    }

}