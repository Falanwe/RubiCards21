using ShiFuMiTournament.Models;
using System.Threading.Tasks;

namespace ShiFuMiTournament.Services
{
    public interface IShiFuMiService
    {
        Task<Game> GetGame();
        Task<RoundState[]> GetGameState(string gameId);
        Task Play(string gameId, IndividualPlay play);
        Task<GameResult> GetResult(string gameId);
    }
}