namespace GameStore.Api.Dtos;


// A DTO is a contaract between the client and server since it represents
// a shared agreement about how data will be transferred and used.
public record class GameDto(
    int Id,
    string Name,
    string Genre,
    decimal Price,
    DateOnly releaseDate
);