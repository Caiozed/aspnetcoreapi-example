using AspNetCoreApiExample.Data.Converter;
using AspNetCoreApiExample.Data.VO;
using AspNetCoreApiExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApiExample.Data.Converters
{
    public class UserConverter : IParser<UserVO, User>, IParser<User, UserVO>
    {
        public User Parse(UserVO input)
        {
            if (input == null) return null;
            return new User
            {
                Id = input.Id,
                Login = input.Login,
                AccessKey = input.AccessKey
            };
        }

        public UserVO Parse(User input)
        {
            if (input == null) return null;
            return new UserVO
            {
                Id = input.Id,
                Login = input.Login,
                AccessKey = input.AccessKey
            };
        }

        public List<User> ParseList(List<UserVO> inputList)
        {
            if (inputList == null) return null;
            return inputList.Select(s => Parse(s)).ToList();
        }

        public List<UserVO> ParseList(List<User> inputList)
        {
            if (inputList == null) return null;
            return inputList.Select(s => Parse(s)).ToList();
        }
    }
}
