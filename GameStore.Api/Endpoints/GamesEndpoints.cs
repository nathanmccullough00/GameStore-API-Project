using GameStore.Api.Dtos;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";
    private static readonly List<GameDto> games = [
        new (
            1,
            "Street Fighter II",
            "Fighting",
            19.99M,
            new DateOnly(1992,7,15)),
        new(
            2,
            "Final Fantasy VII Rebirth",
            "RPG",
            69.99M,
            new DateOnly(2024,2,29)),
        new(
            3,
            "Astro Bot",
            "Platformer",
            59.99M,
            new DateOnly(2024,9,6)),
    ];

    // Remember all extension methods should be static

    public static void MapGamesEndpoints(this WebApplication app)
    {
        var routeGroup = app.MapGroup("/games");
        // GET /games
        routeGroup.MapGet("/", () => games);

        // GET /games/1
        // Return the part in the list by its unique identifier
        routeGroup.MapGet("/{id}", (int id) =>
        {
            var game = games.Find(game => game.Id == id);

            return game is null ? Results.NoContent() : Results.Ok(game);
        })
        .WithName(GetGameEndpointName);
        // Post endpoint
        // POST /games
        routeGroup.MapPost("/", (CreateGameDto newGame) =>
        {

            if(string.IsNullOrEmpty(newGame.Name))
            {
                return Results.BadRequest("Name is required");
            }
            GameDto game = new(
                games.Count + 1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.releaseDate
            );
            games.Add(game);

            return Results.CreatedAtRoute(GetGameEndpointName, new {id = game.Id}, game);
        });

        // PUT /games/1

        routeGroup.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
        {
            var index = games.FindIndex(game => game.Id == id);

            if (index == -1)
            {
                return Results.NotFound();
            }
            
            games[index] = new GameDto(
                id,
                updatedGame.Name,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate
            );

            return Results.NoContent();
        });

        routeGroup.MapDelete("/{id}", (int id) =>
        {
            games.RemoveAll(game => game.Id == id);

            return Results.NoContent();
        });
    }
}