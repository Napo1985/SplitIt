using System;
using AutoMapper;
using Splitit.App.Models;
using Splitit.Splitit.Entities;
using Splitit.Splitit.ValueObjects;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Splitit.App.Mapping
{
    public class ActorProfile : Profile
    {
        public ActorProfile()
        {
            CreateMap<Actor, ActorRequest>()
                .ForMember(dest => dest.Rank, opt => opt.MapFrom(src => src.Rank.Value))
                .ReverseMap()
                .ForMember(dest => dest.Rank, opt => opt.MapFrom(src => new Rank(src.Rank)));
        }
    }
}

