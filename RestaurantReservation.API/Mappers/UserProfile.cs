﻿using AutoMapper;
using RestaurantReservation.API.Contracts.Requests;
using RestaurantReservation.Domain.Users;

namespace RestaurantReservation.API.Mappers;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserCreate, User>();
    }
}