using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZwajApp.Api.ViewModels;
using ZwajApp.API.Models;

namespace ZwajApp.Api.Helpers
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForListVM>()
                .ForMember(dest => dest.PhotoUrl, opt => { opt.MapFrom(u => u.Photos.FirstOrDefault(p => p.IsMain).Url); })
                .ForMember(dest => dest.Age,opt => { opt.ResolveUsing(u=> u.DateOfBirth.CalculateAge()); });
            CreateMap<User, UserForDetailsVM>()
                .ForMember(dest => dest.PhotoUrl , opt => { opt.MapFrom(u => u.Photos.FirstOrDefault(p => p.IsMain).Url); })
                .ForMember(dest => dest.Age , opt => { opt.ResolveUsing(u => u.DateOfBirth.CalculateAge()); });
            CreateMap<Photo, PhotoForDetailsVM>();
        }
    }
}
