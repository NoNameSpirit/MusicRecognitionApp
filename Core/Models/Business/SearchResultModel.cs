namespace MusicRecognitionApp.Core.Models.Business
{
    public class SearchResultModel
    {
        public SongModel Song { get; set; }
        public int Matches { get; set; }
        public double Confidence { get; set; }
    }
}
