using Usuarios.Application.Abstractions.Clock;

namespace Usuarios.Infrastructure.Clock;

internal sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime CurrentTime => DateTime.UtcNow;
}