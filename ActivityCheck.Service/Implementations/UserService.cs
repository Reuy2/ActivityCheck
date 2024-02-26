using ActivityCheck.Domain.Entity;
using ActivityCheck.Domain.Response;
using ActivityCheck.Domain.ViewEntity.User;
using ActivityCheck.Service.Interfaces;
using ActivityCheck.DAL.Repositories;
using ActivityCheck.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ActivityCheck.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly IBaseRepository<User> _userRepository;
        public UserService(IBaseRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<BaseResponse<User>> CreateUser(UserViewModel entity)
        {
            var response = new BaseResponse<User>();
            try
            {
                var userFromDb = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == entity.Name);
                if (userFromDb is not null)
                {
                    response.Description = "User Already Exist";
                    response.StatusCode = Domain.Enum.StatusCode.NotFound;
                    return response;
                }
                var user = new User()
                {
                    Name = entity.Name,
                    Email = entity.Email,
                    Password = entity.Password
                };

                await _userRepository.Create(user);
                response.StatusCode = Domain.Enum.StatusCode.Ok;
                response.Data = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == entity.Name);
                return response;
            }
            catch(Exception ex)
            {
                response.StatusCode = Domain.Enum.StatusCode.InternalServerError;
                response.Description = $"[CreateUser]: {ex.Message}";
                return response;
            }
        }

        public async Task<BaseResponse<bool>> DeleteUser(long Id)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Id == Id);
                if(user is null)
                {
                    response.StatusCode = Domain.Enum.StatusCode.NotFound;
                    response.Description = "User Dosent Exist";
                    response.Data = false;
                    return response;
                }
                await _userRepository.Delete(user);
                response.StatusCode = Domain.Enum.StatusCode.Ok;
                response.Data = true;
                return response;
            }
            catch(Exception ex )
            {
                response.StatusCode = Domain.Enum.StatusCode.InternalServerError;
                response.Description = $"[DeleteUser]: {ex.Message}";
                response.Data = false;
                return response;
            }
        }

        public async Task<BaseResponse<User>> FindUser(string UserName, string Email)
        {
            var response = new BaseResponse<User>();
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Email == Email || x.Name == UserName);

                if (user is null)
                {
                    response.StatusCode = Domain.Enum.StatusCode.NotFound;
                    response.Description = "Юзер не найден";
                    return response;
                }

                response.StatusCode = Domain.Enum.StatusCode.Ok;
                response.Data = user;
                return response;
            }
            catch (Exception ex)
            {
                response.StatusCode = Domain.Enum.StatusCode.InternalServerError;
                response.Description = $"[FindUser]: {ex.Message}";
                return response;
            }
        }

        public async Task<BaseResponse<User>> GetUser(long Id)
        {
            var response = new BaseResponse<User>();
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x=>x.Id == Id);
                if(user is null)
                {
                    response.StatusCode = Domain.Enum.StatusCode.NotFound;
                    response.Description = "User Dosen't Exist";
                    return response;
                }
                response.StatusCode = Domain.Enum.StatusCode.Ok;
                response.Data = user;
                return response;
            }
            catch(Exception ex )
            {
                response.StatusCode = Domain.Enum.StatusCode.InternalServerError;
                response.Description = $"[GetUser]: {ex.Message}";
                return response;
            }
        }

        public async Task<BaseResponse<User>> GetUser(string UserName, string password)
        {
            var response = new BaseResponse<User>();
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Password == password && x.Name == UserName);

                if(user is null)
                {
                    response.StatusCode = Domain.Enum.StatusCode.NotFound;
                    response.Description = "Юзер не найден";
                    return response;
                }

                response.StatusCode = Domain.Enum.StatusCode.Ok;
                response.Data = user;
                return response;
            }
            catch(Exception ex)
            {
                response.StatusCode = Domain.Enum.StatusCode.InternalServerError;
                response.Description = $"[GetUser]: {ex.Message}";
                return response;
            }
        }

        public async Task<BaseResponse<IEnumerable<User>>> GetUsers()
        {
            var response = new BaseResponse<IEnumerable<User>>();
            try
            {
                var users = await _userRepository.GetAll().ToListAsync() ;
                if(users is null)
                {
                    response.Description = "Users Not Found";
                    response.StatusCode = Domain.Enum.StatusCode.NotFound;
                    return response;
                }
                response.StatusCode = Domain.Enum.StatusCode.Ok;
                response.Data = users;
                return response;
            }
            catch(Exception ex )
            {
                response.StatusCode = Domain.Enum.StatusCode.InternalServerError;
                response.Description = $"[GetUsers]: {ex.Message}";
                return response;
            }
        }

        public async Task<BaseResponse<User>> UpdateUser(long Id, UserViewModel entity)
        {
            var response = new BaseResponse<User>();
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Id == Id);
                if(user is null)
                {
                    response.StatusCode = Domain.Enum.StatusCode.NotFound;
                    response.Description = "User Not Found";
                    return response;
                }
                user = new User()
                {
                    Name = entity.Name,
                    Password = entity.Password
                };

                await _userRepository.Update(user);
                response.StatusCode = Domain.Enum.StatusCode.Ok;
                response.Data = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == entity.Name);
                return response;
            }
            catch(Exception ex)
            {
                response.StatusCode = Domain.Enum.StatusCode.InternalServerError;
                response.Description = $"[UpdateUser]: {ex.Message}";
                return response;
            }
        }
    }
}
