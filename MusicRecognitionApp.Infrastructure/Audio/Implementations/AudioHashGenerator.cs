using MusicRecognitionApp.Application.Interfaces.Audio;
using MusicRecognitionApp.Core.Models.Audio;

namespace MusicRecognitionApp.Infrastructure.Audio.Implementations
{
    public class AudioHashGenerator : IAudioHashGenerator
    {
        private const int MaxPairs = 15;
        private const double MinTimeDelta = 0.02;
        private const double MaxTimeDelta = 0.4;
        private const double MaxFreqDelta = 1000;

        public List<AudioHash> GenerateHashes(List<Peak> peaks, int songId = 0)
        {
            List<AudioHash> hashes = new List<AudioHash>();

            var sortedPeaks = peaks.OrderBy(p => p.TimeSeconds).ToList();

            for (int i = 0; i < sortedPeaks.Count - 1; i++)
            {
                Peak anchorPeak = sortedPeaks[i];
                List<Peak> targetPeaks = FindTargetPeaks(sortedPeaks, i, anchorPeak);

                foreach (Peak targetPeak in targetPeaks.Take(MaxPairs))
                {
                    uint hash = CreateHash(anchorPeak, targetPeak);
                    hashes.Add(new AudioHash(hash, Math.Round(anchorPeak.TimeSeconds, 2), songId));
                }
            }

            return hashes;
        }

        private List<Peak> FindTargetPeaks(List<Peak> sortedPeaks, int anchorIndex, Peak anchorPeak)
        {
            List<Peak> targetPeaks = new List<Peak>();

            for (int j = anchorIndex + 1; j < sortedPeaks.Count; j++)
            {
                Peak candidate = sortedPeaks[j];
                double timeDelta = candidate.TimeSeconds - anchorPeak.TimeSeconds;

                if (timeDelta < MinTimeDelta)
                    continue;

                if (timeDelta > MaxTimeDelta)
                    break;

                double freqDiff = Math.Abs(candidate.FrequencyHz - anchorPeak.FrequencyHz);

                if (freqDiff <= MaxFreqDelta)
                    targetPeaks.Add(candidate);
            }

            return targetPeaks.OrderByDescending(p => p.Magnitude).ToList();
        }

        private uint CreateHash(Peak anchor, Peak target)
        {
            const int FrequencyRoundingNum = 12;
            const int TimeRoundingNum = 30;
            const int Max10Bit = 0x3FF;

            int anchorFreq = (int)(anchor.FrequencyHz / FrequencyRoundingNum) * FrequencyRoundingNum;
            int targetFreq = (int)(target.FrequencyHz / FrequencyRoundingNum) * FrequencyRoundingNum;

            int timeDeltaMs = (int)((target.TimeSeconds - anchor.TimeSeconds) * 1000);
            int timeDelta = timeDeltaMs / TimeRoundingNum * TimeRoundingNum;

            anchorFreq = Math.Min(anchorFreq / 10, Max10Bit);
            targetFreq = Math.Min(targetFreq / 10, Max10Bit);
            timeDelta = Math.Min(timeDelta, Max10Bit);

            uint hash = (uint)(anchorFreq << 20 | targetFreq << 10 | timeDelta);
            return hash;
        }
    }
}
