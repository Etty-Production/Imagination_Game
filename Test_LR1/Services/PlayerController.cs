using Test_LR1.Models;

namespace Test_LR1.Services
{
    public class PlayerController
    {

        private readonly Dictionary<string, Player> _playerList;
        public PlayerController()
        {
            _playerList = new Dictionary<string, Player>();
        }
    }
}
