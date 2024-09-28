using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitChat.Application.Models.Dtos.User
{
    public class LoginResponseDto
    {
        public UserDto User { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public Guid LoginHistoryId { get; set; }
    }
}
