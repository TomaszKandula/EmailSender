using System.Diagnostics.CodeAnalysis;

namespace EmailSender.Backend.Core.Extensions.Models;

[ExcludeFromCodeCoverage]
public class DateRangeModel
{
    public DateTime StartDate { get; }
    public DateTime EndDate { get; }

    public DateRangeModel(DateTime startDate, DateTime endDate)
    {
        StartDate = startDate;
        EndDate = endDate;
    }
}