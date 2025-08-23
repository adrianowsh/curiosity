namespace Curiosity.Domain.Users;

/// <summary>
/// Represents a name as a read-only value.
/// </summary>
/// <remarks>This struct is immutable and is intended to encapsulate a name as a single string value.</remarks>
/// <param name="Value"></param>
public readonly record struct Name(string Value);
