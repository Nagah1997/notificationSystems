using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace _4EPlatForm.Infrastrucure.UnitOfWork
{
    public class LoggedInUserProvider
    {
        public static Guid GetLoggedInUserId(IHttpContextAccessor httpContextAccessor)
        {

            //Guid userId = Guid.Parse("3eb17e81-ede8-4b02-90a1-3625e695af03");

            //return userId;

            try
            {
                var userIdClaim = httpContextAccessor.HttpContext.User.Claims
                    .SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out Guid userId))
                {
                    return userId;
                }

                throw new UnauthorizedAccessException("User ID claim not found or not valid.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new UnauthorizedAccessException("Failed to retrieve logged-in user ID.");
            }
        }
    }
}



