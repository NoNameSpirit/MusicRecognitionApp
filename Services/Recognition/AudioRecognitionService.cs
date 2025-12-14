using Microsoft.Extensions.DependencyInjection;
using MusicRecognitionApp.Core.Models.Business;
using MusicRecognitionApp.Data;
using MusicRecognitionApp.Services.Audio;
using MusicRecognitionApp.Services.Audio.Interfaces;
using MusicRecognitionApp.Services.Data.Interfaces;
using MusicRecognitionApp.Services.Interfaces;
using System.Diagnostics;
using System.DirectoryServices;
using System.Threading;

namespace MusicRecognitionApp.Services
{
    public class AudioRecognitionService : IAudioRecognition
    {
        private readonly IAudioDatabase _databaseService;
        private readonly IAudioRecorder _recorderService;
        private readonly IAudioProcessor _audioProcessor;
        private readonly ISpectrogramBuilder _spectrogramBuilder;
        private readonly IPeakDetector _peakDetector;
        private readonly IAudioHashGenerator _hashGenerator;

        private readonly string[] AUDIO_EXTENSIONS = { ".mp3", ".wav" };

        public event Action<int> AnalysisProgress;
        public event Action<int> ImportProgress;

        public AudioRecognitionService(
            IAudioDatabase databaseService,
            IAudioRecorder recorderService,
            IAudioProcessor audioProcessor,
            ISpectrogramBuilder spectrogramBuilder,
            IPeakDetector peakDetector,
            IAudioHashGenerator hashGenerator)
        {
            _databaseService = databaseService;
            _recorderService = recorderService;
            _audioProcessor = audioProcessor;
            _spectrogramBuilder = spectrogramBuilder;
            _peakDetector = peakDetector;
            _hashGenerator = hashGenerator;
        }

        public async Task<string> RecordAudioAsync(int durationTime = 15, CancellationToken cancellationToken = default)
        {
            return await _recorderService.RecordAudioFromMicrophoneAsync(durationTime, cancellationToken);
        }

        public async Task<List<SearchResultModel>> RecognizeFromMicrophoneAsync(string audioFilePath)
        {
            if (string.IsNullOrEmpty(audioFilePath))
                return new List<SearchResultModel>();

            return await Task.Run(() =>
            {
                AnalysisProgress?.Invoke(10);
                float[] processedAudio = _audioProcessor
                    .PreprocessAudio(audioFilePath);

                AnalysisProgress?.Invoke(20);
                SpectrogramData spectrogramData = _spectrogramBuilder
                    .ProcessAudio(processedAudio, 11025);

                AnalysisProgress?.Invoke(40);
                List<Peak> allPeaks = _peakDetector
                    .ProcessPeekDetector(spectrogramData);

                AnalysisProgress?.Invoke(50);
                List<AudioHash> queryHashes = _hashGenerator
                    .GenerateHashes(allPeaks);

                AnalysisProgress?.Invoke(70);
                var results = _databaseService
                    .SearchSong(queryHashes);

                AnalysisProgress?.Invoke(100);
                return results;
            });
        }

        public async Task<(int added, int failed, List<string> errors)> AddTracksFromFolderAsync(string folderPath)
        {
            var added = 0;
            var failed = 0;
            var errors = new List<string>();

            if (!Directory.Exists(folderPath) || string.IsNullOrEmpty(folderPath))
            {
                errors.Add("File doesn't exists");
                return (added, failed, errors);
            }

            List<string> audioFiles = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories)
                .Where(f => AUDIO_EXTENSIONS.Contains(Path.GetExtension(f).ToLower()))
                .ToList();

            if (audioFiles.Count == 0)
            {
                errors.Add("В папке не найдены аудио файлы (mp3/wav)");
                return (added, failed, errors);
            }

            ImportProgress?.Invoke(0);

            await Task.Run(async () =>
            {
                for (int i = 0; i < audioFiles.Count; i++)
                {
                    string filepath = audioFiles[i];
                    try
                    {
                        string artist = GetArtistFromFile(filepath, folderPath);
                        string title = Path.GetFileNameWithoutExtension(filepath);

                        await ProcessAndAddTrackAsync(filepath, title, artist); 
                        added++;
                    }
                    catch (Exception ex)
                    {
                        failed++;
                        errors.Add($"{Path.GetFileName(audioFiles[i])}: {ex.Message}");
                    }

                    int progress = (i + 1) * 100 / audioFiles.Count;
                    ImportProgress?.Invoke(progress);
                }
            });

            ImportProgress?.Invoke(100);
            return (added, failed, errors);
        }

        private string GetArtistFromFile(string filePath, string selectedFolder)
        {
            string? dirName = Path.GetDirectoryName(filePath);

            if (dirName.Equals(selectedFolder.TrimEnd(Path.DirectorySeparatorChar), StringComparison.OrdinalIgnoreCase))
            {
                string? artist = Path.GetFileName(selectedFolder.TrimEnd(Path.DirectorySeparatorChar));
                return string.IsNullOrEmpty(artist) ? "Unknown Artist": artist;
            }

            return Path.GetFileName(dirName);
        }

        private async Task ProcessAndAddTrackAsync(string filePath, string title, string artist)
        {
            float[] processedAudio = _audioProcessor
                .PreprocessAudio(filePath);

            SpectrogramData spectrogramData = _spectrogramBuilder
                .ProcessAudio(processedAudio, 11025);

            List<Peak> allPeaks = _peakDetector
                .ProcessPeekDetector(spectrogramData);

            List<AudioHash> queryHashes = _hashGenerator
                .GenerateHashes(allPeaks);

            await _databaseService
                .AddSongWithHashesAsync(title, artist, queryHashes);
        }
    }
}
