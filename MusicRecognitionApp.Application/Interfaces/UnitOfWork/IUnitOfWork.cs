namespace MusicRecognitionApp.Application.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task SaveAsync(CancellationToken cancellationToken = default);

        void Clear();
    }
}
