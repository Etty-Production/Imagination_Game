using Microsoft.Extensions.Configuration.UserSecrets;
using System.Data.SqlTypes;
using System.Drawing;

namespace Test_LR1.Models
{
    public class Player
    {
        public string UserId { get; set; } = string.Empty;
        public Point Location { get; set; } = new Point();
        public bool IsActive;
        public static bool TestUserId(string userId)
        {
            return string.IsNullOrEmpty(userId);
        }
    }
}
