using Microsoft.EntityFrameworkCore;
using MusicRecognitionApp.Core.Models.Entities;

namespace MusicRecognitionApp.Services
{
    public class MusicRecognitionContext : DbContext
    {
        // При описании свзяи с одной стороны с другой уже не требуется, id автоматически индексируются и определяются как PK
        public MusicRecognitionContext(DbContextOptions<MusicRecognitionContext> options)
            : base(options)
        {
        }

        public DbSet<SongEntity> Songs { get; set; } = null!;
        public DbSet<RecognizedSongEntity> RecognizedSongs { get; set; } = null!;
        public DbSet<AudioHashEntity> AudioHashes { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SongEntity>(entity =>
            {
                entity.ToTable("Songs");

                entity.HasIndex(e => new { e.Title, e.Artist}, "IX_Songs_Title_Artist")
                      .IsUnique();

                entity.HasMany(e => e.AudioHashes)
                      .WithOne(e => e.Song)
                      .HasForeignKey(e => e.SongId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e=>e.RecognizedSongs)
                      .WithOne(e=>e.Song)
                      .HasForeignKey(e=>e.SongId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<AudioHashEntity>(entity =>
            {
                entity.ToTable("AudioHashes");

                entity.HasIndex(e => e.SongId, "IX_AudioHashes_SongId");
                
                entity.HasIndex(e => e.Hash, "IX_AudioHashes_Hash");

                entity.Property(e => e.Hash)
                      .HasColumnType("INT");
            });

            modelBuilder.Entity<RecognizedSongEntity>(entity =>
            {
                entity.ToTable("RecognizedSongs");

                entity.HasIndex(e => e.RecognitionDate, "IX_RecognizedSongs_RecognitionDate");
               
                entity.HasIndex(e => e.SongId, "IX_RecognizedSongs_SongId");
                
                entity.Property(e => e.RecognitionDate)
                      .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });
        }
    }
}