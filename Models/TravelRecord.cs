namespace TravelRecords.Models;

public record Expense(
    int id,
    string title,
    float amount,
    string createdAt,
    string updatedAt,
    int payerId,
    int accountId,
    int travelRecordId
);

public record TravelRecord(
    int id,
    string title,
    string from,
    string to,
    string createdAt,
    string updatedAt,
    int ownerId
);

public record UsersOnTravelRecord(
    int id,
    int userId,
    int travelRecordId,
    string joined
);