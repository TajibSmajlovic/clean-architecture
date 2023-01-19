using AutoMapper;
using Kinoteka.TicketManagement.Application.Features.Categories.Commands.CreateCateogry;
using Kinoteka.TicketManagement.Application.Features.Categories.Queries.GetCategoriesList;
using Kinoteka.TicketManagement.Application.Features.Categories.Queries.GetCategoriesListWithEvents;
using Kinoteka.TicketManagement.Application.Features.Events.Commands.CreateEvent;
using Kinoteka.TicketManagement.Application.Features.Events.Commands.DeleteEvent;
using Kinoteka.TicketManagement.Application.Features.Events.Commands.UpdateEvent;
using Kinoteka.TicketManagement.Application.Features.Events.Queries.GetEventDetail;
using Kinoteka.TicketManagement.Application.Features.Events.Queries.GetEventList;
using Kinoteka.TicketManagement.Application.Features.Events.Queries.GetEventsExport;
using Kinoteka.TicketManagement.Domain.Entities;

namespace Kinoteka.TicketManagement.Application.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Event, EventListVm>().ReverseMap();
            CreateMap<Event, EventDetailsVm>().ReverseMap();
            CreateMap<Event, CreateEventCommand>().ReverseMap();
            CreateMap<Event, UpdateEventCommand>().ReverseMap();
            CreateMap<Event, DeleteEventCommand>().ReverseMap();
            CreateMap<Event, EventExportDto>();

            CreateMap<Category, CategoryDto>();
            CreateMap<Category, CategoryListVm>();
            CreateMap<Category, CategoryEventListVm>();
            CreateMap<Category, CreateCategoryCommand>();
            CreateMap<Category, CreateCategoryDto>();
        }
    }
}