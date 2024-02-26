using ActivityCheck.Domain.Entity;
using ActivityCheck.Domain.Response;
using ActivityCheck.Domain.ViewEntity.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityCheck.Service.Interfaces
{
    public interface IUserService
    {
        public Task<BaseResponse<IEnumerable<User>>> GetUsers();
        public Task<BaseResponse<User>> GetUser(long Id);

        public Task<BaseResponse<User>> GetUser(string UserName, string password);
        public Task<BaseResponse<User>> FindUser(string UserName, string Email);
        public Task<BaseResponse<bool>> DeleteUser(long Id);
        public Task<BaseResponse<User>> CreateUser(UserViewModel entity);
        public Task<BaseResponse<User>> UpdateUser(long Id, UserViewModel entity);
    }
}
