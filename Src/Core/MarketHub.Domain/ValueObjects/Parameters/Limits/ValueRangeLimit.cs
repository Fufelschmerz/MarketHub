namespace MarketHub.Domain.ValueObjects.Parameters.Limits;

public sealed class ValueRangeLimit
{
    public ValueRangeLimit(int min,
        int max)
    {
        if (min < 0)
            throw new ArgumentOutOfRangeException(nameof(min));

        if (max < 0 || min > max)
            throw new ArgumentOutOfRangeException(nameof(max));

        Min = min;
        Max = max;
    }
    
    public int Min { get; private set; }
    
    public int Max { get; private set; }
}