namespace Usuarios.Domain.Usuarios;

public record Contrasenia
{
    public string Value { get; init; }

    private Contrasenia(string _value) => Value = _value;

    public static Contrasenia Create(string _value)
    {
        if (string.IsNullOrEmpty(_value) || _value.Length < 8)
        {
            throw new ApplicationException("La contrasenia es invalida");
        }
        return new Contrasenia(_value);
    }
}
