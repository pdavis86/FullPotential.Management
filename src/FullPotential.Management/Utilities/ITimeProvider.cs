namespace FullPotential.Management.Utilities;

public interface ITimeProvider
{
    DateTimeOffset GetUtcNow();
}
