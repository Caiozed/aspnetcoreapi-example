using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
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
    public class FileBusiness : IFileBusiness
    {
        public FileBusiness()
        {

        }

        public string GetFile()
        {
            var path = Directory.GetCurrentDirectory();
            path = path + "\\Other\\Teste.pdf";
            return path;
        }
    }
}
