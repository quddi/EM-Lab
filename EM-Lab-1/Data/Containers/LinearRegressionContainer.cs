
using System.DirectoryServices.ActiveDirectory;

namespace EM_Lab_1;

public class LinearRegressionContainer : TwoSelectionsContainer
{
    protected RegressionParameterContainer[]? _parameterContainers;

    protected double? _fTestStatistics;

    protected double? _residualsVariance; //S_зал^2
    protected double? _determinationCoefficient; //R^2
    protected double? _correctedDeterminationCoefficient;
    protected double? _sse;
    protected double? _sseConst;
    protected Func<double, double?>? _regressionFunction;
    protected Func<double, Interval?>? _regressionTrustIntervalFunction;

    public virtual int ParametersCount => 2;
    public RegressionParameterContainer[] ParameterContainers
    {
        get
        {
            if (_parameterContainers == null)
                ComputeParametersContainers();

            return _parameterContainers!;
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

    public double CorrectedDeterminationCoefficient
    {
        get
        {
            if (_correctedDeterminationCoefficient == null)
                ComputeCorrectedDeterminationCoefficient();

            return _correctedDeterminationCoefficient!.Value;
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
    protected virtual void ComputeParametersContainers()
    {
        _parameterContainers = new RegressionParameterContainer[ParametersCount];

        _parameterContainers[1] = new(StudentQuantile) { Value = PearsonCoefficient * SecondSelection.StandardDeviation / FirstSelection.StandardDeviation };

        _parameterContainers[0] = new(StudentQuantile) { Value = SecondSelection.Mean - _parameterContainers[1].Value * FirstSelection.Mean };

        _parameterContainers[0].Variance = ResidualsVariance / (ElementsCount * FirstSelection.Variance);
        _parameterContainers[1].Variance = ResidualsVariance * (1D / ElementsCount + Math.Pow(FirstSelection.Mean, 2) / (ElementsCount * FirstSelection.Variance));
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
            .Sum(pair => Math.Pow(pair.y - ParameterContainers[0].Value - ParameterContainers[1].Value * pair.x, 2));

        _residualsVariance = nominator / denominator;
    }

    protected virtual void ComputeDeterminationCoefficient()
    {
        var nominator = (ElementsCount - ParametersCount) * ResidualsVariance;
        var demonimator = (ElementsCount - 1) * SecondSelection.Variance;

        _determinationCoefficient = 1 - nominator / demonimator;
    }

    protected virtual void ComputeCorrectedDeterminationCoefficient()
    {
        _correctedDeterminationCoefficient = 1D - ResidualsVariance / SecondSelection.Variance;
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
        _regressionFunction = x => ParameterContainers[0].Value + ParameterContainers[1].Value * x;
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
