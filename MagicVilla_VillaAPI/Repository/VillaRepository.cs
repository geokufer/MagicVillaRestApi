using System.Linq.Expressions;
using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaAPI.Repository;

public class VillaRepository(ApplicationDbContext applicationDbContext)
    : Repository<Villa>(applicationDbContext), IVillaRepository
{
    private readonly ApplicationDbContext _appDbContext = applicationDbContext;

    public async Task<Villa> UpdateAsync(Villa villa)
    {
        villa.UpdatedDate = DateTime.Now;
        _appDbContext.Villas.Update(villa);
        await _appDbContext.SaveChangesAsync();
        return villa;
    }
}