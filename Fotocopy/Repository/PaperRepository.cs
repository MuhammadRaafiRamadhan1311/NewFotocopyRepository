using Fotocopy.Data;
using Fotocopy.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fotocopy.Repository
{
    public class PaperRepository : IPaperRepository
    {
        private readonly FotocopyContext _context;
        public PaperRepository(FotocopyContext context) 
        {
            _context = context;
        }
        public async Task<List<PaperModel>> GetAllPaperAsync()
        {
            var records = await _context.Paper.Select(x=> new PaperModel()
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description
            }).ToListAsync();

            return records;
        }

        public async Task<PaperModel> GetPaperByIdAsync(int paperId)
        {
            var records = await _context.Paper.Where(x => x.Id == paperId).Select(x => new PaperModel()
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description
            }).FirstOrDefaultAsync();

            return records;
        }

        public async Task<int> AddPaperAsync(PaperModel paperModel)
        {
            var onepaper = new Paper()
            {
                Title = paperModel.Title,
                Description = paperModel.Description
            };

            _context.Paper.Add(onepaper);
            await _context.SaveChangesAsync();

            return onepaper.Id;
        }

        public async Task UpdatePaperAsync(int paperId, PaperModel paperModel)
        {
            //Dua kali panggilan (call) database
            //var onepaper = await _context.Paper.FindAsync(paperId);
            //if (onepaper != null) 
            //{ 
            //    onepaper.Title = paperModel.Title;
            //    onepaper.Description = paperModel.Description;

            //    _context.SaveChangesAsync();
            //}

            //Satu kali panggilan (call) database
            var onepaper = new Paper()
            {
                Id = paperId,
                Title = paperModel.Title,
                Description = paperModel.Description
            };

            _context.Paper.Update(onepaper);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePaperPatchAsync(int paperId, JsonPatchDocument paperModel)
        {
            var onepaper = await _context.Paper.FindAsync(paperId);
            if (onepaper == null) 
            { 
                paperModel.ApplyTo(onepaper);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeletePaperAsync(int paperId)
        {
            var onepaper = new Paper() {  Id = paperId};

            _context.Paper.Remove(onepaper);

            await _context.SaveChangesAsync();
        }
    }
}
