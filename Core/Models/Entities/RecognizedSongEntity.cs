namespace MusicRecognitionApp.Core.Models.Entities
{
    public class RecognizedSongEntity
    {
        public int Id { get; set; }
        public DateTime RecognitionDate { get; set; } = DateTime.UtcNow;
        public int Matches { get; set; }
        public int SongId { get; set; }

        public virtual SongEntity Song { get; set; } = null!;
    }
}
