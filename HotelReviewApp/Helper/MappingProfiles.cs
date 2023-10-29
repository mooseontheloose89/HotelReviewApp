﻿using AutoMapper;
using HotelReviewApp.DTO;
using HotelReviewApp.Models;

namespace HotelReviewApp.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<Hotel, HotelDTO>();
            CreateMap<Review, ReviewDTO>();
            CreateMap<User, UserDTO>();
        }
    }
}