using MusicRecognitionApp.Application.Services.Interfaces;
using MusicRecognitionApp.Core.Models.Audio;
using MusicRecognitionApp.Core.Models.Business;
using MusicRecognitionApp.Infrastructure.Audio.Interfaces;

namespace MusicRecognitionApp.Application.Services.Implementations
{
    public class AudioRecognitionService : IAudioRecognition
    {
        private readonly IAudioProcessor _audioProcessor;
        private readonly ISpectrogramBuilder _spectrogramBuilder;
        private readonly IPeakDetector _peakDetector;
        private readonly IAudioHashGenerator _hashGenerator;
        private readonly ISongImportService _importService;
        private readonly ISongSearchService _searchService;

        private readonly string[] AUDIO_EXTENSIONS = { ".mp3", ".wav" };

        public event Action<int> AnalysisProgress;
        public event Action<int> ImportProgress;

        public AudioRecognitionService(
            IAudioProcessor audioProcessor,
            ISpectrogramBuilder spectrogramBuilder,
            IPeakDetector peakDetector,
            IAudioHashGenerator hashGenerator,
            ISongImportService importService,
            ISongSearchService searchService)
        {
            _audioProcessor = audioProcessor;
            _spectrogramBuilder = spectrogramBuilder;
            _peakDetector = peakDetector;
            _hashGenerator = hashGenerator;
            _importService = importService;
            _searchService = searchService;
        }

        public async Task<List<SearchResultModel>> RecognizeFromMicrophoneAsync(string audioFilePath)
        {
            if (string.IsNullOrEmpty(audioFilePath))
                return new List<SearchResultModel>();

            AnalysisProgress?.Invoke(10);
            float[] processedAudio = await Task.Run(() 
                => _audioProcessor.PreprocessAudio(audioFilePath));

            AnalysisProgress?.Invoke(20);
            SpectrogramData spectrogramData = await Task.Run(()
                => _spectrogramBuilder.ProcessAudio(processedAudio, 11025));

            AnalysisProgress?.Invoke(40);
            List<Peak> allPeaks = await Task.Run(()
                => _peakDetector.ProcessPeekDetector(spectrogramData));

            AnalysisProgress?.Invoke(50);
            List<AudioHash> queryHashes = await Task.Run(() 
                => _hashGenerator.GenerateHashes(allPeaks));

            AnalysisProgress?.Invoke(70);
            var results = await _searchService.SearchSong(queryHashes);

            AnalysisProgress?.Invoke(100);
            return results;
        }

        public async Task<(int added, int failed, List<string> errors)> AddTracksFromFolderAsync(string folderPath)
        {
            if (!Directory.Exists(folderPath) || string.IsNullOrEmpty(folderPath))
            {
                return (0, 0, new List<string> { "Папка не существует" });
            }

            List<string> audioFiles = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories)
                .Where(f => AUDIO_EXTENSIONS.Contains(Path.GetExtension(f).ToLower()))
                .ToList();

            if (audioFiles.Count == 0)
            {
                return (0, 0, new List<string> { "В папке не найдены аудио файлы (mp3/wav)" });
            }

            ImportProgress?.Invoke(0);

            var added = 0;
            var failed = 0;
            var errors = new List<string>();

            for (int i = 0; i < audioFiles.Count; i++)
            {
                string filepath = audioFiles[i];

                try
                {
                    string artist = GetArtistFromFile(filepath, folderPath);
                    string title = Path.GetFileNameWithoutExtension(filepath);
            
                    await AddTrackAsync(filepath, title, artist);
                    added++;
                }
                catch (Exception ex)
                {
                    failed++;
                    errors.Add($"{Path.GetFileName(filepath)}: {ex.Message}");
                }
            
                int progress = (i + 1) * 100 / audioFiles.Count;
                ImportProgress?.Invoke(progress);
            }

            ImportProgress?.Invoke(100);
            return (added, failed, errors);
        }

        private string GetArtistFromFile(string filePath, string selectedFolder)
        {
            string? dirName = Path.GetDirectoryName(filePath);

            if (dirName.Equals(selectedFolder.TrimEnd(Path.DirectorySeparatorChar), StringComparison.OrdinalIgnoreCase))
            {
                string? artist = Path.GetFileName(selectedFolder.TrimEnd(Path.DirectorySeparatorChar));
                return string.IsNullOrEmpty(artist) ? "Unknown Artist" : artist;
            }

            return Path.GetFileName(dirName);
        }

        private async Task AddTrackAsync(string audioFilePath, string title, string artist)
        {

            float[] processedAudio = await Task.Run(()
                => _audioProcessor.PreprocessAudio(audioFilePath));

            SpectrogramData spectrogramData = await Task.Run(()
                => _spectrogramBuilder.ProcessAudio(processedAudio, 11025));

            List<Peak> allPeaks = await Task.Run(()
                => _peakDetector.ProcessPeekDetector(spectrogramData));

            List<AudioHash> queryHashes = await Task.Run(()
                => _hashGenerator.GenerateHashes(allPeaks));

            await _importService.AddSongAsync(title, artist, queryHashes);
        }
    }
}
