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

    private TextBox?[] _linearParametersValuesTextBoxes = new TextBox?[2];
    private TextBox?[] _linearStandartDeviatonsTextBoxes = new TextBox?[2];
    private TextBox?[] _linearTrustIntervalsTextBoxes = new TextBox?[2];
    private TextBox?[] _linearStatisticsTextBoxes = new TextBox?[2];
    private TextBox?[] _linearQuantilesTextBoxes = new TextBox?[2];
    private TextBox?[] _linearSignificancesTextBoxes = new TextBox?[2];

    private TextBox?[] _notLinearParametersValuesTextBoxes = new TextBox?[3];
    private TextBox?[] _notLinearStandartDeviatonsTextBoxes = new TextBox?[3];
    private TextBox?[] _notLinearTrustIntervalsTextBoxes = new TextBox?[3];
    private TextBox?[] _notLinearStatisticsTextBoxes = new TextBox?[3];
    private TextBox?[] _notLinearQuantilesTextBoxes = new TextBox?[3];
    private TextBox?[] _notLinearSignificancesTextBoxes = new TextBox?[3];

    private TextBox? _linearResidualVarianceTextBox;
    private TextBox? _linearDeterminationCoefficientTextBox;
    private TextBox? _linearCorrectedDeterminationCoefficientTextBox;
    private TextBox? _linearFTestStatisticsTextBox;
    private TextBox? _linearFTestQuantileTextBox;
    private TextBox? _linearFTestConclusionTextBox;
    private TextBox? _linearInputXTextBox;
    private TextBox? _linearComputedRegressionValueTextBox;
    private TextBox? _linearComputedRegressionTrustIntervalTextBox;

    private TextBox? _notLinearResidualVarianceTextBox;
    private TextBox? _notLinearDeterminationCoefficientTextBox;
    private TextBox? _notLinearCorrectedDeterminationCoefficientTextBox;
    private TextBox? _notLinearFTestStatisticsTextBox;
    private TextBox? _notLinearFTestQuantileTextBox;
    private TextBox? _notLinearFTestConclusionTextBox;
    private TextBox? _notLinearInputXTextBox;
    private TextBox? _notLinearComputedRegressionValueTextBox;
    private TextBox? _notLinearComputedRegressionTrustIntervalTextBox;

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

    public TwoSelectionsTab SetA0TextBoxes(TextBox valueA0TextBox, TextBox standartDeviatonA0TextBox, 
        TextBox trustIntervalA0TextBox, TextBox statisticsA0TextBox, TextBox quantileA0TextBox, 
        TextBox significanceA0TextBox)
    {
        _linearParametersValuesTextBoxes[0] = valueA0TextBox;
        _linearStandartDeviatonsTextBoxes[0] = standartDeviatonA0TextBox;
        _linearTrustIntervalsTextBoxes[0] = trustIntervalA0TextBox;
        _linearStatisticsTextBoxes[0] = statisticsA0TextBox;
        _linearQuantilesTextBoxes[0] = quantileA0TextBox;
        _linearSignificancesTextBoxes[0] = significanceA0TextBox;

        return this;
    }

    public TwoSelectionsTab SetA1TextBoxes(TextBox valueA1TextBox, TextBox standartDeviatonA1TextBox,
        TextBox trustIntervalA1TextBox, TextBox statisticsA1TextBox, TextBox quantileA1TextBox,
        TextBox significanceA1TextBox)
    {
        _linearParametersValuesTextBoxes[1] = valueA1TextBox;
        _linearStandartDeviatonsTextBoxes[1] = standartDeviatonA1TextBox;
        _linearTrustIntervalsTextBoxes[1] = trustIntervalA1TextBox;
        _linearStatisticsTextBoxes[1] = statisticsA1TextBox;
        _linearQuantilesTextBoxes[1] = quantileA1TextBox;
        _linearSignificancesTextBoxes[1] = significanceA1TextBox;

        return this;
    }

    public TwoSelectionsTab SetATextBoxes(TextBox valueATextBox, TextBox standartDeviatonATextBox,
        TextBox trustIntervalATextBox, TextBox statisticsATextBox, TextBox quantileATextBox,
        TextBox significanceATextBox)
    {
        _notLinearParametersValuesTextBoxes[0] = valueATextBox;
        _notLinearStandartDeviatonsTextBoxes[0] = standartDeviatonATextBox;
        _notLinearTrustIntervalsTextBoxes[0] = trustIntervalATextBox;
        _notLinearStatisticsTextBoxes[0] = statisticsATextBox;
        _notLinearQuantilesTextBoxes[0] = quantileATextBox;
        _notLinearSignificancesTextBoxes[0] = significanceATextBox;

        return this;
    }

    public TwoSelectionsTab SetBTextBoxes(TextBox valueBTextBox, TextBox standartDeviatonBTextBox,
        TextBox trustIntervalBTextBox, TextBox statisticsBTextBox, TextBox quantileBTextBox,
        TextBox significanceBTextBox)
    {
        _notLinearParametersValuesTextBoxes[1] = valueBTextBox;
        _notLinearStandartDeviatonsTextBoxes[1] = standartDeviatonBTextBox;
        _notLinearTrustIntervalsTextBoxes[1] = trustIntervalBTextBox;
        _notLinearStatisticsTextBoxes[1] = statisticsBTextBox;
        _notLinearQuantilesTextBoxes[1] = quantileBTextBox;
        _notLinearSignificancesTextBoxes[1] = significanceBTextBox;

        return this;
    }

    public TwoSelectionsTab SetCTextBoxes(TextBox valueCTextBox, TextBox standartDeviatonCTextBox,
        TextBox trustIntervalCTextBox, TextBox statisticsCTextBox, TextBox quantileCTextBox,
        TextBox significanceCTextBox)
    {
        _notLinearParametersValuesTextBoxes[2] = valueCTextBox;
        _notLinearStandartDeviatonsTextBoxes[2] = standartDeviatonCTextBox;
        _notLinearTrustIntervalsTextBoxes[2] = trustIntervalCTextBox;
        _notLinearStatisticsTextBoxes[2] = statisticsCTextBox;
        _notLinearQuantilesTextBoxes[2] = quantileCTextBox;
        _notLinearSignificancesTextBoxes[2] = significanceCTextBox;

        return this;
    }

    public TwoSelectionsTab SetLinearTextBoxes(TextBox linearResidualVarianceTextBox, TextBox linearDeterminationCoefficientTextBox,
        TextBox linearCorrectedDeterminationCoefficientTextBox, TextBox linearFTestStatisticsTextBox, TextBox linearFTestQuantileTextBox, 
        TextBox linearFTestConclusionTextBox, TextBox linearInputXTextBox, TextBox linearComputedRegressionValueTextBox, TextBox linearComputedRegressionTrustIntervalTextBox)
    {
        _linearResidualVarianceTextBox = linearResidualVarianceTextBox;
        _linearDeterminationCoefficientTextBox = linearDeterminationCoefficientTextBox;
        _linearCorrectedDeterminationCoefficientTextBox = linearCorrectedDeterminationCoefficientTextBox;
        _linearFTestStatisticsTextBox = linearFTestStatisticsTextBox;
        _linearFTestQuantileTextBox = linearFTestQuantileTextBox;
        _linearFTestConclusionTextBox = linearFTestConclusionTextBox;
        _linearInputXTextBox = linearInputXTextBox;
        _linearComputedRegressionValueTextBox = linearComputedRegressionValueTextBox;
        _linearComputedRegressionTrustIntervalTextBox = linearComputedRegressionTrustIntervalTextBox;

        return this;
    }

    public TwoSelectionsTab SetNotLinearTextBoxes(TextBox notLinearResidualVarianceTextBox, TextBox notLinearDeterminationCoefficientTextBox,
        TextBox notLinearCorrectedDeterminationCoefficientTextBox, TextBox notLinearFTestStatisticsTextBox, TextBox notLinearFTestQuantileTextBox, 
        TextBox notLinearFTestConclusionTextBox, TextBox notLinearInputXTextBox, TextBox notLinearComputedRegressionValueTextBox, TextBox notLinearComputedRegressionTrustIntervalTextBox)
    {
        _notLinearResidualVarianceTextBox = notLinearResidualVarianceTextBox;
        _notLinearDeterminationCoefficientTextBox = notLinearDeterminationCoefficientTextBox;
        _notLinearCorrectedDeterminationCoefficientTextBox = notLinearCorrectedDeterminationCoefficientTextBox;
        _notLinearFTestStatisticsTextBox = notLinearFTestStatisticsTextBox;
        _notLinearFTestQuantileTextBox = notLinearFTestQuantileTextBox;
        _notLinearFTestConclusionTextBox = notLinearFTestConclusionTextBox;
        _notLinearInputXTextBox = notLinearInputXTextBox;
        _notLinearComputedRegressionValueTextBox = notLinearComputedRegressionValueTextBox;
        _notLinearComputedRegressionTrustIntervalTextBox = notLinearComputedRegressionTrustIntervalTextBox;

        return this;
    }
}
