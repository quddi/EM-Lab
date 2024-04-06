
namespace EM_Lab_1;

public class LinearRegressionContainer : TwoSelectionsContainer
{
    protected double? _interceptCoefficient;  //a0
    protected double? _interceptVariance;
    protected double? _interceptStandardDeviation;
    protected double? _interceptStatistics;
    protected Interval? _interceptTrustInterval;

    protected double? _slopeCoefficient;      //a1
    protected double? _slopeVariance;
    protected double? _slopeStandardDeviation;
    protected double? _slopeStatistics;
    protected Interval? _slopeTrustInterval;

    protected double? _fTestStatistics;

    protected double? _residualsVariance; //S_зал^2
    protected double? _determinationCoefficient; //R^2
    protected double? _sse;
    protected double? _sseConst;
    protected Func<double, double?>? _regressionFunction;
    protected Func<double, Interval?>? _regressionTrustIntervalFunction;

    public const int ParametersCount = 2;

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

    public double FTestStatistics
    {
        get
        {
            if (_fTestStatistics == null)
                ComputeFTestStatistics();

            return _fTestStatistics!.Value;
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

    public double DeterminationCoefficient
    {
        get
        {
            if (_determinationCoefficient == null)
                ComputeDeterminationCoefficient();

            return _determinationCoefficient!.Value;
        }
    }

    public double SSE
    {
        get
        {
            if (_sse == null)
                ComputeSSE();

            return _sse!.Value;
        }
    }

    public double SSEConst
    {
        get
        {
            if (_sseConst == null)
                ComputeSSEConst();

            return _sseConst!.Value;
        }
    }

    public Func<double, double?> RegressionFunction
    {
        get
        {
            if (_regressionFunction == null)
                ComputeRegressionFunction();

            return _regressionFunction!;
        }
    }

    public Func<double, Interval?> RegressionTrustIntervalFunction
    {
        get
        {
            if (_regressionTrustIntervalFunction == null)
                ComputeRegressionTrustIntervalFunction();

            return _regressionTrustIntervalFunction!;
        }
    }

    public LinearRegressionContainer() : base(isClassifyingReformed: false) { }

    #region Computing methods
    protected virtual void ComputeInterceptCoefficient()
    {
        _interceptCoefficient = SecondSelection.Mean - SlopeCoefficient * FirstSelection.Mean;
    }

    protected virtual void ComputeInterceptVariance()
    {
        var firstApplication = 1D / ElementsCount;

        var secondApplication = Math.Pow(FirstSelection.Mean, 2) / (ElementsCount * FirstSelection.Variance);

        _interceptVariance = ResidualsVariance * (firstApplication + secondApplication);
    }

    protected virtual void ComputeInterceptStandardDeviation()
    {
        _interceptStandardDeviation = Math.Sqrt(InterceptVariance);
    }

    protected virtual void ComputeInterceptStatistics()
    {
        _interceptStatistics = InterceptCoefficient / InterceptStandardDeviation;
    }

    protected virtual void ComputeInterceptTrustInterval()
    {
        var sqrt = Math.Sqrt(InterceptVariance);

        _interceptTrustInterval = new Interval
        {
            LeftEdge = InterceptCoefficient - Constants.NormalDistributionQuantile * sqrt,
            RightEdge = InterceptCoefficient + Constants.NormalDistributionQuantile * sqrt
        };
    }

    protected virtual void ComputeSlopeCoefficient()
    {
        _slopeCoefficient = PearsonCoefficient * SecondSelection.StandardDeviation 
            / FirstSelection.StandardDeviation;
    }

    protected virtual void ComputeSlopeVariance()
    {
        _slopeVariance = ResidualsVariance / (ElementsCount * FirstSelection.Variance);
    }

    protected virtual void ComputeSlopeStandardDeviation()
    {
        _slopeStandardDeviation = Math.Sqrt(SlopeVariance);
    }

    protected virtual void ComputeSlopeStatistics()
    {
        _slopeStatistics = SlopeCoefficient / SlopeStandardDeviation;
    }

    protected virtual void ComputeSlopeTrustInterval()
    {
        var sqrt = Math.Sqrt(SlopeVariance);

        _slopeTrustInterval = new Interval
        {
            LeftEdge = SlopeCoefficient - Constants.NormalDistributionQuantile * sqrt,
            RightEdge = SlopeCoefficient + Constants.NormalDistributionQuantile * sqrt
        };
    }

    protected virtual void ComputeFTestStatistics()
    {
        var nominator = (SSEConst - SSE) / (ParametersCount - 1);
        var denominator = SSE / (ElementsCount - ParametersCount);

        _fTestStatistics = nominator / denominator;
    }

    protected virtual void ComputeResidualsVariance()
    {
        var denominator = (ElementsCount - 2);

        var nominator = FirstSelection.Values
            .Zip(SecondSelection.Values, (x, y) => (x, y))
            .Sum(pair => Math.Pow(pair.y - InterceptCoefficient - SlopeCoefficient * pair.x, 2));

        _residualsVariance = nominator / denominator;
    }

    protected virtual void ComputeDeterminationCoefficient()
    {
        var nominator = (ElementsCount - ParametersCount) * ResidualsVariance;
        var demonimator = (ElementsCount - 1) * SecondSelection.Variance;

        _determinationCoefficient = 1 - nominator / demonimator;
    }

    protected virtual void ComputeSSE()
    {
        _sse = FirstSelection.Values
            .Zip(SecondSelection.Values, (x, y) => (x, y))
            .Sum(pair => Math.Pow(pair.y - RegressionFunction(pair.x)!.Value, 2));
    }

    protected virtual void ComputeSSEConst()
    {
        _sseConst = SecondSelection.Values
            .Sum(y => Math.Pow(y - SecondSelection.Mean, 2));
    }

    protected virtual void ComputeRegressionFunction()
    {
        _regressionFunction = x => InterceptCoefficient + SlopeCoefficient * x;
    }

    protected virtual void ComputeRegressionTrustIntervalFunction()
    {
        _regressionTrustIntervalFunction = x => new Interval(
            RegressionFunction(x)!.Value - StudentQuantile * Math.Sqrt(ResidualsVariance),
            RegressionFunction(x)!.Value + StudentQuantile * Math.Sqrt(ResidualsVariance)
            );
    }
    #endregion
}
