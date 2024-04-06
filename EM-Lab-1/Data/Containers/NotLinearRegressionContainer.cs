using MathNet.Numerics.LinearAlgebra;

namespace EM_Lab_1;

public class NotLinearRegressionContainer : LinearRegressionContainer
{
    private double? _delta0;
    private double? _delta1;
    private double? _delta2;

    private double? _parameter0;
    private double? _parameter1;
    private double? _parameter2;

    private double? _variance0;
    private double? _variance1;
    private double? _variance2;

    private double? _statistics0;
    private double? _statistics1;
    private double? _statistics2;

    private Interval? _parameter0TrustInterval;
    private Interval? _parameter1TrustInterval;
    private Interval? _parameter2TrustInterval;

    private Matrix<double>? _aMatrix;
    private Vector<double>? _bVector;
    private Matrix<double>? _variancesMatrix;

    public double Delta0
    {
        get
        {
            if (_delta0 == null)
                ComputeDelta0();

            return _delta0!.Value;
        }
    }

    public double Delta1
    {
        get
        {
            if (_delta1 == null)
                ComputeDelta1();

            return _delta1!.Value;
        }
    }

    public double Delta2
    {
        get
        {
            if (_delta0 == null)
                ComputeDelta2();

            return _delta2!.Value;
        }
    }

    public double Parameter0
    {
        get
        {
            if (_parameter0 == 0)
                ComputeParameter0();

            return _parameter0!.Value;
        }
    }

    public double Parameter1
    {
        get
        {
            if (_parameter1 == 0)
                ComputeParameter1();

            return _parameter1!.Value;
        }
    }

    public double Parameter2
    {
        get
        {
            if (_parameter2 == 0)
                ComputeParameter2();

            return _parameter2!.Value;
        }
    }

    public double Variance0
    {
        get
        {
            if (_variance0 == null)
                ComputeVariance0();

            return _variance0!.Value;
        }
    }

    public double Variance1
    {
        get
        {
            if (_variance1 == null)
                ComputeVariance1();

            return _variance1!.Value;
        }
    }

    public double Variance2
    {
        get
        {
            if (_variance2 == null)
                ComputeVariance2();

            return _variance2!.Value;
        }
    }

    public double Statistics0
    {
        get
        {
            if (_statistics0 == null)
                ComputeStatistics0();

            return _statistics0!.Value;
        }
    }

    public double Statistics1
    {
        get
        {
            if (_statistics1 == null)
                ComputeStatistics1();

            return _statistics1!.Value;
        }
    }

    public double Statistics2
    {
        get
        {
            if (_statistics2 == null)
                ComputeStatistics2();

            return _statistics2!.Value;
        }
    }

    public Interval Parameter0TrustInterval
    {
        get
        {
            if (_parameter0TrustInterval == null)
                ComputeParameter0TrustInterval();

            return _parameter0TrustInterval!.Value;
        }
    }

    public Interval Parameter1TrustInterval
    {
        get
        {
            if (_parameter1TrustInterval == null)
                ComputeParameter1TrustInterval();

            return _parameter1TrustInterval!.Value;
        }
    }

    public Interval Parameter2TrustInterval
    {
        get
        {
            if (_parameter2TrustInterval == null)
                ComputeParameter2TrustInterval();

            return _parameter2TrustInterval!.Value;
        }
    }

    public Matrix<double> AMatrix
    {
        get
        {
            if (_aMatrix == null)
                ComputeAMatrix();

            return _aMatrix!;
        }
    }

    public Vector<double> BVector
    {
        get
        {
            if (_bVector == null)
                ComputeBVector();

            return _bVector!;
        }
    }

    public Matrix<double> VariancesMatrix
    {
        get
        {
            if (_variancesMatrix == null)
                ComputeVariancesMatrix();

            return _variancesMatrix!;
        }
    }

    #region Computing method
    private void ComputeDelta0()
    {
        _delta0 = Constants.MatrixBuilder.DenseOfArray
        (
            new double[,] 
            {
                { BVector[0], AMatrix[0, 1], AMatrix[0, 2]},
                { BVector[1], AMatrix[1, 1], AMatrix[1, 2]},
                { BVector[2], AMatrix[2, 1], AMatrix[2, 2]}
            }
        ).Determinant();
    }

    private void ComputeDelta1()
    {
        _delta0 = Constants.MatrixBuilder.DenseOfArray
        (
            new double[,]
            {
                { AMatrix[0, 0], BVector[0], AMatrix[0, 2]},
                { AMatrix[1, 0], BVector[1], AMatrix[1, 2]},
                { AMatrix[2, 0], BVector[2] ,AMatrix[2, 2]}
            }
        ).Determinant();
    }

    private void ComputeDelta2()
    {
        _delta0 = Constants.MatrixBuilder.DenseOfArray
        (
            new double[,]
            {
                { AMatrix[0, 0], AMatrix[0, 1], BVector[0] },
                { AMatrix[1, 0], AMatrix[1, 1], BVector[1] },
                { AMatrix[2, 0], AMatrix[2, 1], BVector[2] }
            }
        ).Determinant();
    }

    private void ComputeParameter0()
    {
        _parameter0 = Delta0 / AMatrix.Determinant();
    }

    private void ComputeParameter1()
    {
        _parameter1 = Delta1 / AMatrix.Determinant();
    }

    private void ComputeParameter2()
    {
        _parameter2 = Delta2 / AMatrix.Determinant();
    }

    private void ComputeVariance0()
    {
        _variance0 = VariancesMatrix[0, 0];
    }

    private void ComputeVariance1()
    {
        _variance1 = VariancesMatrix[1, 1];
    }

    private void ComputeVariance2()
    {
        _variance2 = VariancesMatrix[2, 2];
    }

    private void ComputeStatistics0()
    {
        _statistics0 = Parameter0 / Math.Sqrt(Variance0);
    }

    private void ComputeStatistics1()
    {
        _statistics1 = Parameter1 / Math.Sqrt(Variance1);
    }

    private void ComputeStatistics2()
    {
        _statistics2 = Parameter2 / Math.Sqrt(Variance2);
    }

    private void ComputeParameter0TrustInterval()
    {
        var sqrt = Math.Sqrt(Variance0);

        _parameter0TrustInterval = new Interval
        {
            LeftEdge = Parameter0 - Constants.NormalDistributionQuantile * sqrt,
            RightEdge = Parameter0 + Constants.NormalDistributionQuantile * sqrt
        };
    }

    private void ComputeParameter1TrustInterval()
    {
        var sqrt = Math.Sqrt(Variance1);

        _parameter1TrustInterval = new Interval
        {
            LeftEdge = Parameter1 - Constants.NormalDistributionQuantile * sqrt,
            RightEdge = Parameter1 + Constants.NormalDistributionQuantile * sqrt
        };
    }

    private void ComputeParameter2TrustInterval()
    {
        var sqrt = Math.Sqrt(Variance2);

        _parameter2TrustInterval = new Interval
        {
            LeftEdge = Parameter2 - Constants.NormalDistributionQuantile * sqrt,
            RightEdge = Parameter2 + Constants.NormalDistributionQuantile * sqrt
        };
    }

    private void ComputeAMatrix()
    {
        var normal = FirstSelection.Values.Sum();
        var squared = FirstSelection.Values.Sum(value => Math.Pow(value, 2));
        var cubed = FirstSelection.Values.Sum(value => Math.Pow(value, 3));
        var hipercubed = FirstSelection.Values.Sum(value => Math.Pow(value, 4));

        _aMatrix = Constants.MatrixBuilder.DenseOfArray(new double[,]
        {
            { ElementsCount, normal, squared },
            { normal, squared, cubed },
            { squared, cubed, hipercubed }
        });
    }

    private void ComputeBVector()
    {
        var bSingleY = SecondSelection.Values.Sum();
        var zipped = FirstSelection.Values.Zip(SecondSelection.Values).ToList();
        var bDefault = zipped.Sum(pair => pair.First * pair.Second);
        var bSquared = zipped.Sum(pair => pair.First * pair.First * pair.Second);

        _bVector = Constants.VectorBuilder.DenseOfArray([bSingleY, bDefault, bSquared]);
    }

    protected override void ComputeResidualsVariance()
    {
        var denominator = ElementsCount - 3.0D;

        var nominator = FirstSelection.Values
            .Zip(SecondSelection.Values, (x, y) => (x, y))
            .Sum(pair => Math.Pow(pair.y - Parameter0 - Parameter1 * pair.x - Parameter2 * pair.x * pair.x, 2));

        _residualsVariance = nominator / denominator;
    }

    private void ComputeVariancesMatrix()
    #endregion
}
