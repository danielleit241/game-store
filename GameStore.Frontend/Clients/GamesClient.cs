using GameStore.Frontend.Models;

namespace GameStore.Frontend.Clients
{
    public class GamesClient(GenresClient genresClient)
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

        private readonly Genre[] genres = genresClient.GetGenres();

        public GameSummary?[] GetGames()
        {
            return [.. games];
        }

        public void AddGame(GameDetails game)
        {
            var genre = GetGenreById(game.GenreId);
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

        public void UpdateGame(GameDetails updateGame)
        {
            var genre = GetGenreById(updateGame.GenreId);
            var gameSumary = GetGameSummaryById(updateGame.Id) ?? throw new ArgumentException("Game not found");
            gameSumary.Name = updateGame.Name;
            gameSumary.Price = updateGame.Price;
            gameSumary.Genre = genre.Name;
            gameSumary.ReseleaseDate = updateGame.ReseleaseDate;
        }

        public void DeleteGame(int id)
        {
            var game = GetGameSummaryById(id) ?? throw new ArgumentException("Game not found");
            games.Remove(game);
        }

        private Genre GetGenreById(string? id)
        {
            var genre = genres.FirstOrDefault(g => g.Id == int.Parse(id ?? ""));
            return genre == null ? throw new ArgumentException("Invalid genre ID") : genre;
        }

        public GameDetails? GetGameDetailsById(int id)
        {
            var game = GetGameSummaryById(id);
            var genresId = genres.FirstOrDefault(g => g.Name == game?.Genre)?.Id.ToString();
            return new GameDetails
            {
                Id = game.Id,
                Name = game.Name,
                GenreId = genresId,
                Price = game.Price,
                ReseleaseDate = game.ReseleaseDate
            };
        }

        private GameSummary? GetGameSummaryById(int? id)
        {
            var game = games.Find(g => g?.Id == id);
            if (game == null)
            {
                return null;
            }
            return game;
        }
    }
}
