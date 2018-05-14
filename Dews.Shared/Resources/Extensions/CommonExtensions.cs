using System;
using System.Collections.Generic;
using System.Text;

namespace Dews.Shared.Resources.Extensions
{
    public static class CommonExtensions
    {
        public static bool CheckIsNull(this string value)
        {
            return value == null || value.Length <= 0;
        }
    }
}
