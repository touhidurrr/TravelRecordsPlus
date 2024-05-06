namespace TravelRecords.Models;

public record Account(
    int id,
    string name,
    float balance,
    string createdAt,
    string updatedAt,
    int ownerId
);