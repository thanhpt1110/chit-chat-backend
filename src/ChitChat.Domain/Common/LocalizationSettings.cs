using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitChat.Domain.Common
{
    public struct LocalizationSettings
    {
        public static readonly string[] Cultures = new[] { "vi-VN", "en-US" };

        public static readonly string DefaultCulture = Cultures[0];

        public static readonly string CultureQueryString = "culture";
    }

}
