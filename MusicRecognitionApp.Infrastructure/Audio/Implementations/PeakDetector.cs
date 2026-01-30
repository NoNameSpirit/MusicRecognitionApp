using MusicRecognitionApp.Application.Interfaces.Audio;
using MusicRecognitionApp.Core.Models.Audio;

namespace MusicRecognitionApp.Infrastructure.Audio.Implementations
{
    public class PeakDetector : IPeakDetector
    {
        public List<Peak> ProcessPeekDetector(SpectrogramData spectrogramData)
        {
            List<Peak> allPeaks = FindPeaks(spectrogramData);
            allPeaks = LimitPeakDensity(allPeaks);

            return allPeaks;
        }

        private List<Peak> FindPeaks(SpectrogramData spectrogramData, int neighborhoodSize = 3)
        {
            List<Peak> peaks = new List<Peak>();

            double[,] spectrogram = spectrogramData.Spectrogram;
            int numTimeFrames = spectrogram.GetLength(0);
            int numFreqBins = spectrogram.GetLength(1);

            for (int t = neighborhoodSize; t < numTimeFrames - neighborhoodSize; t++)
            {
                for (int f = neighborhoodSize; f < numFreqBins - neighborhoodSize; f++)
                {
                    double currentValue = spectrogram[t, f];

                    if (currentValue < 0.1) continue;

                    if (IsLocalMaximum(spectrogram, t, f, neighborhoodSize, currentValue))
                    {
                        Peak peak = new Peak
                        {
                            TimeIndex = t,
                            FrequencyIndex = f,
                            Magnitude = currentValue,
                            TimeSeconds = spectrogramData.TimeAxis[t],
                            FrequencyHz = spectrogramData.FrequencyAxis[f]
                        };

                        peaks.Add(peak);
                    }
                }
            }

            return peaks;
        }
        private bool IsLocalMaximum(double[,] spectrogram, int centerT, int centerF,
                           int neighborhoodSize, double centerValue)
        {
            for (int dt = -neighborhoodSize; dt <= neighborhoodSize; dt++)
            {
                for (int df = -neighborhoodSize; df <= neighborhoodSize; df++)
                {
                    if (dt == 0 && df == 0) continue;

                    int t = centerT + dt;
                    int f = centerF + df;

                    if (t < 0 || t >= spectrogram.GetLength(0) ||
                        f < 0 || f >= spectrogram.GetLength(1)) continue;

                    if (spectrogram[t, f] >= centerValue)
                        return false;
                }
            }
            return true;
        }

        private List<Peak> LimitPeakDensity(List<Peak> peaks, int maxPeaksPerSecond = 50)
        {
            if (peaks.Count == 0)
                return peaks;

            var peaksBySecond = peaks.GroupBy(p => (int)Math.Floor(p.TimeSeconds))
                                     .OrderBy(g => g.Key);

            List<Peak> limitedPeaks = new List<Peak>();

            foreach (var secondGroup in peaksBySecond)
            {
                var sortedPeaks = secondGroup.OrderByDescending(p => p.Magnitude);
                var selectedPeaks = sortedPeaks.Take(maxPeaksPerSecond);
                limitedPeaks.AddRange(selectedPeaks);
            }

            return limitedPeaks;
        }
    }
}
