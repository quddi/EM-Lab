namespace EM_Lab_1;

public class RegressionParameterContainer
{
    private double _significanceQuantile;

    private double? _standardDeviation;
    private double? _statistics;
    private bool? _isSignificant;

    private Interval? _trustInterval;

    public double Value { get; init; }
    public double Variance { get; set; }

    public double SignificanceQuantile => _significanceQuantile;

    public double ResultValue => IsSignificant ? Value : 0;

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

    public bool IsSignificant
    {
        get
        {
            if (_isSignificant == null)
                ComputeIsSignificant();

            return _isSignificant!.Value;
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

    public RegressionParameterContainer(double significanceQuantile)
    {
        _significanceQuantile = significanceQuantile; 
    }

    private void ComputeStandardDeviation()
    {
        _standardDeviation = Math.Sqrt(Variance);
    }

    private void ComputeStatistics()
    {
        _statistics = Value / Math.Sqrt(Variance);
    }

    private void ComputeIsSignificant()
    {
        _isSignificant = Statistics.IsLessOrEqual(_significanceQuantile);
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
