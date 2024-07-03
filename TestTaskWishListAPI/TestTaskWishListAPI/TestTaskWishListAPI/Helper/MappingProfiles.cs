using AutoMapper;
using TestTaskWishListAPI.Dto;
using TestTaskWishListAPI.Models;

namespace TestTaskWishListAPI.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<WishItem, WishItemDto>();
            CreateMap<WishItemDto, WishItem>();
        }
    }
}
