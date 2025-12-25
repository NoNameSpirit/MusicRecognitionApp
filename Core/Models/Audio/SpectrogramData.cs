namespace MusicRecognitionApp.Core.Models.Audio
{
    public class SpectrogramData
    {
        public double[,] Spectrogram { get; set; }
        public double[] FrequencyAxis { get; set; }
        public double[] TimeAxis { get; set; }
        public int SampleRate { get; set; }
    }
}
