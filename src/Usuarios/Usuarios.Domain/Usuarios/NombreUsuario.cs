namespace Usuarios.Domain.Usuarios;

public record NombreUsuario
{
    public string Value { get; init; }

    private NombreUsuario(string _value)
    {
        Value = _value;
    }

    public static NombreUsuario Create(string _value)
    {
       if (string.IsNullOrWhiteSpace(_value))
       {
            throw new InvalidOperationException("El nombre de usuario no puede ser vacio.");
       }  
       return new NombreUsuario(_value); 
    }

}
