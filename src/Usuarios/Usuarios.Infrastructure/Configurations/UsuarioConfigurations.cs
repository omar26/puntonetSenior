using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Usuarios.Domain.Roles;
using Usuarios.Domain.Usuarios;

namespace Usuarios.Infrastructure.Configurations;

internal sealed class UsuarioConfigurations : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("usuarios");
        builder.HasKey(x => x.Id); //PK

        builder.OwnsOne(usuario => usuario.Direccion);

        builder.Property(usuario => usuario.NombreUsuario)
        .HasMaxLength(200)
        .HasConversion(nombreUsuario => nombreUsuario!.Value, value => NombreUsuario.Create(value));

        builder.Property(usuario => usuario.NombresPersona)
        .HasMaxLength(100)
        .HasConversion(nombresPersona => nombresPersona!.Value, value => new NombresPersona(value));

        builder.Property(usuario => usuario.ApellidoPaterno)
        .HasMaxLength(100)
        .HasConversion(apellidoPaterno => apellidoPaterno!.Value, value => new ApellidoPaterno(value));

        builder.Property(usuario => usuario.ApellidoMaterno)
        .HasMaxLength(100)
        .HasConversion(apellidoMaterno => apellidoMaterno!.Value, value => new ApellidoMaterno(value));

        builder.Property(usuario => usuario.CorreoElectronico)
        .HasMaxLength(50)
        .HasConversion(correoElectronico => correoElectronico!.Value, value => CorreoElectronico.Create(value));

        builder.HasIndex(usuario => usuario.CorreoElectronico).IsUnique(); // constraint para que sea unico

        builder.Property(usuario => usuario.Contrasenia)
        .HasMaxLength(50)
        .HasConversion(contrasenia => contrasenia!.Value, value => Contrasenia.Create(value));
        
        builder.Property(usuario => usuario.FechaNacimiento);

        builder.Property(usuario => usuario.Estado)
        .HasConversion(
            estado => estado.ToString(),
            estado => (UsuarioEstado)Enum.Parse(typeof(UsuarioEstado), estado)
        );

        builder.Property(usuario => usuario.FechaUltimoCambio);

        builder.HasOne(usuario => usuario.Rol)
        .WithMany()
        .HasForeignKey(usuario => usuario.RolId)
        .IsRequired();

        builder.Property<uint>("Version").IsRowVersion();

    }
}