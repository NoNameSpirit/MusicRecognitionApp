namespace MusicRecognitionApp.Infrastructure.Data.Entities
{
    public class AudioHashEntity
    {
        public int Id { get; set; }
        public uint Hash { get; set; }
        public double TimeOffset { get; set; }
        public int SongId { get; set; }

        public virtual SongEntity Song { get; set; } = null!;
    }
}
