namespace EM_Lab_1;

public partial class TwoSelectionsTab
{
    public void VisualizeSelections(SelectionContainer? firstSelection, SelectionContainer? secondSelection)
    {
        if (firstSelection == null && secondSelection == null)
            VisualizeNoneSelections();
        else if (firstSelection != null && secondSelection != null)
            VisualizeExistingSelections(firstSelection, secondSelection);
        else
            throw new ArgumentNullException($"Only one of the selections was null! First: {firstSelection == null}, Second: {secondSelection == null}");
    }

    private void VisualizeNoneSelections()
    {
        _correlationFieldPlot!.Plot.Clear();

        SetTextBoxesEmpty();
    }

    private void SetTextBoxesEmpty()
    {
        TextBox
    }

    private void VisualizeExistingSelections(SelectionContainer firstSelection, SelectionContainer secondSelection)
    {
        _correlationFieldPlot!.Plot.Clear();

        var pointsCount = firstSelection.Values.Count;
        var pointsColor = ExtensionsMethods.GetRandomColor();

        for (int i = 0; i < pointsCount; i++)
        {
            var x = firstSelection.Values[i];
            var y = secondSelection.Values[i];

            _correlationFieldPlot!.Plot.AddPoint(x, y, pointsColor);
        }

        _correlationFieldPlot?.Refresh();
    }
}
