using MusicRecognitionApp.Data;
using MusicRecognitionApp.Services.Audio;
using MusicRecognitionApp.Services.Interfaces;
using NAudio.Wave;
using System.Data.SQLite;

namespace MusicRecognitionApp.Services
{
    public class AudioDatabaseService : IAudioDatabase
    {
        private readonly string _connectionString;

        public AudioDatabaseService(string connectionString)
        {
            _connectionString = connectionString;
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            string _databasePath = _connectionString.Replace("Data Source=", "").Split(";")[0];

            if (!File.Exists(_databasePath))
            {
                SQLiteConnection.CreateFile(_databasePath);
                Console.WriteLine("Создана новая база данных SQLite");
            }

            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();

                const string createSongsTable = @"
                    CREATE TABLE IF NOT EXISTS Songs (
                        SongId INTEGER PRIMARY KEY AUTOINCREMENT,
                        Title TEXT NOT NULL,
                        Artist TEXT NOT NULL,
                        UNIQUE(Title, Artist)
                    )";

                const string createAudioHashesTable = @"
                    CREATE TABLE IF NOT EXISTS AudioHashes (
                        Hash INTEGER NOT NULL,
                        TimeOffset REAL NOT NULL,
                        SongId INTEGER NOT NULL,
                        FOREIGN KEY (SongId) REFERENCES Songs(SongId)
                    )";

                const string createRecognizedSongsTable = @"
                    CREATE TABLE IF NOT EXISTS RecognizedSongs (
                        RecognitionId INTEGER PRIMARY KEY AUTOINCREMENT,
                        SongId INTEGER NOT NULL,
                        Title TEXT NOT NULL,
                        Artist TEXT NOT NULL,
                        RecognitionDate DATETIME DEFAULT CURRENT_TIMESTAMP,
                        Matches INTEGER NOT NULL,
                        FOREIGN KEY (SongId) REFERENCES Songs(SongId)
                    )";

                const string createHashIndex = @"
                    CREATE INDEX IF NOT EXISTS IX_AudioHashes_Hash 
                    ON AudioHashes(Hash)";

                const string createSongIndex = @"
                    CREATE INDEX IF NOT EXISTS IX_AudioHashes_SongId 
                    ON AudioHashes(SongId)";

                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = createSongsTable;
                    command.ExecuteNonQuery();

                    command.CommandText = createAudioHashesTable;
                    command.ExecuteNonQuery();

                    command.CommandText = createRecognizedSongsTable;
                    command.ExecuteNonQuery();

                    command.CommandText = createHashIndex;
                    command.ExecuteNonQuery();

                    command.CommandText = createSongIndex;
                    command.ExecuteNonQuery();
                }
            }
        }

        private Dictionary<uint, List<AudioHash>> FindMatchingHashes(List<uint> queryHashes)
        {
            var results = new Dictionary<uint, List<AudioHash>>();

            if (queryHashes == null || queryHashes.Count == 0)
                return results;

            var parameters = new List<string>();
            for (int i = 0; i < queryHashes.Count; i++)
            {
                parameters.Add($"@hash{i}");
            }

            var query = $@"
                SELECT Hash, TimeOffset, SongId 
                FROM AudioHashes 
                WHERE Hash IN ({string.Join(",", parameters)})";

            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(query, connection))
                {
                    for (int i = 0; i < queryHashes.Count; i++)
                    {
                        command.Parameters.AddWithValue($"@hash{i}", queryHashes[i]);
                    }

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var hash = (uint)Convert.ToInt64(reader["Hash"]);
                            var timeOffset = Convert.ToDouble(reader["TimeOffset"]);
                            var songId = Convert.ToInt32(reader["SongId"]);

                            var audioHash = new AudioHash(hash, timeOffset, songId);

                            if (!results.ContainsKey(hash))
                                results[hash] = new List<AudioHash>();

                            results[hash].Add(audioHash);
                        }
                    }
                }
            }

            return results;
        }

        private (string title, string artist) GetSongInfo(int songId)
        {
            const string query = @"
                SELECT Title, Artist 
                FROM Songs 
                WHERE SongId = @songId";

            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@songId", songId);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                            return (reader["Title"].ToString(), reader["Artist"].ToString());
                    }
                }
            }
            return ("Unknown", "Unknown");
        }

        public List<(int songId, string title, string artist, int matches, double confidence)> SearchSong(List<AudioHash> queryHashes)
        {
            var hashGenerator = new AudioHashGenerator();
            var hashValues = queryHashes.ConvertAll(h => h.Hash);
            var matchingHashes = FindMatchingHashes(hashValues);

            var database = new Dictionary<uint, List<AudioHash>>(matchingHashes);

            var matches = hashGenerator.FindMatches(queryHashes, database);
            var results = new List<(int songId, string title, string artist, int matches, double confidence)>();

            foreach (var match in matches)
            {
                var songInfo = GetSongInfo(match.songId);
                results.Add((match.songId, songInfo.title, songInfo.artist, match.matches, match.confidence));
            }

            return results.OrderByDescending(r => r.matches)
                   .ThenByDescending(r => r.confidence)
                   .ToList();
        }

        public void SaveRecognizedSongs(int songId, string title, string artist, int matches)
        {
            const string query = @"
                INSERT INTO RecognizedSongs (SongId, Title, Artist, Matches)
                VALUES (@songId, @title, @artist, @matches)";

            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@songId", songId);
                    command.Parameters.AddWithValue("@title", title);
                    command.Parameters.AddWithValue("@artist", artist);
                    command.Parameters.AddWithValue("@matches", matches);
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<(int songId, string title, string artist, int matches, DateTime recognitionDate)> GetRecognizedSongs()
        {
            var results = new List<(int songId, string title, string artist, int matches, DateTime recognitionDate)>();

            const string query = @"
                SELECT SongId, Title, Artist, Matches, RecognitionDate
                FROM RecognizedSongs
                ORDER BY RecognitionDate DESC";

            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var songId = Convert.ToInt32(reader["SongId"]);
                            var title = reader["Title"].ToString();
                            var artist = reader["Artist"].ToString();
                            var matches = Convert.ToInt32(reader["Matches"]);
                            var recognitionDate = Convert.ToDateTime(reader["RecognitionDate"]);

                            results.Add((songId, title, artist, matches, recognitionDate));
                        }
                    }
                }
            }

            return results;
        }

        public List<(string artist, int songCount)> GetRecognizedArtists()
        {
            var results = new List<(string artist, int songCount)>();

            const string query = @"
                SELECT Artist, COUNT(*) as SongCount
                FROM RecognizedSongs
                GROUP BY Artist
                ORDER BY SongCount DESC";

            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var artist = reader["Artist"].ToString();
                            var songCount = Convert.ToInt32(reader["SongCount"]);

                            results.Add((artist, songCount));
                        }
                    }
                }
            }

            return results;
        }

        public bool TestConnection()
        {
            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
