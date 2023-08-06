using Globomantics.Areas.Identity.Data;
using Globomantics.Data;
using Globomantics.Models;
using Microsoft.EntityFrameworkCore;

namespace Globomantics.Repositories;
public class ConferenceRepository : IConferenceRepository
{
    private readonly ApplicationDbContext _Dbcontext;

    /// <summary>
    /// Constructor for the ConferenceRepository.
    /// </summary>
    /// <param name="context"></param>
    public ConferenceRepository(ApplicationDbContext context)
    {
        this._Dbcontext = context;
    }

    public async Task<IEnumerable<ConferenceModel>> GetAll()
    {
        return await _Dbcontext
            .Conferences
            .Select(c => new ConferenceModel
            {
                Id = c.Id,
                Name = c.Name,
                Start = c.Start,
                Location = c.Location,
                AttendeeCount = c.AttendeeTotal
            })
            .ToArrayAsync();
    }

    public async Task<ConferenceModel?> GetById(int id)
    {
        return await _Dbcontext.Conferences
            .Select(c => new ConferenceModel
            {
                Id = c.Id,
                Name = c.Name,
                Start = c.Start,
                Location = c.Location,
                AttendeeCount = c.AttendeeTotal
            })
            .FirstOrDefaultAsync(c => c.Id == id);
            
    }

    public async Task<int> Add(ConferenceModel model)
    {
        model.Id = await _Dbcontext.Conferences.MaxAsync(c => c.Id) + 1;

        await _Dbcontext.AddAsync(
            new ConferenceEntity()
            {
                Id = model.Id,
                Name = model.Name,
                Start = model.Start,
                AttendeeTotal = model.AttendeeCount,
                Location = model.Location
            }
        );

        await _Dbcontext.SaveChangesAsync();

        return model.Id;
    }
}


