namespace EM_Lab_1;

public class RegressionParameterContainer
{
    private double? _standardDeviation;
    private double? _statistics;

    private Interval? _trustInterval;

    public double Value { get; init; }
    public double Variance { get; init; }

    public double StandardDeviation
    {
        get
        {
            if (_standardDeviation == null)
                ComputeStandardDeviation();

            return _standardDeviation!.Value;
        }
    }

    public double Statistics
    {
        get
        {
            if (_statistics == null)
                ComputeStatistics();

            return _statistics!.Value;
        }
    }

    public Interval TrustInterval
    {
        get
        {
            if (_trustInterval == null)
                ComputeTrustInterval();

            return _trustInterval!.Value;
        }
    }

    private void ComputeStandardDeviation()
    {
        _standardDeviation = Math.Sqrt(Variance);
    }

    private void ComputeStatistics()
    {
        _statistics = Value / Math.Sqrt(Variance);
    }

    private void ComputeTrustInterval()
    {
        var sqrt = Math.Sqrt(Variance);

        _trustInterval = new Interval
        {
            LeftEdge = Value - Constants.NormalDistributionQuantile * sqrt,
            RightEdge = Value + Constants.NormalDistributionQuantile * sqrt
        };
    }
}
