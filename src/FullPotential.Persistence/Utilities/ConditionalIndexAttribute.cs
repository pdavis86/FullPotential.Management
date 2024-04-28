namespace FullPotential.Persistence.Utilities;

public class ConditionalIndexAttribute : Attribute
{
    public string ColumnName {get;set;}

    public string Filter { get; set; }

    public bool IsUnique {get;set;}

    public ConditionalIndexAttribute(string columnName, string filter, bool isUnique = false)
    {
        ColumnName = columnName;
        Filter = filter;
        IsUnique = isUnique;
    }
}
