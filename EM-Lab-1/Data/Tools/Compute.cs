namespace EM_Lab_1;

using static Math;
using Point = (double x, double y);

public static class Compute
{
    public static double Variance(IReadOnlyCollection<double> values, double mean)
    {
        var sum = values.Select(z =>
        {
            var difference = z - mean;

            return difference * difference;
        }).Sum();

        return sum / (values.Count - 1);
    }

    public static double StandardDeviation(double variance)
    {
        return Sqrt(variance);
    }

    public static double StudentDistributionQuantile(double p, double v)
    {
        var uP = NormalDistributionQuantile(p);

        var g1 = Compute.G1(uP);
        var g2 = Compute.G2(uP);
        var g3 = Compute.G3(uP);
        var g4 = Compute.G4(uP);

        return uP + (g1 / v) + (g2 / Pow(v, 2)) + (g3 / Pow(v, 3)) + (g4 / Pow(v, 4));
    }

    public static double NormalDistributionQuantile(double p)
    {
        return p.IsLessOrEqual(0.5)
            ? -1.0 * QuantilePhi(p)
            : QuantilePhi(1.0 - p);
    }

    public static double QuantilePhi(double a)
    {
        var t = Compute.QuantileT(a);

        var numerator =
            Constants.C0 +
            Constants.C1 * t +
            Constants.C2 * t * t;

        var denominator =
            1 +
            Constants.D1 * t +
            Constants.D2 * t * t +
            Constants.D3 * t * t * t;

        return t - (numerator / denominator);
    }

    public static double FisherDistributionQuantile(double p, double v1, double v2)
    {
        var delta = (1 / v1) - (1 / v2);
        var delta2 = delta * delta;
        var delta3 = delta2 * delta;
        var delta4 = delta3 * delta;
        var sigma = (1 / v1) + (1 / v2);
        var sigmaHalfSqrt = Sqrt(sigma / 2);
        var normalQuantile = Compute.NormalDistributionQuantile(p);
        var normalQuantile2 = normalQuantile * normalQuantile;
        var normalQuantile3 = normalQuantile2 * normalQuantile;
        var normalQuantile4 = normalQuantile3 * normalQuantile;
        var normalQuantile5 = normalQuantile4 * normalQuantile;

        var s1 = normalQuantile * sigmaHalfSqrt;
        var s2 = -1 * delta * (normalQuantile2 + 2) / 6;
        var s3 = sigmaHalfSqrt *
                (sigma * (normalQuantile2 + 3 * normalQuantile) / 24 +
                (delta2 * (normalQuantile3 + 11 * normalQuantile)) / (72 * sigma));
        var s4 = -1 * delta * sigma * (normalQuantile4 + 9 * normalQuantile2 + 8) / 120;
        var s5 = delta3 * (3 * normalQuantile4 + 7 * normalQuantile2 - 16) / (3240 * sigma);

        var s6_1 = sigma * sigma * (normalQuantile5 + 20 * normalQuantile3 + 15 * normalQuantile) / 1920;
        var s6_2 = delta4 * (normalQuantile5 + 44 * normalQuantile3 + 183 * normalQuantile) / 2880;
        var s6_3 = delta4 * (9 * normalQuantile5 - 284 * normalQuantile3 - 1513 * normalQuantile) / (155520 * sigma * sigma);
        var s6 = sigmaHalfSqrt * (s6_1 + s6_2 + s6_3);

        return Exp((s1 + s2 + s3 + s4 + s5 + s6) * 2);
    }

    private static double QuantileT(double a)
    {
        return Sqrt(-2 * Log(a));
    }

    public static double MeanSquaredStandardDeviation(double S, int N)
    {
        return S / Sqrt(N);
    }

    public static double SampleMeanSquaredStandardDeviation(double S, int N)
    {
        return S / Sqrt(2.0 * N);
    }

    public static double SkewnessCoefficientRootMeanSquareDeviation(int N)
    {
        return Sqrt(
        (6.0 * N * (N - 1))
        /
        (((double)N - 2) * (N + 1) * (N + 3))
        );
    }

    public static double KurtosisCoefficientRootMeanSquareDeviation(int N)
    {
        return Sqrt(
        (24.0 * N * (N - 1) * (N - 1))
        /
        (N - 3D)
        /
        (N - 2D)
        /
        (N + 3D)
        /
        (N + 5D));
    }

    public static double G1(double uP)
    {
        return (uP * uP * uP + uP) / 4;
    }

    public static double G2(double uP)
    {
        return (5 * Pow(uP, 5) + 16 * Pow(uP, 3) + 3 * uP) / 96;
    }

    public static double G3(double uP)
    {
        return (3 * Pow(uP, 7) + 19 * Pow(uP, 5) + 17 * Pow(uP, 3) - 15 * uP) / 384;
    }

    public static double G4(double uP)
    {
        return (79 * Pow(uP, 9) + 779 * Pow(uP, 7) + 1482 * Pow(uP, 5) - 1920 * Pow(uP, 3) - 945 * uP) / 92160;
    }

    public static Dictionary<double, double> Ranks(List<double> values)
    {
        var orderedValues = values.Order().ToList();

        var previous = orderedValues[0];
        var positions = new List<int>();

        var result = new Dictionary<double, double>();

        for (int i = 0; i < orderedValues.Count; i++)
        {
            var current = orderedValues[i];

            if (i == 0 || current == previous)
            {
                positions.Add(i + 1);
                continue;
            }

            result.Add(previous, positions.Average());
            positions.Clear();
            positions.Add(i + 1);

            previous = current;

            if (i + 1 == orderedValues.Count)
                result.Add(previous, positions.Average());
        }

        return result;
    }

    public static double CorrelationRatio(List<double> x, List<double> y)
    {
        var partition = x
            .Zip(y)
            .GroupBy(pair => pair.First, pair => pair.Second)
            .ToList();

        var avgY = y.Average();
        var denominator = y.Sum(yi => Pow(yi - avgY, 2));

        var numerator = partition.Sum(c => c.Count() * Pow(c.Average() - avgY, 2));

        return Sqrt(numerator / denominator);
    }

    public static int ClassesCount(int elementsCount)
    {
        return (int)Ceiling(1 + 1.44 * Log(elementsCount));
    }

    public static List<double> ClassifiedValues(List<double> xAxis, int classesCount)
    {
        var max = xAxis.Max();
        var min = xAxis.Min();
        var classWidth = (max - min) / classesCount;

        return xAxis
            .Select(value => MiddleValueSelector(GetClassIndexByValue(value)))
            .ToList();

        double GetClassIndexByValue(double x)
        {
            return x.IsEqual(max) ? classesCount : (int)Floor((x - min) / classWidth + 1);
        }

        double MiddleValueSelector(double k)
        {
            return min + (k - 0.5) * classWidth;
        }
    }

    public static double KendallCoefficient(List<double> x, List<double> y)
    {
        var points = x
            .Zip(y, (xi, yi) => new Point(xi, yi))
            .ToArray();

        var allPointsCombinations = points
            .SelectMany((point1, i) => points
                .Skip(i + 1)
                .Select(point2 => (point1, point2)));

        var tiesDictionary = allPointsCombinations
            .GroupBy(pointsPair => GetTieValue(pointsPair.point1, pointsPair.point2))
            .ToDictionary(grouping => grouping.Key, grouping => grouping.Count());

        double nc = tiesDictionary.GetValueOrDefault(1, 0);
        double nd = tiesDictionary.GetValueOrDefault(-1, 0);
        double nx = tiesDictionary.GetValueOrDefault(0.5, 0);
        double ny = tiesDictionary.GetValueOrDefault(-0.5, 0);

        return (nc - nd) / (Sqrt(nc + nd + nx) * Sqrt(nc + nd + ny));

        double GetTieValue(Point p1, Point p2)
        {
            if (Concordant(p1, p2)) return 1;

            if (Discordant(p1, p2)) return -1;

            var isTieByX = IsTieByX(p1, p2);
            var isTieByY = IsTieByY(p1, p2);

            return isTieByX switch
            {
                true when isTieByY => 0,
                true => 0.5,
                _ => -0.5
            };
        }
        bool IsTieByX(Point p1, Point p2)
        {
            return p1.x.IsEqual(p2.x);
        }
        bool IsTieByY(Point p1, Point p2)
        {
            return p1.y.IsEqual(p2.y);
        }
        bool Discordant(Point p1, Point p2)
        {
            return (p1.x < p2.x && p1.y > p2.y) || (p1.x > p2.x && p1.y < p2.y);
        }
        bool Concordant(Point p1, Point p2)
        {
            return (p1.x > p2.x && p1.y > p2.y) || (p1.x < p2.x && p1.y < p2.y);
        }
    }
}
