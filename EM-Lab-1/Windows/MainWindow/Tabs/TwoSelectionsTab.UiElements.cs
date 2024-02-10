using ScottPlot;

namespace EM_Lab_1;

public partial class TwoSelectionsTab
{
    private WpfPlot? _correlationFieldPlot;

    public TwoSelectionsTab SetCorrelationFieldPlot(WpfPlot correlationFieldPlot)
    {
        _correlationFieldPlot = correlationFieldPlot;

        _correlationFieldPlot.Plot.Title("Кореляційне поле");
        _correlationFieldPlot.Plot.XLabel("X");
        _correlationFieldPlot.Plot.YLabel("Y");

        return this;
    }
}
