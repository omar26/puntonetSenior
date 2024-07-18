using Bogus;
using Bogus.DataSets;
using Dapper;
using Usuarios.Application.Abstractions.Data;
using Usuarios.Domain.Roles;

namespace Usuarios.Api.Extensions;

public static class SeedDataExtensions
{
    public static void SeedData(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var sqlConnectionFactory = scope.ServiceProvider.GetRequiredService<ISqlConnectionFactory>();
        using var connection = sqlConnectionFactory.CreateConnection();

        List<object> roles;

        const string sqlContarRoles = "SELECT COUNT(*) FROM public.roles";

        int totalRoles = connection.ExecuteScalar<int>(sqlContarRoles);

        if (totalRoles == 0)
        {
            roles = [
                new {
                    Id = Guid.NewGuid(),
                    Nombre = "Docente",
                    Descripcion = "Usuario a cargo del dictado de los cursos"
                },
                new {
                    Id = Guid.NewGuid(),
                    Nombre = "Estudiante",
                    Descripcion = "Usuario inscrito en los cursos a dictar"
                }
            ];

            const string sqlInsertRoles = """
             INSERT INTO public.roles 
                (id,nombre_rol,descripcion)
                values (@Id,@Nombre,@Descripcion)
            """;    

            connection.Execute(sqlInsertRoles,roles);

            List<object> usuarios = new();

            for (int i = 0; i < 100; i++)
            {
                var fake = new Faker("es");

                int indexRolAleatorio = fake.Random.Int(0,roles.Count - 1);
                var maximoFechaNacimiento = fake.Date.Past(50);

                var usuario = new 
                {
                    id = Guid.NewGuid(),
                    nombre_usuario = fake.Person.UserName,
                    contrasenia = fake.Person.UserName,
                    rol_id = (Guid)((dynamic)roles[indexRolAleatorio]).Id,
                    nombres_persona = fake.Person.FirstName,
                    apellido_paterno = fake.Person.LastName,
                    apellido_materno = fake.Person.LastName,
                    direccion_pais = fake.Address.Country(),
                    direccion_departamento = fake.Address.City(),
                    direccion_provincia = fake.Address.City(),
                    direccion_ciudad = fake.Address.StreetName(),
                    direccion_calle = fake.Address.StreetAddress(),
                    fecha_nacimiento = fake.Date.Between(maximoFechaNacimiento,DateTime.UtcNow),
                    correo_electronico = fake.Person.Email,
                    estado = fake.PickRandom("Activo","Inactivo"),
                    fecha_ultimo_cambio = DateTime.UtcNow
                };

                usuarios.Add(usuario);
            }


           const string sqlInsertUsuarios = """
            INSERT INTO public.usuarios
                (id,nombre_usuario,contrasenia,rol_id,nombres_persona,apellido_paterno,apellido_materno,direccion_pais,direccion_departamento,direccion_provincia,direccion_ciudad,direccion_calle,fecha_nacimiento,correo_electronico,estado,fecha_ultimo_cambio)
            values (@id,@nombre_usuario,@contrasenia,@rol_id,@nombres_persona,@apellido_paterno,@apellido_materno,@direccion_pais,@direccion_departamento,@direccion_provincia,@direccion_ciudad,@direccion_calle,@fecha_nacimiento,@correo_electronico,@estado,@fecha_ultimo_cambio)
           """;

            connection.Execute(sqlInsertUsuarios,usuarios);
        }

    }
}