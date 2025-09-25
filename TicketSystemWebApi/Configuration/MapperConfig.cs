using AutoMapper;
using System.Runtime.InteropServices;
using TicketSystemWebApi.Data;
using TicketSystemWebApi.Models.ClientAccount;
using TicketSystemWebApi.Models.ClientAccountCompanyAccess;
using TicketSystemWebApi.Models.Companies;
using TicketSystemWebApi.Models.ConsumebledUsed;
using TicketSystemWebApi.Models.ConsumebleItem;
using TicketSystemWebApi.Models.Sla;
using TicketSystemWebApi.Models.Ticket;
using TicketSystemWebApi.Models.TicketCategory;
using TicketSystemWebApi.Models.TicketDetail;
using TicketSystemWebApi.Models.TicketStatus;
using TicketSystemWebApi.Models.UserGroup;

namespace TicketSystemWebApi.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<ClientAccountCreateDto, ClientAccount>().ReverseMap();
            CreateMap<ClientAccountDetailsDto, ClientAccount>().ReverseMap();
            CreateMap<ClientAccountReadOnlyDto, ClientAccount>().ReverseMap();
            //CreateMap<ClientAccount, ClientAccountReadOnlyDto>()
            //.ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company != null ? src.Company.CompanyName: null))
            //.ReverseMap();

            CreateMap<ClientAccountUpdateDto,  ClientAccount>().ReverseMap();

           
            CreateMap<SlaCreateDto, Sla>()
              .ForMember(dest => dest.MinResponseTime, opt => opt.MapFrom(src => TimeSpan.Parse(src.MinResponseTime)))
              .ForMember(dest => dest.MinResolvedTime, opt => opt.MapFrom(src => TimeSpan.Parse(src.MinResolvedTime)));


            CreateMap<SlaUpdateDto, Sla>()
                .ForMember(dest => dest.MinResponseTime, opt => opt.MapFrom(src => TimeSpan.Parse(src.MinResponseTime)))
                .ForMember(dest => dest.MinResolvedTime, opt => opt.MapFrom(src => TimeSpan.Parse(src.MinResolvedTime)));


            CreateMap<Sla, SlaReadOnlyDto>()
                .ForMember(dest => dest.MinResponseTime, opt => opt.MapFrom(src => src.MinResponseTime.ToString(@"hh\:mm\:ss")))
                .ForMember(dest => dest.MinResolvedTime, opt => opt.MapFrom(src => src.MinResolvedTime.ToString(@"hh\:mm\:ss")));


            CreateMap<Sla, SlaDetailsDto>()
                .ForMember(dest => dest.MinResponseTime, opt => opt.MapFrom(src => src.MinResponseTime.ToString(@"hh\:mm\:ss")))
                .ForMember(dest => dest.MinResolvedTime, opt => opt.MapFrom(src => src.MinResolvedTime.ToString(@"hh\:mm\:ss")));



            CreateMap<ConsumebledUsedCreateDto, ConsumebledUsed>().ReverseMap();
            CreateMap<ConsumebledUsedDetailsDto, ConsumebledUsed>().ReverseMap();
            CreateMap<ConsumebledUsedReadOnlyDto, ConsumebledUsed>().ReverseMap();
            CreateMap<ConsumebledUsedUpdateDto, ConsumebledUsed>().ReverseMap();

            CreateMap<TicketCategoryCreateDto, TicketCategory>().ReverseMap();
            CreateMap<TicketCategoryDetailsDto, TicketCategory>().ReverseMap();
            CreateMap<TicketCategoryReadOnlyDto, TicketCategory>().ReverseMap();
            CreateMap<TicketCategoryUpdateDto, TicketCategory>().ReverseMap();


            CreateMap<UserGroupCreateDto, UserGroup>().ReverseMap();
            CreateMap<UserGroupDetailDto, UserGroup>().ReverseMap();
            CreateMap<UserGroupReadOnlyDto, UserGroup>().ReverseMap();
            CreateMap<UserGroupUpdateDto, UserGroup>().ReverseMap();

            CreateMap<TicketCreateDto, Ticket>().ReverseMap();
            CreateMap<TicketDetailsDto, Ticket>().ReverseMap();
            CreateMap<TicketReadOnlyDto , Ticket>().ReverseMap();
            CreateMap<TicketUpdateDto, Ticket>().ReverseMap();



            CreateMap<TicketDetail, TicketDetailDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.TicketStatus.Status))
                .ForMember(dest => dest.TicketTitle, opt => opt.MapFrom(src => src.Ticket.Title))
                .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Ticket.ClientAccount.FirstName + " " + src.Ticket.ClientAccount.LastName));

            CreateMap<TicketDetail, TicketDetailReadOnlyDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.TicketStatus.Status));

            CreateMap<TicketDetailCreateDto, TicketDetail>();
            CreateMap<TicketDetailUpdateDto, TicketDetail>();



            CreateMap<TicketStatus, TicketStatusReadOnly>();
            CreateMap<TicketStatus, TicketStatusDetailsDto>();
            CreateMap<TicketStatusCreateDto, TicketStatus>();
            CreateMap<TicketStatusUpdateDto, TicketStatus>().ReverseMap();


            CreateMap<ConsumebleItemCreateDto, ConsumebleItem>().ReverseMap();
            CreateMap<ConsumebleItemDetailsDto, ConsumebleItem>().ReverseMap();
            CreateMap<ConsumebleItemReadOnlyDto, ConsumebleItem>().ReverseMap();
            CreateMap<ConsumebleItemUpdateDto, ConsumebleItem>().ReverseMap();

            CreateMap<CompaniesCreateDto, Company>().ReverseMap();
            CreateMap<CompaniesDetailsDto, Company>().ReverseMap();
            CreateMap<CompaniesReadOnly, Company>().ReverseMap();
            CreateMap<CompaniesUpdateDto, Company>().ReverseMap();

            CreateMap<ClientAccountCompanyAccessCreateDto, ClientAccountCompanyAccess>().ReverseMap();
            CreateMap<ClientAccountCompanyAccess, ClientAccountCompanyAccessReadOnly>().ReverseMap();






        }


    }
}
