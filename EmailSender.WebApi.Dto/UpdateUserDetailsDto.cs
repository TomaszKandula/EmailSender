using System.Diagnostics.CodeAnalysis;

namespace EmailSender.WebApi.Dto;

/// <summary>
/// Use it when you want to change user details.
/// </summary>
[ExcludeFromCodeCoverage]
public class UpdateUserDetailsDto
{
    /// <summary>
    /// User ID.
    /// </summary>
    public Guid? UserId { get; set; }

    /// <summary>
    /// Company name.
    /// </summary>
    public string CompanyName { get; set; } = "";

    /// <summary>
    /// VAT number.
    /// </summary>
    public string VatNumber { get; set; } = "";

    /// <summary>
    /// Street address.
    /// </summary>
    public string StreetAddress { get; set; } = "";

    /// <summary>
    /// Postal code.
    /// </summary>
    public string PostalCode { get; set; } = "";

    /// <summary>
    /// Country.
    /// </summary>
    public string Country { get; set; } = "";

    /// <summary>
    /// City.
    /// </summary>
    public string City { get; set; } = "";
}