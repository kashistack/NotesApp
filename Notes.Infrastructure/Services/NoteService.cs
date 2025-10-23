
using Microsoft.EntityFrameworkCore;
using Notes.Application.DTOs;
using Notes.Application.Interfaces;
using Notes.Domain.Enums;
using Notes.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notes.Infrastructure.Services
{
    public class NoteService : INoteService
    {
        private readonly NotesDbContext _db;
        public NoteService(NotesDbContext db) => _db = db;

        public async Task<NoteDto> CreateAsync(string? title, string? content, Priority priority)
        {
            var note = new Notes.Domain.Entities.Note
            {
                Title = title,
                Content = content,
                Priority = priority,
                CreatedAt = DateTime.UtcNow
            };
            _db.Notes.Add(note);
            await _db.SaveChangesAsync();
            return new NoteDto { Id = note.Id, Title = note.Title, Content = note.Content, Priority = note.Priority, CreatedAt = note.CreatedAt };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var note = await _db.Notes.FindAsync(id);
            if (note == null) return false;
            _db.Notes.Remove(note);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<NoteDto>> GetAllAsync()
        {
            return await _db.Notes
                .OrderByDescending(n => n.CreatedAt)
                .Select(n => new NoteDto
                {
                    Id = n.Id,
                    Title = n.Title,
                    Content = n.Content,
                    Priority = n.Priority,
                    CreatedAt = n.CreatedAt,
                    UpdatedAt = n.UpdatedAt
                }).ToListAsync();
        }

        public async Task<NoteDto?> GetByIdAsync(int id)
        {
            var n = await _db.Notes.FindAsync(id);
            if (n == null) return null;
            return new NoteDto { Id = n.Id, Title = n.Title, Content = n.Content, Priority = n.Priority, CreatedAt = n.CreatedAt, UpdatedAt = n.UpdatedAt };
        }

        public async Task<bool> UpdateAsync(int id, string? title, string? content, Priority priority)
        {
            var note = await _db.Notes.FindAsync(id);
            if (note == null) return false;
            note.Title = title;
            note.Content = content;
            note.Priority = priority;
            note.UpdatedAt = DateTime.UtcNow;
            _db.Notes.Update(note);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
