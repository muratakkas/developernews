using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Dews.Api.Extensions
{
    public static class UserExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal User)
        {
            return Guid.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
        }
    }
}
