using GameStore.Frontend.Models;

namespace GameStore.Frontend.Clients
{
    public class GameClient
    {
        private readonly List<GameSummary?> games =
        [
            new(){
                Id = 1,
                Name = "The Witcher 3: Wild Hunt",
                Genre = "RPG",
                Price = 49.99m,
                ReseleaseDate = new DateOnly(2015, 5, 19)
            },
            new(){
                Id = 2,
                Name = "DOOM Eternal",
                Genre = "FPS",
                Price = 29.99m,
                ReseleaseDate = new DateOnly(2020, 3, 20)
            },
            new(){
                Id = 3,
                Name = "GTA V",
                Genre = "Action",
                Price = 30.00m,
                ReseleaseDate = new DateOnly(2020, 3, 24)
            }
        ];

        private readonly Genre[] genres = new GenresClient().GetGenres();

        public GameSummary?[] GetGames()
        {
            return [.. games];
        }

        public void AddGame(GameDetails game)
        {
            var genre = genres.ToList().FirstOrDefault(g => g.Id == int.Parse(game.GenreId!));
            if (genre == null)
            {
                throw new ArgumentException("Invalid genre ID");
            }
            var newGame = new GameSummary
            {
                Id = games.Count + 1,
                Name = game.Name,
                Genre = genre.Name,
                Price = game.Price,
                ReseleaseDate = game.ReseleaseDate
            };
            games.Add(newGame);
        }
    }
}
