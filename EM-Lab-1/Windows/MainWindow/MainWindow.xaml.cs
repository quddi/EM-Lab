using System.Windows;

namespace EM_Lab_1;

public partial class MainWindow : Window
{
    private OneSelectionTab? _oneSelectionTab;
    private TwoSelectionsTab? _twoSelectionsTab;

    private SelectionContainer? _firstSelectionContainer;
    private SelectionContainer? _secondSelectionContainer;

    public MainWindow()
    {
        InitializeComponent();
        InitializeTabs();
    }

    private void InitializeTabs()
    {
        InitializeOneSelectionTab();
        InitializeTwoSelectionsTab();
    }

    private void InitializeOneSelectionTab()
    {
        _oneSelectionTab = new OneSelectionTab()
            .SetMeanTextBoxes(MeanTextBox, MeanIntervalTextBox)
            .SetMedianTextBoxes(MedianTextBox, MedianIntervalTextBox)
            .SetStandardDeviationTextBoxes(StandardDeviationTextBox, StandardDeviationIntervalTextBox)
            .SetSkewnessTextBoxes(SkewnessTextBox, SkewnessIntervalTextBox)
            .SetKurtosisTextBoxes(KurtosisTextBox, KurtosisIntervalTextBox);

        SelectionComboBox.SelectedIndex = (int)SelectionNumber.None;
    }

    private void InitializeTwoSelectionsTab()
    {
        _twoSelectionsTab = new TwoSelectionsTab()
            .SetCorrelationFieldPlot(CorrelationFieldPlot);
    }

    private void SetOneSelection(SelectionNumber selectionNumber)
    {
        var selection = selectionNumber switch
        {
            SelectionNumber.None => null,
            SelectionNumber.First => _firstSelectionContainer!,
            SelectionNumber.Second => _secondSelectionContainer!,
            _ => throw new NotImplementedException($"Unknown {nameof(SelectionNumber)} value: {selectionNumber}!")
        };

        _oneSelectionTab?.VisualizeSelection(selection);
    }
}