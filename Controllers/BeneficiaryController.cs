using System;
using System.Security.Claims;
using API.Dtos.Beneficiary;
using API.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/beneficiary")]
[Authorize]
public class BeneficiaryController : ControllerBase
{
    private readonly BeneficiaryRepository _beneficiaryRepository;

    public BeneficiaryController(BeneficiaryRepository beneficiaryRepository)
    {
        _beneficiaryRepository = beneficiaryRepository;
    }

    [HttpPost]
    public async Task<IActionResult> CreateBeneficiary([FromBody] CreateBeneficiaryDto createBeneficiaryDto)
    {
        if (createBeneficiaryDto == null)
        {
            return BadRequest("Invalid beneficiary data.");
        }

        int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        await _beneficiaryRepository.AddBeneficiaryAsync(createBeneficiaryDto, userId);

        return Ok(new { Message = "Beneficiary created successfully" });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBeneficiary(int id)
    {
        var beneficiary = await _beneficiaryRepository.GetBeneficiaryByIdAsync(id);
        if (beneficiary == null)
        {
            return NotFound("Beneficiary not found.");
        }

        return Ok(beneficiary);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBeneficiary(int id, [FromBody] CreateBeneficiaryDto updateDto)
    {
        if (updateDto == null)
        {
            return BadRequest("Invalid beneficiary data.");
        }

        var beneficiary = await _beneficiaryRepository.GetBeneficiaryByIdAsync(id);
        if (beneficiary == null)
        {
            return NotFound("Beneficiary not found.");
        }

        beneficiary.BeneficiaryAccountNumber = updateDto.BeneficiaryAccountNumber;
        beneficiary.Nickname = updateDto.Nickname;

        await _beneficiaryRepository.UpdateBeneficiaryAsync(beneficiary);

        return Ok(new { Message = "Beneficiary updated successfully" });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBeneficiary(int id)
    {
        await _beneficiaryRepository.DeleteBeneficiaryAsync(id);
        return Ok(new { Message = "Beneficiary deleted successfully" });
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBeneficiaries()
    {
        var beneficiaries = await _beneficiaryRepository.GetAllBeneficiariesAsync();
        return Ok(beneficiaries);
    }
}
