using ScottPlot;
using System.Windows.Controls;

namespace EM_Lab_1;

public partial class TwoSelectionsTab
{
    private WpfPlot? _correlationFieldPlot;

    private TextBox? _pearsonValueTextBox;
    private TextBox? _pearsonIntervalTextBox;
    private TextBox? _pearsonStatisticsTextBox;
    private TextBox? _pearsonQuantileTextBox;
    private TextBox? _pearsonFirstConclusionTextBox;
    private TextBox? _pearsonSecondConclusionTextBox;

    private TextBox? _spearmanValueTextBox;
    private TextBox? _spearmanStatisticsTextBox;
    private TextBox? _spearmanQuantileTextBox;
    private TextBox? _spearmanFirstConclusionTextBox;
    private TextBox? _spearmanSecondConclusionTextBox;

    private TextBox? _kendallValueTextBox;
    private TextBox? _kendallStatisticsTextBox;
    private TextBox? _kendallQuantileTextBox;
    private TextBox? _kendallFirstConclusionTextBox;
    private TextBox? _kendallSecondConclusionTextBox;

    private TextBox? _corellationRatioValueTextBox;
    private TextBox? _corellationRatioStatisticsTextBox;
    private TextBox? _corellationRatioQuantileTextBox;
    private TextBox? _corellationRatioFirstConclusionTextBox;
    private TextBox? _corellationRatioSecondConclusionTextBox;

    private TextBox? _mainPearsonValueTextBox;
    private TextBox? _mainCorellationRatioValueTextBox;
    private TextBox? _mainStatisticsTextBox;
    private TextBox? _mainQuantileTextBox;
    private TextBox? _mainFirstConclusionTextBox;
    private TextBox? _mainSecondConclusionTextBox;

    public TwoSelectionsTab SetCorrelationFieldPlot(WpfPlot correlationFieldPlot)
    {
        _correlationFieldPlot = correlationFieldPlot;

        _correlationFieldPlot.Plot.Title("Кореляційне поле");
        _correlationFieldPlot.Plot.XLabel("X");
        _correlationFieldPlot.Plot.YLabel("Y");

        return this;
    }

    public TwoSelectionsTab SetPearsonTextBoxes(TextBox pearsonValueTextBox, TextBox pearsonIntervalTextBox, TextBox pearsonStatisticsTextBox, 
        TextBox pearsonQuantileTextBox, TextBox pearsonFirstConclusionTextBox, TextBox pearsonSecondConclusionTextBox)
    {
        _pearsonValueTextBox = pearsonValueTextBox;
        _pearsonIntervalTextBox = pearsonIntervalTextBox;
        _pearsonStatisticsTextBox = pearsonStatisticsTextBox;
        _pearsonQuantileTextBox = pearsonQuantileTextBox;
        _pearsonFirstConclusionTextBox = pearsonFirstConclusionTextBox;
        _pearsonSecondConclusionTextBox = pearsonSecondConclusionTextBox;

        return this;
    }

    public TwoSelectionsTab SetCorellationRatioTextBoxes(TextBox corellationRatioValueTextBox, TextBox corellationRatioStatisticsTextBox, 
        TextBox corellationRatioQuantileTextBox, TextBox corellationRatioFirstConclusionTextBox, 
        TextBox corellationRatioSecondConclusionTextBox)
    {
        _corellationRatioValueTextBox = corellationRatioValueTextBox;
        _corellationRatioStatisticsTextBox = corellationRatioStatisticsTextBox;
        _corellationRatioQuantileTextBox = corellationRatioQuantileTextBox;
        _corellationRatioFirstConclusionTextBox = corellationRatioFirstConclusionTextBox;
        _corellationRatioSecondConclusionTextBox = corellationRatioSecondConclusionTextBox;

        return this;
    }

    public TwoSelectionsTab SetSpearmanTextBoxes(TextBox spearmanValueTextBox, TextBox spearmanStatisticsTextBox, TextBox spearmanQuantileTextBox, 
        TextBox spearmanFirstConclusionTextBox, TextBox spearmanSecondConclusionTextBox)
    {
        _spearmanValueTextBox = spearmanValueTextBox;
        _spearmanStatisticsTextBox = spearmanStatisticsTextBox;
        _spearmanQuantileTextBox = spearmanQuantileTextBox;
        _spearmanFirstConclusionTextBox = spearmanFirstConclusionTextBox;
        _spearmanSecondConclusionTextBox = spearmanSecondConclusionTextBox;

        return this;
    }

    public TwoSelectionsTab SetKendallTextBoxes(TextBox kendallValueTextBox, TextBox kendallStatisticsTextBox, TextBox kendallQuantileTextBox, 
        TextBox kendallFirstConclusionTextBox, TextBox kendallSecondConclusionTextBox)
    {
        _kendallValueTextBox = kendallValueTextBox;
        _kendallStatisticsTextBox = kendallStatisticsTextBox;
        _kendallQuantileTextBox = kendallQuantileTextBox;
        _kendallFirstConclusionTextBox = kendallFirstConclusionTextBox;
        _kendallSecondConclusionTextBox = kendallSecondConclusionTextBox;

        return this;
    }

    public TwoSelectionsTab SetMainTextBoxes(TextBox mainPearsonValueTextBox, TextBox mainCorellationRatioValueTextBox, 
        TextBox mainStatisticsTextBox, TextBox mainQuantileTextBox, TextBox mainFirstConclusionTextBox, 
        TextBox mainSecondConclusionTextBox)
    {
        _mainPearsonValueTextBox = mainPearsonValueTextBox;
        _mainCorellationRatioValueTextBox = mainCorellationRatioValueTextBox;
        _mainStatisticsTextBox = mainStatisticsTextBox;
        _mainQuantileTextBox = mainQuantileTextBox;
        _mainFirstConclusionTextBox = mainFirstConclusionTextBox;
        _mainSecondConclusionTextBox = mainSecondConclusionTextBox;

        return this;
    }
}
