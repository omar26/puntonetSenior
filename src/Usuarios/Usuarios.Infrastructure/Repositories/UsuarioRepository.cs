using Microsoft.EntityFrameworkCore;
using Usuarios.Domain.Usuarios;

namespace Usuarios.Infrastructure.Repositories;

internal sealed class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
{
    public UsuarioRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        
    }

    public async Task<bool> ExisteCorreoAsync(CorreoElectronico correoElectronico, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<Usuario>()
        .AnyAsync(usuario => ((string)usuario.CorreoElectronico!) == correoElectronico.Value, cancellationToken);
    }
}