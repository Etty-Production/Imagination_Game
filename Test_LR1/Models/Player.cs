using Microsoft.Extensions.Configuration.UserSecrets;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Drawing;

namespace Test_LR1.Models
{
    public class Player
    {
        [Key]
        public string UserId { get; private set; } = string.Empty;
        [Required]
        public int Location_X { get; set; }
        [Required]
        public int Location_Y { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public int HP { get; set; }

        public Player(string userId, int HP)
        {
            UserId = userId;
            this.HP = HP;
            IsActive = true;
        }
        public static bool TestUserId(string userId)
        {
            return !string.IsNullOrEmpty(userId);
        }

        public Player Clone()
        {
           return (Player)this.MemberwiseClone();
        }
    }
}
