using AutoMapper;
using MultiSync.Models.Item;

namespace MultiSync
{
    public class AppMapping : Profile
    {
        public AppMapping()
        {
            CreateMap<MSItem, ItemViewModel>()
                .ReverseMap();
            CreateMap<XMLItem, ItemViewModel>()
                .ReverseMap();
            CreateMap<ItemViewModel, BsonItem>()
                .ReverseMap();
        }
    }
}