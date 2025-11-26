using MusicRecognitionApp.Data;
using MusicRecognitionApp.Services.Audio.Interfaces;

namespace MusicRecognitionApp.Services.Audio
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

        public List<(int songId, int matches, double confidence)> FindMatches(List<AudioHash> queryHashes, Dictionary<uint, List<AudioHash>> database)
        {
            Dictionary<int, List<(double queryTime, double dbTime)>> songMatches = new Dictionary<int, List<(double, double)>>();

            foreach (var queryHash in queryHashes)
            {
                if (database.ContainsKey(queryHash.Hash))
                {
                    foreach (var dbHash in database[queryHash.Hash])
                    {
                        if (!songMatches.ContainsKey(dbHash.SongId))
                            songMatches[dbHash.SongId] = new List<(double, double)>();

                        songMatches[dbHash.SongId].Add((queryHash.TimeOffset, dbHash.TimeOffset));
                    }
                }
            }

            var results = new List<(int songId, int consistentMatches, double confidence)>();

            foreach (var songMatch in songMatches)
            {
                int songId = songMatch.Key;
                var matches = songMatch.Value;

                int minMatches = Math.Max(2, Math.Min(queryHashes.Count / 15, 6));

                if (matches.Count < minMatches)
                    continue;

                Dictionary<int, int> offsetCounts = new Dictionary<int, int>();

                var exactOffsetCounts = new Dictionary<double, int>();
                double threshold = 0.1;

                foreach (var match in matches)
                {
                    double exactOffset = match.dbTime - match.queryTime;
                    var existingKey = exactOffsetCounts.Keys
                        .FirstOrDefault(k => Math.Abs(k - exactOffset) <= threshold);

                    if (existingKey != 0.0)
                        exactOffsetCounts[existingKey]++;
                    else
                        exactOffsetCounts[exactOffset] = 1;
                }

                foreach (var kvp in exactOffsetCounts)
                {
                    int roundedOffset = (int)Math.Round(kvp.Key);
                    if (!offsetCounts.ContainsKey(roundedOffset))
                        offsetCounts[roundedOffset] = 0;
                    offsetCounts[roundedOffset] += kvp.Value;
                }

                var bestOffset = offsetCounts.OrderByDescending(kvp => kvp.Value).First();
                int consistentMatches = bestOffset.Value;
                double confidence = (double)consistentMatches / matches.Count;

                if (IsAcceptable(confidence, consistentMatches))
                {
                    results.Add((songId, consistentMatches, confidence));
                }
            }

            return results;
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
            int timeDelta = (timeDeltaMs / TimeRoundingNum) * TimeRoundingNum;

            anchorFreq = Math.Min(anchorFreq / 10, Max10Bit);
            targetFreq = Math.Min(targetFreq / 10, Max10Bit);
            timeDelta = Math.Min(timeDelta, Max10Bit);

            uint hash = (uint)((anchorFreq << 20) | (targetFreq << 10) | timeDelta);
            return hash;
        }

        private bool IsAcceptable(double confidence, int consistentMatches)
        {
            if (confidence > 0.10) return true;
            if (confidence > 0.05 && consistentMatches >= 8) return true;
            if (confidence > 0.02 && consistentMatches >= 5) return true;
            return false;
        }
    }
}
