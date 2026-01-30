namespace MusicRecognitionApp.Core.Models.Audio
{
    public class AudioHash
    {
        public uint Hash { get; set; }
        public double TimeOffset { get; set; }
        public int SongId { get; set; }

        public AudioHash(uint hash, double timeOffset, int songId = 0)
        {
            Hash = hash;
            TimeOffset = timeOffset;
            SongId = songId;
        }
    }
}
