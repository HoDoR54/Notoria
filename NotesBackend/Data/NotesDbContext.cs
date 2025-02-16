﻿using Microsoft.EntityFrameworkCore;
using NotesBackend.Models;

namespace NotesBackend.Data
{
    public class NotesDbContext: DbContext
    {
        public NotesDbContext (DbContextOptions dbContextOptions): base(dbContextOptions)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<NoteTag> NoteTags { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Preference> Preferences { get; set; }
        public DbSet<Draft> Drafts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<NoteTag>()
               .HasKey(nt => nt.Id );

            modelBuilder.Entity<NoteTag>()
                .HasOne(nt => nt.Note)
                .WithMany(n => n.NoteTags)
                .HasForeignKey(nt => nt.NoteId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<NoteTag>()
                .HasOne(nt => nt.Tag)
                .WithMany(t => t.NoteTags)
                .HasForeignKey(nt => nt.TagId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
