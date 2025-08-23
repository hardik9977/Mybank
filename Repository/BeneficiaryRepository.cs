using System;
using API.Context;
using API.Dtos.Beneficiary;
using API.Entity;
using Microsoft.EntityFrameworkCore;

namespace API.Repository;

public class BeneficiaryRepository
{
    private readonly DBContext _context;

    public BeneficiaryRepository(DBContext context)
    {
        _context = context;
    }

    // Add methods for CRUD operations on Beneficiary entity
    public async Task<Beneficiary> GetBeneficiaryByIdAsync(int id)
    {
        return await _context.Beneficiaries.FindAsync(id);
    }

    public async Task<IEnumerable<Beneficiary>> GetAllBeneficiariesAsync()
    {
        return await _context.Beneficiaries.ToListAsync();
    }

    public async Task AddBeneficiaryAsync(CreateBeneficiaryDto beneficiaryDto, int userId)
    {
        Beneficiary beneficiary = new Beneficiary
        {
            BeneficiaryAccountNumber = beneficiaryDto.BeneficiaryAccountNumber,
            Nickname = beneficiaryDto.Nickname,
            AddedAt = DateTime.UtcNow,
            UserId = userId // Assuming you have a UserId or similar to associate with the beneficiary
        };

        // Assuming you have a UserId or similar to associate with the beneficiary
        // beneficiary.UserId = userId; // Set this if needed
        await _context.Beneficiaries.AddAsync(beneficiary);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateBeneficiaryAsync(Beneficiary beneficiary)
    {
        _context.Beneficiaries.Update(beneficiary);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteBeneficiaryAsync(int id)
    {
        var beneficiary = await GetBeneficiaryByIdAsync(id);
        if (beneficiary != null)
        {
            _context.Beneficiaries.Remove(beneficiary);
            await _context.SaveChangesAsync();
        }
    }

}
