
using Notes.Application.DTOs;
using Notes.Domain.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notes.Application.Interfaces
{
    public interface INoteService
    {
        Task<IEnumerable<NoteDto>> GetAllAsync();
        Task<NoteDto?> GetByIdAsync(int id);
        Task<NoteDto> CreateAsync(string? title, string? content, Priority priority);
        Task<bool> UpdateAsync(int id, string? title, string? content, Priority priority);
        Task<bool> DeleteAsync(int id);
    }
}
