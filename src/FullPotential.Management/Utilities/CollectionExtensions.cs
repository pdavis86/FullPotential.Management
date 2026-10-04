namespace FullPotential.Management.Utilities;

public static class CollectionExtensions
{
    public static void MatchOrOtherwise<T>(
        this ICollection<T> collection,
        Func<T, bool> predicate,
        Action<T> handleMatch,
        Action handleOtherwise)
    {
        var match = collection.FirstOrDefault(predicate);
        if (match != null)
        {
            handleMatch(match);
        }
        else
        {
            handleOtherwise();
        }
    }
}
