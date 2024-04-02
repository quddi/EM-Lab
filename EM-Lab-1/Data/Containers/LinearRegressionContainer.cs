
namespace EM_Lab_1;

public class LinearRegressionContainer : TwoSelectionsContainer
{
    private double? _interceptCoefficient;  //a0
    private double? _interceptVariance;
    private double? _interceptStandardDeviation;
    private double? _interceptStatistics;
    private Interval? _interceptTrustInterval;

    private double? _slopeCoefficient;      //a1
    private double? _slopeVariance;
    private double? _slopeStandardDeviation;
    private double? _slopeStatistics;
    private Interval? _slopeTrustInterval;

    private double? _residualsVariance; //S_зал^2
    private Func<double, double?>? _linearFunction;

    public double InterceptCoefficient
    {
        get
        {
            if (_interceptCoefficient == null)
                ComputeInterceptCoefficient();

            return _interceptCoefficient!.Value;
        }
    }
    public double InterceptVariance
    {
        get
        {
            if (_interceptVariance == null)
                ComputeInterceptVariance();

            return _interceptVariance!.Value;
        }
    }
    public double InterceptStandardDeviation
    {
        get
        {
            if (_interceptStandardDeviation == null)
                ComputeInterceptStandardDeviation();

            return _interceptStandardDeviation!.Value;
        }
    }
    public double InterceptStatistics
    {
        get
        {
            if (_interceptStatistics == null)
                ComputeInterceptStatistics();

            return _interceptStatistics!.Value;
        }
    }
    public Interval InterceptTrustInterval
    {
        get
        {
            if (_interceptTrustInterval == null)
                ComputeInterceptTrustInterval();

            return _interceptTrustInterval!.Value;
        }
    }

    public double SlopeCoefficient
    {
        get
        {
            if (_slopeCoefficient == null)
                ComputeSlopeCoefficient();

            return _slopeCoefficient!.Value;
        }
    }
    public double SlopeVariance
    {
        get
        {
            if (_slopeVariance == null)
                ComputeSlopeVariance();

            return _slopeVariance!.Value;
        }
    }
    public double SlopeStandardDeviation
    {
        get
        {
            if (_slopeStandardDeviation == null)
                ComputeSlopeStandardDeviation();

            return _slopeStandardDeviation!.Value;
        }
    }
    public double SlopeStatistics
    {
        get
        {
            if (_slopeStatistics == null)
                ComputeSlopeStatistics();

            return _slopeStatistics!.Value;
        }
    }
    public Interval SlopeTrustInterval
    {
        get
        {
            if (_slopeTrustInterval == null)
                ComputeSlopeTrustInterval();

            return _slopeTrustInterval!.Value;
        }
    }

    public double ResidualsVariance
    {
        get
        {
            if (_residualsVariance == null)
                ComputeResidualsVariance();

            return _residualsVariance!.Value;
        }
    }

    public Func<double, double?> LinearFunction
    {
        get
        {
            if (_linearFunction == null)
                ComputeLinearFunction();

            return _linearFunction!;
        }
    }

    public LinearRegressionContainer() : base(isClassifyingReformed: false) { }

    #region Computing methods
    private void ComputeInterceptCoefficient()
    {
        _interceptCoefficient = SecondSelection.Mean - SlopeCoefficient * FirstSelection.Mean;
    }

    private void ComputeInterceptVariance()
    {
        var firstApplication = 1D / ElementsCount;

        var secondApplication = Math.Pow(FirstSelection.Mean, 2) / (ElementsCount * FirstSelection.Variance);

        _interceptVariance = ResidualsVariance * (firstApplication + secondApplication);
    }

    private void ComputeInterceptStandardDeviation()
    {
        _interceptStandardDeviation = Math.Sqrt(InterceptVariance);
    }

    private void ComputeInterceptStatistics()
    {
        _interceptStatistics = InterceptCoefficient / InterceptStandardDeviation;
    }

    private void ComputeInterceptTrustInterval()
    {
        var sqrt = Math.Sqrt(InterceptVariance);

        _interceptTrustInterval = new Interval
        {
            LeftEdge = InterceptCoefficient - Constants.NormalDistributionQuantile * sqrt,
            RightEdge = InterceptCoefficient + Constants.NormalDistributionQuantile * sqrt
        };
    }

    private void ComputeSlopeCoefficient()
    {
        _slopeCoefficient = PearsonCoefficient * SecondSelection.StandardDeviation 
            / FirstSelection.StandardDeviation;
    }

    private void ComputeSlopeVariance()
    {
        _slopeVariance = ResidualsVariance / (ElementsCount * FirstSelection.Variance);
    }

    private void ComputeSlopeStandardDeviation()
    {
        _slopeStandardDeviation = Math.Sqrt(SlopeVariance);
    }

    private void ComputeSlopeStatistics()
    {
        _slopeStatistics = SlopeCoefficient / SlopeStandardDeviation;
    }

    private void ComputeSlopeTrustInterval()
    {
        var sqrt = Math.Sqrt(SlopeVariance);

        _slopeTrustInterval = new Interval
        {
            LeftEdge = SlopeCoefficient - Constants.NormalDistributionQuantile * sqrt,
            RightEdge = SlopeCoefficient + Constants.NormalDistributionQuantile * sqrt
        };
    }

    private void ComputeResidualsVariance()
    {
        var denominator = 1.0D / (ElementsCount - 2);

        var nominator = FirstSelection.Values
            .Zip(SecondSelection.Values, (x, y) => (x, y))
            .Sum(pair => Math.Pow(pair.y - InterceptCoefficient - SlopeCoefficient * pair.x, 2));

        _residualsVariance = nominator / denominator;
    }

    private void ComputeLinearFunction()
    {
        _linearFunction = x => InterceptCoefficient + SlopeCoefficient * x;
    }
    #endregion
}
