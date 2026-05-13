namespace MusicRecognitionApp.Blazor.Components.Pages.Table.Model
{
    public interface ITableDetailProvider<T>
    {
        IReadOnlyList<ListTableColumn> Columns { get; }
        Task Remove(int id);
        IQueryable<T> SearchByName(IQueryable<T> items, string? searchString);
        IQueryable<T> GetQueryableAll();
    }
}
