using System;

namespace API.Dtos.Transfer;

public class CreateTransferDto
{
    public string FromAccountNumber { get; set; }
    public string ToAccountNumber { get; set; }
    public decimal Amount { get; set; }
    public string ReferenceNote { get; set; }
    public DateTime TransferDate { get; set; } = DateTime.UtcNow;

}
