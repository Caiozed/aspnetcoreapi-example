using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using AspNetCoreApiExample.Data.Converters;
using AspNetCoreApiExample.Data.VO;
using AspNetCoreApiExample.Models;
using AspNetCoreApiExample.Repository;
using AspNetCoreApiExample.Security.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AspNetCoreApiExample.Business.Implementations
{
    public class UserBusiness : IUserBusiness
    {
        private IUserRepository _repository;
        private SignInConfiguration _signInConfiguration;
        private TokenConfiguration _tokenConfiguration;
        private UserConverter _converter;


        public UserBusiness(IUserRepository repository, SignInConfiguration signInConfiguration, TokenConfiguration tokenConfiguration)
        {
            _repository = repository;
            _signInConfiguration = signInConfiguration;
            _tokenConfiguration = tokenConfiguration;
            _converter = new UserConverter();
        }

        public object FindByLogin(UserVO userVO)
        {
            bool credentialsIsValid = false;
            var user = _converter.Parse(userVO);

            if (user != null && !string.IsNullOrWhiteSpace(user.Login))
            {
                var baseUser = _repository.FindByLogin(user.Login);
                credentialsIsValid = (baseUser != null && user.Login == baseUser.Login && user.AccessKey == baseUser.AccessKey);
            }
            if (credentialsIsValid)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(user.Login, "Login"),
                        new[] {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                            new Claim(JwtRegisteredClaimNames.UniqueName, user.Login),
                        }
                    );
                DateTime createDate = DateTime.Now;
                DateTime expirationDate = createDate + TimeSpan.FromSeconds(_tokenConfiguration.Seconds);

                var handler = new JwtSecurityTokenHandler();
                string token = CreateToken(identity, createDate, expirationDate, handler);

                return SucessObject(createDate, expirationDate, token);
            }
            else
            {
                return ExceptionObject();
            }
        }

        private string CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expirationDate, JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor()
            {
                Issuer = _tokenConfiguration.Issuer,
                Audience = _tokenConfiguration.Audience,
                SigningCredentials = _signInConfiguration.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate
            });

            var token = handler.WriteToken(securityToken);
            return token;
        }

        private object SucessObject(DateTime createDate, DateTime experitaionDate, string token)
        {
            return new
            {
                autheticated = true,
                createdAt = createDate.ToString("yyyy-MM-dd HH:mm::ss"),
                expriresAt = experitaionDate.ToString("yyyy-MM-dd HH:mm::ss"),
                accessToken = token,
                message = "OK"
            };
        }

        private object ExceptionObject()
        {
            return new
            {
                autheticated = false,
                message = "Failed To authenticate!"
            };
        }
    }
}
