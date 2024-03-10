using Fotocopy.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fotocopy.Repository
{
    public interface IPaperRepository
    {
        Task<List<PaperModel>> GetAllPaperAsync();
        Task<PaperModel> GetPaperByIdAsync(int paperId);
        Task<int> AddPaperAsync(PaperModel paperModel);
        Task UpdatePaperAsync(int paperId, PaperModel paperModel);
        Task UpdatePaperPatchAsync(int paperId, JsonPatchDocument paperModel);
        Task DeletePaperAsync(int paperId);
    }
}
