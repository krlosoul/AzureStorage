namespace AzureStorage.Application.Common.Mapper
{
    using AzureStorage.Domain.Common.Enums;
    using AzureStorage.Domain.Dtos;
    using AzureStorage.Domain.Entities;
    using AutoMapper;
    using Newtonsoft.Json;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Audit, AuditDto>()
                .ForMember(dest => dest.OldData, opt => opt.MapFrom(src => JsonConvert.DeserializeObject(src.OldData)))
                .ForMember(dest => dest.NewData, opt => opt.MapFrom(src => JsonConvert.DeserializeObject(src.NewData)))
                .ForMember(dest => dest.TransactionDate, opt => opt.MapFrom(src => src.Timestamp.ToString("MM/dd/yyyy HH:mm")))
                .ForMember(dest => dest.TransactionName, opt => opt.MapFrom(src => (TransactionTypeEnum)Enum.Parse(typeof(TransactionTypeEnum), src.TransactionTypeId.ToString())))
                .ForMember(dest => dest.AuditId, opt => opt.MapFrom(src => src.RowKey))
                .ForMember(dest => dest.Entity, opt => opt.MapFrom(src => src.PartitionKey));

            CreateMap<CreateAuditDto, Audit>()
                .ForMember(dest => dest.PartitionKey, opt => opt.MapFrom(src => src.Entity));
        }
    }
}