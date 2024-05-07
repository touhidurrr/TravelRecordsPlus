namespace TravelRecords.Models;

public record Transaction(
    int id,
    string @ref,
    float amount,
    string createdAt,
    int ownerId,
    int accountId
);