﻿using System.Windows.Media;

namespace EM_Lab_1;

public partial class TwoSelectionsTab
{
    public void VisualizeSelections(TwoSelectionsContainer? twoSelectionsContainer)
    {
        if (twoSelectionsContainer == null)
            VisualizeNoneSelections();
        else
            VisualizeExistingSelections(twoSelectionsContainer);
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

    private void VisualizeExistingSelections(TwoSelectionsContainer twoSelectionsContainer)
    {
        VisualizePlot(twoSelectionsContainer);
        VisualizePearson(twoSelectionsContainer);
        VisualizeCorellationRation(twoSelectionsContainer);
        VisualizeSpearman(twoSelectionsContainer);
        VisualizeKendall(twoSelectionsContainer);
        VisualizeMain(twoSelectionsContainer);
    }

    private void VisualizePlot(TwoSelectionsContainer twoSelectionsContainer)
    {
        _correlationFieldPlot!.Plot.Clear();

        var pointsCount = twoSelectionsContainer.FirstSelection.Values.Count;
        var pointsColor = ExtensionsMethods.GetRandomColor();

        for (int i = 0; i < pointsCount; i++)
        {
            var x = twoSelectionsContainer.FirstSelection.Values[i];
            var y = twoSelectionsContainer.SecondSelection.Values[i];

            _correlationFieldPlot!.Plot.AddPoint(x, y, pointsColor);
        }

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
}
