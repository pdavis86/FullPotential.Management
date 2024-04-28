
namespace FullPotential.Management.Utilities;

public class SystemTimeProvider : ITimeProvider
{
    public DateTimeOffset GetUtcNow()
    {
        return TimeProvider.System.GetUtcNow();
    }
}
