using MusicRecognitionApp.Core.Models.Business;
using MusicRecognitionApp.Core.Models.Entities;

namespace MusicRecognitionApp.Services.Data.Mappers
{
    public class ModelToEntity
    {
        public static RecognizedSongEntity ToRecognizedSongEntity(int songId, int matches)
        {
            return new RecognizedSongEntity
            {
                SongId = songId,
                Matches = matches,
                RecognitionDate = DateTime.UtcNow
            };
        }

        public static SongEntity ToSongEntity(SongModel model)
        {
            if (model == null) 
                return null;

            return new SongEntity
            {
                Id = model.Id,
                Title = model.Title,
                Artist = model.Artist,
            };
        }

        public static RecognizedSongEntity ToRecognizedSongEntity(RecognizedSongModel model)
        {
            if (model == null) 
                return null;

            return new RecognizedSongEntity
            {
                Id = model.Id,
                RecognitionDate = model.RecognitionDate,
                Matches = model.Matches,
                SongId = model.Song?.Id ?? 0,
            };
        }
    }
}
