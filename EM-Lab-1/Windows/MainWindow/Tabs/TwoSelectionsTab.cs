using ScottPlot.Plottable;
using System.Windows;

namespace EM_Lab_1;

public partial class TwoSelectionsTab
{
    private MarkerPlot? _linearComputedPoint;
    private MarkerPlot? _notLinearComputedPoint;

    public void VisualizeSelections(LinearRegressionContainer? linearRegressionContainer, NotLinearRegressionContainer? notLinearRegressionContainer)
    {
        if (linearRegressionContainer == null || notLinearRegressionContainer == null)
            VisualizeNoneSelections();
        else
            VisualizeExistingSelections(linearRegressionContainer, notLinearRegressionContainer);
    }

    public void VisualizeLinearComputings(LinearRegressionContainer linearRegressionContainer)
    {
        if (!double.TryParse(_linearInputXTextBox!.Text, out double x))
        {
            MessageBox.Show("Не вдалося зчитати х! (лінійна регресія)");
            return;
        }

        var y = linearRegressionContainer.RegressionFunction(x);
        var yTrustInterval = linearRegressionContainer.RegressionTrustIntervalFunction(x);

        _linearComputedRegressionValueTextBox!.Text = y.ToFormattedString();
        _linearComputedRegressionTrustIntervalTextBox!.Text = yTrustInterval.ToFormattedString();

        if (y != null)
        {
            _linearComputedPoint = _correlationFieldPlot!.Plot.AddPoint(x, y.Value, 
                Constants.PlotLinearComputedPointColor, size: Constants.ComputedPointsSize);

            _correlationFieldPlot!.Refresh();
        }
    }

    public void ClearLinearComputings()
    {
        _linearComputedRegressionValueTextBox!.Text = string.Empty;
        _linearComputedRegressionTrustIntervalTextBox!.Text = string.Empty;
        
        if (_linearComputedPoint != null)
        {
            _correlationFieldPlot!.Plot.Remove(_linearComputedPoint);
            _correlationFieldPlot!.Refresh();
            _linearComputedPoint = null;
        }
    }

    public void VisualizeNotLinearComputings(NotLinearRegressionContainer notLinearRegressionContainer)
    {
        if (!double.TryParse(_notLinearInputXTextBox!.Text, out double x))
        {
            MessageBox.Show("Не вдалося зчитати х! (нелінійна регресія)");
            return;
        }

        var y = notLinearRegressionContainer.RegressionFunction(x);
        var yTrustInterval = notLinearRegressionContainer.RegressionTrustIntervalFunction(x);

        _notLinearComputedRegressionValueTextBox!.Text = y.ToFormattedString();
        _notLinearComputedRegressionTrustIntervalTextBox!.Text = yTrustInterval.ToFormattedString();

        if (y != null)
        {
            _notLinearComputedPoint = _correlationFieldPlot!.Plot.AddPoint(x, y.Value,
                Constants.PlotNotLinearComputedPointColor, size: Constants.ComputedPointsSize);

            _correlationFieldPlot!.Refresh();
        }
    }

    public void ClearNotLinearComputings()
    {
        _notLinearComputedRegressionValueTextBox!.Text = string.Empty;
        _notLinearComputedRegressionTrustIntervalTextBox!.Text = string.Empty;

        if (_notLinearComputedPoint != null)
        {
            _correlationFieldPlot!.Plot.Remove(_notLinearComputedPoint);
            _correlationFieldPlot!.Refresh();
            _notLinearComputedPoint = null;
        }
    }

    private void VisualizeNoneSelections()
    {
        _correlationFieldPlot!.Plot.Clear();

        SetTextBoxesEmpty();
    }

    private void SetTextBoxesEmpty()
    {
        _pearsonValueTextBox!.Text = string.Empty;
        _pearsonIntervalTextBox!.Text = string.Empty;
        _pearsonStatisticsTextBox!.Text = string.Empty;
        _pearsonQuantileTextBox!.Text = string.Empty;
        _pearsonFirstConclusionTextBox!.Text = string.Empty;
        _pearsonSecondConclusionTextBox!.Text = string.Empty;
        _spearmanValueTextBox!.Text = string.Empty;
        _spearmanStatisticsTextBox!.Text = string.Empty;
        _spearmanQuantileTextBox!.Text = string.Empty;
        _spearmanFirstConclusionTextBox!.Text = string.Empty;
        _spearmanSecondConclusionTextBox!.Text = string.Empty;
        _kendallValueTextBox!.Text = string.Empty;
        _kendallStatisticsTextBox!.Text = string.Empty;
        _kendallQuantileTextBox!.Text = string.Empty;
        _kendallFirstConclusionTextBox!.Text = string.Empty;
        _kendallSecondConclusionTextBox!.Text = string.Empty;
        _corellationRatioValueTextBox!.Text = string.Empty;
        _corellationRatioStatisticsTextBox!.Text = string.Empty;
        _corellationRatioQuantileTextBox!.Text = string.Empty;
        _corellationRatioFirstConclusionTextBox!.Text = string.Empty;
        _corellationRatioSecondConclusionTextBox!.Text = string.Empty;
        _mainPearsonValueTextBox!.Text = string.Empty;
        _mainCorellationRatioValueTextBox!.Text = string.Empty;
        _mainStatisticsTextBox!.Text = string.Empty;
        _mainQuantileTextBox!.Text = string.Empty;
        _mainFirstConclusionTextBox!.Text = string.Empty;
        _mainSecondConclusionTextBox!.Text = string.Empty;

        ClearLinearComputings();
        ClearNotLinearComputings();
    }

    private void VisualizeExistingSelections(LinearRegressionContainer linearRegressionContainer, NotLinearRegressionContainer notLinearRegressionContainer)
    {
        VisualizePlot(linearRegressionContainer, notLinearRegressionContainer);
        VisualizePearson(linearRegressionContainer);
        VisualizeCorellationRation(linearRegressionContainer);
        VisualizeSpearman(linearRegressionContainer);
        VisualizeKendall(linearRegressionContainer);
        VisualizeMain(linearRegressionContainer);
        VisualizeLinearRegression(linearRegressionContainer);
        VisualizeNotLinearRegression(notLinearRegressionContainer);
    }

    private void VisualizePlot(LinearRegressionContainer linearRegressionContainer, NotLinearRegressionContainer notLinearRegressionContainer)
    {
        _correlationFieldPlot!.Plot.Clear();

        var pointsCount = linearRegressionContainer.FirstSelection.Values.Count;

        for (int i = 0; i < pointsCount; i++)
        {
            var x = linearRegressionContainer.FirstSelection.Values[i];
            var y = linearRegressionContainer.SecondSelection.Values[i];

            _correlationFieldPlot!.Plot.AddPoint(x, y, Constants.PlotPointsColor);
        }

        _correlationFieldPlot!.Plot.AddFunction(linearRegressionContainer.RegressionFunction, Constants.PlotLinearColor);
        _correlationFieldPlot!.Plot.AddFunction(notLinearRegressionContainer.RegressionFunction, Constants.PlotNotLinearColor);

        _correlationFieldPlot?.Refresh();
    }

    private void VisualizePearson(TwoSelectionsContainer twoSelectionsContainer)
    {
        _pearsonValueTextBox!.Text = twoSelectionsContainer.PearsonCoefficient.ToFormattedString();
        _pearsonIntervalTextBox!.Text = twoSelectionsContainer.PearsonCoefficientTrustInterval.ToFormattedString();
        _pearsonStatisticsTextBox!.Text = twoSelectionsContainer.PearsonStatistics.ToFormattedString();
        _pearsonQuantileTextBox!.Text = twoSelectionsContainer.StudentQuantile.ToFormattedString();

        var isCorellationSignificant = Math.Abs(twoSelectionsContainer.PearsonStatistics) > twoSelectionsContainer.StudentQuantile;

        var brush = isCorellationSignificant
            ? Constants.OkBrush
            : Constants.NotOkBrush;

        _pearsonFirstConclusionTextBox!.Background = brush;
        _pearsonSecondConclusionTextBox!.Background = brush;

        _pearsonFirstConclusionTextBox!.Text = isCorellationSignificant 
            ? "Кореляція значуща"
            : "Кореляція незначуща";

        _pearsonSecondConclusionTextBox!.Text = isCorellationSignificant
            ? "Є лінійний зв'язок"
            : "Лінійного зв'язку немає";
    }

    private void VisualizeCorellationRation(TwoSelectionsContainer twoSelectionsContainer)
    {
        _corellationRatioValueTextBox!.Text = twoSelectionsContainer.CorellationRatioYX.ToFormattedString();
        _corellationRatioStatisticsTextBox!.Text = twoSelectionsContainer.CorellationRatioYXStatistics.ToFormattedString();
        _corellationRatioQuantileTextBox!.Text = twoSelectionsContainer.FisherQuantile.ToFormattedString();

        var isCorellationSignificant = twoSelectionsContainer.CorellationRatioYXStatistics > twoSelectionsContainer.FisherQuantile;

        var brush = isCorellationSignificant
            ? Constants.OkBrush
            : Constants.NotOkBrush;

        _corellationRatioFirstConclusionTextBox!.Background = brush;
        _corellationRatioSecondConclusionTextBox!.Background = brush;

        _corellationRatioFirstConclusionTextBox!.Text = isCorellationSignificant
            ? "Кореляція значуща"
            : "Кореляція незначуща";

        _corellationRatioSecondConclusionTextBox!.Text = isCorellationSignificant 
            ? "Є стохастичний зв'язок"
            : "Стохастичного зв'язку немає";
    }

    private void VisualizeSpearman(TwoSelectionsContainer twoSelectionsContainer)
    {
        _spearmanValueTextBox!.Text = twoSelectionsContainer.SpearmanCoefficient.ToFormattedString();
        _spearmanStatisticsTextBox!.Text = twoSelectionsContainer.SpearmanStatistics.ToFormattedString();
        _spearmanQuantileTextBox!.Text = twoSelectionsContainer.StudentQuantile.ToFormattedString();

        var isCorellationSignificant = Math.Abs(twoSelectionsContainer.SpearmanStatistics) > twoSelectionsContainer.StudentQuantile;

        var brush = isCorellationSignificant
            ? Constants.OkBrush
            : Constants.NotOkBrush;

        _spearmanFirstConclusionTextBox!.Background = brush;
        _spearmanSecondConclusionTextBox!.Background = brush;

        _spearmanFirstConclusionTextBox!.Text = isCorellationSignificant
            ? "Кореляція значуща"
            : "Кореляція незначуща";

        _spearmanSecondConclusionTextBox!.Text = isCorellationSignificant 
            ? "Є монотонний зв'язок"
            : "Монотонного зв'язку немає";
    }

    private void VisualizeKendall(TwoSelectionsContainer twoSelectionsContainer)
    {
        _kendallValueTextBox!.Text = twoSelectionsContainer.KendallCoefficient.ToFormattedString();
        _kendallStatisticsTextBox!.Text = twoSelectionsContainer.KendallStatistics.ToFormattedString();
        _kendallQuantileTextBox!.Text = Constants.NormalDistributionQuantile.ToFormattedString();

        var isCorellationSignificant = Math.Abs(twoSelectionsContainer.KendallStatistics) > Constants.NormalDistributionQuantile;

        var brush = isCorellationSignificant
            ? Constants.OkBrush
            : Constants.NotOkBrush;

        _kendallFirstConclusionTextBox!.Background = brush;
        _kendallSecondConclusionTextBox!.Background = brush;

        _kendallFirstConclusionTextBox!.Text = isCorellationSignificant 
            ? "Кореляція значуща"
            : "Кореляція незначуща";

        _kendallSecondConclusionTextBox!.Text = isCorellationSignificant 
            ? "Є монотонний зв'язок"
            : "Монотонного зв'язку немає";
    }

    private void VisualizeMain(TwoSelectionsContainer twoSelectionsContainer)
    {
        _mainCorellationRatioValueTextBox!.Text = twoSelectionsContainer.CorellationRatioYX.ToFormattedString();
        _mainPearsonValueTextBox!.Text = twoSelectionsContainer.ClassifyingPerformedSelectionsContainer.PearsonCoefficient.ToFormattedString();
        _mainStatisticsTextBox!.Text = twoSelectionsContainer.ClassifyingPerformedSelectionsContainer.PearsonStatistics.ToFormattedString();
        _mainQuantileTextBox!.Text = twoSelectionsContainer.ClassifyingPerformedSelectionsContainer.StudentQuantile.ToFormattedString();

        var isCorellationSignificant = 
            Math.Abs(twoSelectionsContainer.ClassifyingPerformedSelectionsContainer.PearsonStatistics) 
            >
            twoSelectionsContainer.ClassifyingPerformedSelectionsContainer.StudentQuantile;

        var brush = isCorellationSignificant
            ? Constants.OkBrush
            : Constants.NotOkBrush;

        _mainFirstConclusionTextBox!.Background = brush;
        _mainSecondConclusionTextBox!.Background = brush;

        _mainFirstConclusionTextBox!.Text = isCorellationSignificant
            ? "Кореляція значуща"
            : "Кореляція незначуща";

        _mainSecondConclusionTextBox!.Text = isCorellationSignificant
            ? "Є лінійний зв'язок"
            : "Лінійного зв'язку немає";
    }

    private void VisualizeLinearRegression(LinearRegressionContainer linearRegressionContainer)
    {
        VisualizeLinearParameters(linearRegressionContainer);
        VisualizeLinear(linearRegressionContainer);
    }

    private void VisualizeNotLinearRegression(NotLinearRegressionContainer notLinearRegressionContainer)
    {
        VisualizeNotLinearParameters(notLinearRegressionContainer);
        VisualizeNotLinear(notLinearRegressionContainer);
    }

    private void VisualizeLinearParameters(LinearRegressionContainer linearRegressionContainer)
    {
        for (int i = 0; i < linearRegressionContainer.ParametersCount; i++)
        {
            _linearParametersValuesTextBoxes[i]!.Text = linearRegressionContainer.ParameterContainers[i].Value.ToFormattedString();
            _linearStandartDeviatonsTextBoxes[i]!.Text = linearRegressionContainer.ParameterContainers[i].StandardDeviation.ToFormattedString();
            _linearTrustIntervalsTextBoxes[i]!.Text = linearRegressionContainer.ParameterContainers[i].TrustInterval.ToFormattedString();
            _linearStatisticsTextBoxes[i]!.Text = linearRegressionContainer.ParameterContainers[i].Statistics.ToFormattedString();

            _linearQuantilesTextBoxes[i]!.Text = linearRegressionContainer.StudentQuantile.ToFormattedString();

            var isParameterZero =
                Math.Abs(linearRegressionContainer.ParameterContainers[i].Statistics)
                .IsLessOrEqual(
                linearRegressionContainer.StudentQuantile);

            _linearSignificancesTextBoxes[i]!.Background = isParameterZero
                ? Constants.OkBrush
                : Constants.NotOkBrush;

            _linearSignificancesTextBoxes[i]!.Text = isParameterZero
                ? $"A{i} = 0"
                : $"A{i} ≠ 0";
        }
    }

    private void VisualizeLinear(LinearRegressionContainer linearRegressionContainer)
    {
        _linearResidualVarianceTextBox!.Text = linearRegressionContainer.ResidualsVariance.ToFormattedString();
        _linearDeterminationCoefficientTextBox!.Text = linearRegressionContainer.DeterminationCoefficient.ToFormattedString();
        _linearFTestStatisticsTextBox!.Text = linearRegressionContainer.FTestStatistics.ToFormattedString();

        var quantile = linearRegressionContainer.FisherQuantile;

        _linearFTestQuantileTextBox!.Text = quantile.ToFormattedString();

        var significant = linearRegressionContainer.FTestStatistics > quantile;

        _linearFTestConclusionTextBox!.Background = significant
            ? Constants.OkBrush 
            : Constants.NotOkBrush;

        _linearFTestConclusionTextBox!.Text = significant
            ? "Регресія значуща"
            : "Регресія незначуща";

        //TODO: Fill other boxes
    }

    private void VisualizeNotLinearParameters(NotLinearRegressionContainer notLinearRegressionContainer)
    {
        for (int i = 0; i < notLinearRegressionContainer.ParametersCount; i++)
        {
            _notLinearParametersValuesTextBoxes[i]!.Text = notLinearRegressionContainer.ParameterContainers[i].Value.ToFormattedString();
            _notLinearStandartDeviatonsTextBoxes[i]!.Text = notLinearRegressionContainer.ParameterContainers[i].StandardDeviation.ToFormattedString();
            _notLinearTrustIntervalsTextBoxes[i]!.Text = notLinearRegressionContainer.ParameterContainers[i].TrustInterval.ToFormattedString();
            _notLinearStatisticsTextBoxes[i]!.Text = notLinearRegressionContainer.ParameterContainers[i].Statistics.ToFormattedString();

            _notLinearQuantilesTextBoxes[i]!.Text = notLinearRegressionContainer.StudentQuantile.ToFormattedString();

            var isParameterZero =
                Math.Abs(notLinearRegressionContainer.ParameterContainers[i].Statistics)
                .IsLessOrEqual(
                notLinearRegressionContainer.StudentQuantile);

            _notLinearSignificancesTextBoxes[i]!.Background = isParameterZero
                ? Constants.OkBrush
                : Constants.NotOkBrush;

            _notLinearSignificancesTextBoxes[i]!.Text = isParameterZero
                ? $"{Constants.NotLinearParametersNames[i]} = 0"
                : $"{Constants.NotLinearParametersNames[i]} ≠ 0";
        }
    }

    private void VisualizeNotLinear(NotLinearRegressionContainer notLinearRegressionContainer)
    {
        _notLinearResidualVarianceTextBox!.Text = notLinearRegressionContainer.ResidualsVariance.ToFormattedString();
        _notLinearDeterminationCoefficientTextBox!.Text = notLinearRegressionContainer.DeterminationCoefficient.ToFormattedString();
        _notLinearFTestStatisticsTextBox!.Text = notLinearRegressionContainer.FTestStatistics.ToFormattedString();

        //TODO: override FTest comptuings

        var quantile = notLinearRegressionContainer.FisherQuantile;

        _notLinearFTestQuantileTextBox!.Text = quantile.ToFormattedString();

        var significant = notLinearRegressionContainer.FTestStatistics > quantile;

        _notLinearFTestConclusionTextBox!.Background = significant
            ? Constants.OkBrush
            : Constants.NotOkBrush;

        _notLinearFTestConclusionTextBox!.Text = significant
            ? "Регресія значуща"
            : "Регресія незначуща";
    }
}
