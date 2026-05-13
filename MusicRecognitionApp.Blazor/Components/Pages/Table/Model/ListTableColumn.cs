namespace MusicRecognitionApp.Blazor.Components.Pages.Table.Model
{
    public class ListTableColumn
    {
        public string Name { get; }
        public int WidthValuePercent { get; }
        public Func<object, object?> ValueSelector { get; }

        public ListTableColumn(string name, int widthValuePercent, Func<object, object?> selector)
        {
            Name = name;
            WidthValuePercent = widthValuePercent;
            ValueSelector = selector;
        }
    }
}
