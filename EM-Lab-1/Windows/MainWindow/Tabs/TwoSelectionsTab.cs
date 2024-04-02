using System.Windows.Media;

namespace EM_Lab_1;

public partial class TwoSelectionsTab
{
    public void VisualizeSelections(LinearRegressionContainer? linearRegressionContainer)
    {
        if (linearRegressionContainer == null)
            VisualizeNoneSelections();
        else
            VisualizeExistingSelections(linearRegressionContainer);
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
    }

    private void VisualizeExistingSelections(LinearRegressionContainer linearRegressionContainer)
    {
        VisualizePlot(linearRegressionContainer);
        VisualizePearson(linearRegressionContainer);
        VisualizeCorellationRation(linearRegressionContainer);
        VisualizeSpearman(linearRegressionContainer);
        VisualizeKendall(linearRegressionContainer);
        VisualizeMain(linearRegressionContainer);
        VisualizeLinearRegression(linearRegressionContainer);
    }

    private void VisualizePlot(LinearRegressionContainer linearRegressionContainer)
    {
        _correlationFieldPlot!.Plot.Clear();

        var pointsCount = linearRegressionContainer.FirstSelection.Values.Count;

        for (int i = 0; i < pointsCount; i++)
        {
            var x = linearRegressionContainer.FirstSelection.Values[i];
            var y = linearRegressionContainer.SecondSelection.Values[i];

            _correlationFieldPlot!.Plot.AddPoint(x, y, Constants.PlotPointsColor);
        }

        _correlationFieldPlot!.Plot.AddFunction(linearRegressionContainer.LinearFunction, Constants.PlotLineColor);

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
        VisualizeA0(linearRegressionContainer);
        VisualizeA1(linearRegressionContainer);
    }

    private void VisualizeA0(LinearRegressionContainer linearRegressionContainer)
    {
        _valueA0TextBox!.Text = linearRegressionContainer.InterceptCoefficient.ToFormattedString();
        _standartDeviatonA0TextBox!.Text = linearRegressionContainer.InterceptStandardDeviation.ToFormattedString();
        _trustIntervalA0TextBox!.Text = linearRegressionContainer.InterceptTrustInterval.ToFormattedString();
        _statisticsA0TextBox!.Text = linearRegressionContainer.InterceptStatistics.ToFormattedString();

        //???
        _quantileA0TextBox!.Text = linearRegressionContainer.StudentQuantile.ToFormattedString();

        var isA0Zero =
            Math.Abs(linearRegressionContainer.InterceptStatistics)
            .IsLessOrEqual(
            linearRegressionContainer.StudentQuantile);

        _significanceA0TextBox!.Background = isA0Zero
            ? Constants.OkBrush
            : Constants.NotOkBrush;

        _significanceA0TextBox!.Text = isA0Zero
            ? "A0 = 0"
            : "A0 ≠ 0";
    }

    private void VisualizeA1(LinearRegressionContainer linearRegressionContainer)
    {
        _valueA1TextBox!.Text = linearRegressionContainer.SlopeCoefficient.ToFormattedString();
        _standartDeviatonA1TextBox!.Text = linearRegressionContainer.SlopeStandardDeviation.ToFormattedString();
        _trustIntervalA1TextBox!.Text = linearRegressionContainer.SlopeTrustInterval.ToFormattedString();
        _statisticsA1TextBox!.Text = linearRegressionContainer.SlopeStatistics.ToFormattedString();

        //???
        _quantileA1TextBox!.Text = linearRegressionContainer.StudentQuantile.ToFormattedString();

        var isA1Zero =
            Math.Abs(linearRegressionContainer.SlopeStatistics)
            .IsLessOrEqual(
            linearRegressionContainer.StudentQuantile);

        _significanceA1TextBox!.Background = isA1Zero
            ? Constants.OkBrush
            : Constants.NotOkBrush;

        _significanceA1TextBox!.Text = isA1Zero
            ? "A1 = 0"
            : "A1 ≠ 0";
    }
}
