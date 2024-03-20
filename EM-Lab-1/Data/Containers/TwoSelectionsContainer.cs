namespace EM_Lab_1;

public class TwoSelectionsContainer
{
    private double? _multiplicationMean;
    private double? _studentQuantile;
    private double? _fisherQuantile;

    private double? _pearsonCoefficient;
    private double? _pearsonStatistics;
    private Interval? _pearsonCoefficientTrustInterval;

    private double? _spearmanCoefficient;
    private double? _spearmanStatistics;

    private double? _kendallCoefficient;
    private double? _kendallStatistics;

    private double? _corellationRatioYX;
    private double? _corellationRatioYXStatistics;

    private Dictionary<(double x, double y), (double xRank, double yRank)>? _ranks;
    private TwoSelectionsContainer? _classifyingPerformedSelectionsContainer;
    private readonly bool _isClassifyingReformed;

    public double MultiplicationMean
    {
        get
        {
            if (_multiplicationMean == null)
                ComputeMultiplicationMean();

            return _multiplicationMean!.Value;
        }
    }

    public double FisherQuantile
    {
        get
        {
            if (_fisherQuantile == null)
                ComputeFisherQuantile();

            return _fisherQuantile!.Value;
        }
    }

    public double StudentQuantile
    {
        get
        {
            if (_studentQuantile == null)
                ComputeStudentQuantile();

            return _studentQuantile!.Value;
        }
    }

    public int ClassesCount => FirstSelection.ClassesCount;

    public double PearsonCoefficient
    {
        get
        {
            if (_pearsonCoefficient == null)
                ComputePearsonCoefficient();

            return _pearsonCoefficient!.Value;
        }
    }

    public double PearsonStatistics
    {
        get
        {
            if (_pearsonStatistics == null)
                ComputePearsonStatistics();

            return _pearsonStatistics!.Value;
        }
    }

    public Interval PearsonCoefficientTrustInterval
    {
        get
        {
            if (_pearsonCoefficientTrustInterval == null)
                ComputePearsonCoefficientTrustInterval();

            return _pearsonCoefficientTrustInterval!.Value;
        }
    }

    public double SpearmanCoefficient
    {
        get
        {
            if (_spearmanCoefficient == null)
                ComputeSpearmanCoefficient();

            return _spearmanCoefficient!.Value;
        }
    }

    public double SpearmanStatistics
    {
        get
        {
            if (_spearmanStatistics == null)
                ComputeSpearmanStatistics();

            return _spearmanStatistics!.Value;
        }
    }

    public double KendallCoefficient
    {
        get
        {
            if (_kendallCoefficient == null)
                ComputeKendallCoefficient();

            return _kendallCoefficient!.Value;
        }
    }

    public double KendallStatistics
    {
        get
        {
            if (_kendallStatistics == null)
                ComputeKendallStatistics();

            return _kendallStatistics!.Value;
        }
    }

    public double CorellationRatioYX
    {
        get
        {
            if (_corellationRatioYX == null)
                ComputeCorellationRatioYX();

            return _corellationRatioYX!.Value;
        }
    }

    public double CorellationRatioYXStatistics
    {
        get
        {
            if (_corellationRatioYXStatistics == null)
                ComputeCorellationRatioYXStatistics();

            return _corellationRatioYXStatistics!.Value;
        }
    }

    public int ElementsCount => FirstSelection.ElementsCount;

    /// <summary>
    /// Ordered by x subranks
    /// </summary>
    public Dictionary<(double x, double y), (double xRank, double yRank)> Ranks
    {
        get
        {
            if (_ranks == null)
                ComputeRanks();

            return _ranks!;
        }
    }

    public TwoSelectionsContainer ClassifyingPerformedSelectionsContainer
    {
        get
        {
            if (_classifyingPerformedSelectionsContainer == null)
                ComputeClassifyingPerformedSelectionsContainer();

            return _classifyingPerformedSelectionsContainer!;
        }
    }

    public required SelectionContainer FirstSelection { get; init; }

    public required SelectionContainer SecondSelection { get; init; }

    public TwoSelectionsContainer(bool isClassifyingReformed) 
    {
        _isClassifyingReformed = isClassifyingReformed;
    }

    #region Computing methods
    private void ComputeMultiplicationMean()
    {
        _multiplicationMean = FirstSelection.Values
            .Zip(SecondSelection.Values, (x, y) => x * y)
            .Average();
    }

    private void ComputeFisherQuantile()
    {
        _fisherQuantile = Compute.FisherDistributionQuantile(1 - Constants.Alpha,
            ClassesCount - 1, ElementsCount - ClassesCount);
    }

    private void ComputeStudentQuantile()
    {
        _studentQuantile = Compute.StudentDistributionQuantile(1D - Constants.Alpha / 2, ElementsCount - 2);
    }

    private void ComputePearsonCoefficient()
    {
        _pearsonCoefficient = (MultiplicationMean - FirstSelection.Mean * SecondSelection.Mean)
            / (FirstSelection.StandardDeviation * SecondSelection.StandardDeviation);
    }

    private void ComputePearsonStatistics()
    {
        _pearsonStatistics =
            PearsonCoefficient * Math.Sqrt(ElementsCount - 2)
            /
            Math.Sqrt(1 - PearsonCoefficient * PearsonCoefficient);
    }

    private void ComputePearsonCoefficientTrustInterval()
    {
        var difference = (1 - PearsonCoefficient * PearsonCoefficient);

        var firstApplication =
            PearsonCoefficient +
            (PearsonCoefficient * difference)
            /
            (2 * ElementsCount);

        var secondMultiplier =
            difference
            /
            Math.Sqrt(ElementsCount - 1);

        var secondApplication = Constants.NormalDistributionQuantile * secondMultiplier;

        _pearsonCoefficientTrustInterval = new Interval(firstApplication - secondApplication, firstApplication + secondApplication);
    }

    private void ComputeRanks()
    {
        _ranks = new();

        for (int i = 0; i < FirstSelection.Values.Count; i++)
        {
            var x = FirstSelection.Values[i];
            var y = SecondSelection.Values[i];
            var xRank = FirstSelection.Ranks[x];
            var yRank = SecondSelection.Ranks[y];

            _ranks[(x, y)] = (xRank, yRank);
        }

        _ranks = _ranks.OrderBy(pair => pair.Value.xRank).ToDictionary();
    }

    private void ComputeClassifyingPerformedSelectionsContainer()
    {
        if (_isClassifyingReformed) throw new InvalidOperationException(
            "Trying to compute classifying performed selections container from another one!");

        _classifyingPerformedSelectionsContainer = new TwoSelectionsContainer(true)
        {
            FirstSelection = new() { Values = FirstSelection.ClassifiedValues },
            SecondSelection = new() { Values = SecondSelection.Values },
        };
    }

    private void ComputeSpearmanCoefficient()
    {
        var rx = Ranks.Values.Select(pair => pair.xRank).ToList();
        var ry = Ranks.Values.Select(pair => pair.yRank).ToList();

        var tempSelectionsContainer = new TwoSelectionsContainer(false)
        {
            FirstSelection = new() { Values = rx },
            SecondSelection = new() { Values = ry }
        };

        _spearmanCoefficient = tempSelectionsContainer.PearsonCoefficient;
    }

    private void ComputeSpearmanStatistics()
    {
        _spearmanStatistics =
            SpearmanCoefficient * Math.Sqrt(ElementsCount - 2)
            /
            Math.Sqrt(1 - SpearmanCoefficient * SpearmanCoefficient);
    }

    private void ComputeKendallCoefficient()
    {
        _kendallCoefficient = Compute.KendallCoefficient(FirstSelection.Values, SecondSelection.Values);
    }

    private void ComputeKendallStatistics()
    {
        _kendallStatistics =
            KendallCoefficient * Math.Sqrt(9 * ElementsCount * (ElementsCount - 1))
            /
            Math.Sqrt(2D * (2 * ElementsCount - 5));
    }

    private void ComputeCorellationRatioYX()
    {
        _corellationRatioYX = Compute.CorrelationRatio(ClassifyingPerformedSelectionsContainer.FirstSelection.Values, SecondSelection.Values);
    }

    private void ComputeCorellationRatioYXStatistics()
    {
        var nominator =
            (CorellationRatioYX * CorellationRatioYX)
            / 
            (ClassesCount - 1);

        var denominator =
            (1 - CorellationRatioYX * CorellationRatioYX)
            /
            (ElementsCount - ClassesCount);

        _corellationRatioYXStatistics = nominator / denominator;
    }
    #endregion
}
