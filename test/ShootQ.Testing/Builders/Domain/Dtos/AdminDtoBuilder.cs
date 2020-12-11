using ShootQ.Core.Models;
using ShootQ.Core.ValueObjects;
using ShootQ.Domain.Features.Admins;
using System;

namespace ShootQ.Testing.Builders.Domain.Dtos
{
    public class AdminDtoBuilder
    {
        private AdminDto _adminDto;

        public static AdminDto WithDefaults()
        {
            return new AdminDto(Guid.NewGuid(),"quinntyne",(Email)"quinntynebrown@gmail.com");
        }

        public AdminDtoBuilder()
        {
            _adminDto = WithDefaults();
        }

        public AdminDto Build()
        {
            return _adminDto;
        }
    }
}
