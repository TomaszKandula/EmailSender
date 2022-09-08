using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;

namespace EmailSender.Backend.Domain;

[ExcludeFromCodeCoverage]
public abstract class Entity<TKey>
{
    [Key]
    public TKey Id { get; init; }
}