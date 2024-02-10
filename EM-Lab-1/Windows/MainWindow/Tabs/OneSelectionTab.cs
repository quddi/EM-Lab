using System.ComponentModel;

namespace EM_Lab_1;

public partial class OneSelectionTab
{
    public void VisualizeSelection(SelectionContainer? container)
    {
        if (container == null) 
            SetTextBoxesEmpty();
        else
            VisualizeExistingSelection(container);

    }

    private void SetTextBoxesEmpty()
    {
        _meanTextBox!.Text = string.Empty;
        _meanIntervalTextBox!.Text = string.Empty;
        _medianTextBox!.Text = string.Empty;
        _medianIntervalTextBox!.Text = string.Empty;
        _standardDeviationTextBox!.Text = string.Empty;
        _standardDeviationIntervalTextBox!.Text = string.Empty;
        _skewnessTextBox!.Text = string.Empty;
        _skewnessIntervalTextBox!.Text = string.Empty;
        _kurtosisTextBox!.Text = string.Empty;
        _kurtosisIntervalTextBox!.Text = string.Empty;
    }

    private void VisualizeExistingSelection(SelectionContainer container)
    {
        _meanTextBox!.Text = container.Mean.ToFormattedString();
        _meanIntervalTextBox!.Text = container.MeanTrustInterval.ToFormattedString();

        _medianTextBox!.Text = container.Median.ToFormattedString();
        _medianIntervalTextBox!.Text = container.MedianTrustInterval.ToFormattedString();

        _standardDeviationTextBox!.Text = container.StandardDeviation.ToFormattedString();
        _standardDeviationIntervalTextBox!.Text = container.StandardDeviationTrustInterval.ToFormattedString();

        _skewnessTextBox!.Text = container.SecondSkewnessCoefficient.ToFormattedString();
        _skewnessIntervalTextBox!.Text = container.SecondSkewnessCoefficientTrustInterval.ToFormattedString();

        _kurtosisTextBox!.Text = container.SecondKurtosisCoefficient.ToFormattedString();
        _kurtosisIntervalTextBox!.Text = container.SecondKurtosisCoefficientTrustInterval.ToFormattedString();
    }
}
