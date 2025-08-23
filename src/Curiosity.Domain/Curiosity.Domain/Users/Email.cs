namespace Curiosity.Domain.Users;

/// <summary>
/// Represents an email address as a value object.
/// </summary>
/// <remarks>This struct is immutable and ensures that the email address is treated as a single, cohesive value.
/// Use this type to encapsulate email addresses in a strongly-typed manner.</remarks>
/// <param name="Value"></param>
public readonly record struct Email(string Value);
