using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Usuarios.Domain.Roles;

namespace Usuarios.Infrastructure.Configurations;

public class RolConfigurations : IEntityTypeConfiguration<Rol>
{
    public void Configure(EntityTypeBuilder<Rol> builder)
    {
        builder.ToTable("roles");
        builder.HasKey(x => x.Id);

        builder.Property(rol => rol.NombreRol)
        .HasMaxLength(20)
        .HasConversion(nombreRol => nombreRol!.Value, value => new NombreRol(value));

        builder.HasIndex(rol => rol.NombreRol).IsUnique();

        builder.Property(rol => rol.Descripcion)
        .HasMaxLength(50)
        .HasConversion(descripcion => descripcion!.Value, value => new Descripcion(value));

        builder.Property<uint>("Version").IsRowVersion();
    }
}