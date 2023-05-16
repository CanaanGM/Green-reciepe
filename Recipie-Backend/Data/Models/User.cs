using Microsoft.AspNetCore.Identity;

using System.Collections.Generic;

namespace Recipie_Backend.Data.Models
{
    public class User : IdentityUser
    {
        public List<Recipe> AddedRecipies { get; set; } = new List<Recipe>();
        public List<Recipe> FavouriteRecipies { get; set; } = new List<Recipe>();
        public string DisplayName { get; internal set; }

        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
        // tracking nonesense here, like history of votes, history of whatever
        // profile pic goes here and other crap
    }
}
