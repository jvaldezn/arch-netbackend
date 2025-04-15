using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Dto;
using Data.Util;

namespace Domain.UseCase
{
    public interface IUserUseCase
    {
        Task<Response<string>> AuthenticateUser(string username, string password);
        Task<Response<IEnumerable<UserDto>>> GetAllUsers();
        Task<Response<UserDto>> GetUserById(int id);
        Task<Response<UserDto>> CreateUser(UserDto dto);
        Task<Response<UserDto>> UpdateUser(int id, UserDto dto);
        Task<Response<bool>> DeleteUser(int id);
    }
}
