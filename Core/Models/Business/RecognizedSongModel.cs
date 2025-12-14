namespace MusicRecognitionApp.Core.Models.Business
{
    public class RecognizedSongModel
    {
        public int Id { get; set; }
        public DateTime RecognitionDate { get; set; }
        public int Matches { get; set; }
        public SongModel Song { get; set; }
    }
}
