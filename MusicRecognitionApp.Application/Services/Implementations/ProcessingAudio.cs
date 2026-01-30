using MusicRecognitionApp.Application.Interfaces.Audio;
using MusicRecognitionApp.Application.Models;
using MusicRecognitionApp.Application.Services.Interfaces;
using MusicRecognitionApp.Core.Models.Audio;

namespace MusicRecognitionApp.Application.Services.Implementations
{
    public class ProcessingAudio : IProcessingAudio
    {
        public event Action<int> ImportProgress;

        private readonly string[] AUDIO_EXTENSIONS = { ".mp3", ".wav" };

        private readonly IAudioProcessor _audioProcessor;
        private readonly ISpectrogramBuilder _spectrogramBuilder;
        private readonly IPeakDetector _peakDetector;
        private readonly IAudioHashGenerator _hashGenerator;
        private readonly ISongImportService _importService;

        public ProcessingAudio(
            IAudioProcessor audioProcessor,
            ISpectrogramBuilder spectrogramBuilder,
            IPeakDetector peakDetector,
            IAudioHashGenerator hashGenerator,
            ISongImportService importService)
        {
            _audioProcessor = audioProcessor;
            _spectrogramBuilder = spectrogramBuilder;
            _peakDetector = peakDetector;
            _hashGenerator = hashGenerator;
            _importService = importService;
        }

        public async Task<ImportTracksResult> AddTracksFromFolderAsync(string folderPath)
        {
            var result = new ImportTracksResult(0, 0, new List<string>());

            if (string.IsNullOrEmpty(folderPath) || !Directory.Exists(folderPath))
            {
                result.Errors.Add("Folder doesn't exist");
                return result;
            }

            List<string> audioFiles = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories)
                .Where(f => AUDIO_EXTENSIONS.Contains(Path.GetExtension(f).ToLower()))
                .ToList();

            if (audioFiles.Count == 0)
            {
                result.Errors.Add("Audio files (mp3/wav) were not found in the folder");
                return result;
            }

            ImportProgress?.Invoke(0);

            int added = 0;
            int failed = 0;
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
                    result.Errors.Add($"{Path.GetFileName(filepath)}: {ex.Message}");
                }

                int progress = (i + 1) * 100 / audioFiles.Count;
                ImportProgress?.Invoke(progress);
            }
            ImportProgress?.Invoke(100);

            return new ImportTracksResult(added, failed, result.Errors);
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
