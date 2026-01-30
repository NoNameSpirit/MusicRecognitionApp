namespace MusicRecognitionApp.Infrastructure.Data.Entities
{
    public class SongEntity
    {
        public SongEntity()
        {
            AudioHashes = new HashSet<AudioHashEntity>();
            RecognizedSongs = new HashSet<RecognizedSongEntity>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }

        public virtual ICollection<AudioHashEntity> AudioHashes { get; set; }
        public virtual ICollection<RecognizedSongEntity> RecognizedSongs { get; set; }
    }
}
