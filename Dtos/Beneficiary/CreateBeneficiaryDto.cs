using System;

namespace API.Dtos.Beneficiary;

public class CreateBeneficiaryDto
{
    /// <summary>
    /// The beneficiary's account number.
    /// </summary>
    public string BeneficiaryAccountNumber { get; set; }

    /// <summary>
    /// A friendly nickname for the beneficiary.
    /// </summary>
    public string Nickname { get; set; }
}
