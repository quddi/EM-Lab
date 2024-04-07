using MathNet.Numerics.LinearAlgebra;

namespace EM_Lab_1;

public class NotLinearRegressionContainer : LinearRegressionContainer
{
    private double[]? _deltas;

    private Matrix<double>? _aMatrix;
    private Vector<double>? _bVector;
    private Matrix<double>? _variancesMatrix;

    public override int ParametersCount => 3;

    public double[] Deltas
    {
        get
        {
            if (_deltas == null)
                ComputeDeltas();

            return _deltas!;
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
    private void ComputeDeltas()
    {
        _deltas = new double[ParametersCount];

        for (int i = 0; i < ParametersCount; i++)
        {
            Matrix<double> matrix = Constants.MatrixBuilder.DenseDiagonal(3, 3, 0);
            
            AMatrix.CopyTo(matrix);

            for (int j = 0; j < ParametersCount; j++)
                matrix[j, i] = BVector[j];

            _deltas[i] = matrix.Determinant();
        }
    }

    protected override void ComputeParametersContainers()
    {
        _parameterContainers = new RegressionParameterContainer[ParametersCount];

        var determinant = AMatrix.Determinant();

        for (int i = 0; i < ParametersCount; i++)
            _parameterContainers[i] = new() { Value = Deltas[i] / determinant };

        for (int i = 0; i < ParametersCount; i++)
            _parameterContainers[i].Variance = VariancesMatrix[i, i];
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
            .Sum(pair => Math.Pow(pair.y 
            - ParameterContainers[0].Value 
            - ParameterContainers[1].Value * pair.x 
            - ParameterContainers[2].Value * pair.x * pair.x, 2));

        _residualsVariance = nominator / denominator;
    }

    private void ComputeVariancesMatrix()
    {
        _variancesMatrix = ResidualsVariance * AMatrix.Inverse();
    }

    protected override void ComputeRegressionFunction()
    {
        _regressionFunction = x => 
            ParameterContainers[0].Value + 
            ParameterContainers[1].Value * x +
            ParameterContainers[2].Value * x * x;
    }
    #endregion
}
