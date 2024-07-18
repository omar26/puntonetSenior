using System.Text.RegularExpressions;

namespace Usuarios.Domain.Usuarios;

public record CorreoElectronico
{
    public string Value { get; init; }

    private CorreoElectronico(string _value) => Value = _value;

    public static implicit operator string(CorreoElectronico d) => d.Value;


    public static CorreoElectronico Create(string _value)
    {
        if(!EsCorreoValido(_value))
        {
            throw new InvalidOperationException("El correo electronico no es valido");
        }
        return new CorreoElectronico(_value);
    }

    private static bool EsCorreoValido(string correo)
    {
        const string emailRegex = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$";
        if(string.IsNullOrEmpty(correo))
        {
            return false;
        }

        var esCorrecto = Regex.Match(correo, emailRegex).Success;

        if(esCorrecto)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
