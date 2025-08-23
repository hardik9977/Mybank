using System;
using API.Dtos.Transfer;
using API.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/transfer")]
[Authorize]
public class TransferController : ControllerBase
{
    private readonly TransfersRepository _transfersRepository;

    public TransferController(TransfersRepository transfersRepository)
    {
        _transfersRepository = transfersRepository;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTransfer([FromBody] CreateTransferDto transferDto)
    {
        if (transferDto == null)
        {
            return BadRequest("Invalid transfer data.");
        }

        try
        {
            await _transfersRepository.InitiateTransfer(transferDto);
            return Ok(new { Message = "Transfer created successfully." });
        }
        catch (Exception ex)
        {
            return BadRequest($"Error creating transfer: {ex.Message}");
        }
    }
}
