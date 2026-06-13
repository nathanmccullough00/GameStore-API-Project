using gameStore.Api.Models;

namespace GameStore.Api.Models;

// Data model is going to contain all of the classes
// that will be used to create objects that will need to be
// persisted in the database tables.
public class Game
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public Genre? Genre { get; set; }

    public int GenreID { get; set; }

    public decimal Price { get; set; }

    public DateOnly ReleaseDate { get; set; }
}