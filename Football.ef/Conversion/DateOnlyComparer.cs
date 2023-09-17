namespace Football.EF.Conversion;

public class DateOnlyComparer : ValueComparer<DateOnly>
{
    public DateOnlyComparer() : base(
        (x, y) => x.Year == y.Year,
        dateOnly => dateOnly.GetHashCode())
    { }
}
