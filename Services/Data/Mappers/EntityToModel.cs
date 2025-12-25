using MusicRecognitionApp.Core.Models.Audio;
using MusicRecognitionApp.Core.Models.Business;
using MusicRecognitionApp.Core.Models.Entities;

namespace MusicRecognitionApp.Services.Data.Mappers
{
    public static class EntityToModel
    {
        public static SongModel ToSongModel(SongEntity entity)
        {
            if (entity == null)
                return null;

            return new SongModel
            {
                Id = entity.Id,
                Artist = entity.Artist,
                Title = entity.Title
            };
        }

        public static SearchResultModel ToSearchResultModel(SongEntity entity, int matches, double confidence)
        {
            if (entity == null)
                return null;

            return new SearchResultModel
            {
                Song = ToSongModel(entity),
                Matches = matches,
                Confidence = confidence
            };
        }

        public static RecognizedSongModel ToRecognizedSongModel(RecognizedSongEntity entity)
        {
            if (entity == null)
                return null;

            return new RecognizedSongModel
            {
                Id = entity.Id,
                Matches= entity.Matches,
                RecognitionDate= entity.RecognitionDate,
                Song = ToSongModel(entity.Song),
            };
        }

        public static ArtistStatisticModel ToArtistStatisticModel((string Artist, int SongCount) tuple)
        {
            return new ArtistStatisticModel
            {
                Artist= tuple.Artist,
                SongCount = tuple.SongCount,
            };
        }

        public static AudioHashEntity ToAudioHashEntity(AudioHash entity, int songId)
        {
            return new AudioHashEntity
            {
                Hash = entity.Hash,
                TimeOffset= entity.TimeOffset,
                SongId= songId,
            };
        }

        public static AudioHash ToAudioHash(AudioHashEntity entity)
        {
            return new AudioHash(entity.Hash, entity.TimeOffset, entity.SongId);
        }
    }
}
