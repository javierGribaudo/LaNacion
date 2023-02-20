using AutoMapper;
using LaNacionChallenge.Domain;

namespace LaNacionChallenge.Infrastructure.Mappers
{
    public static class AutoMapper
    {
        public static void InitializeMapper(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ContactRequestModel, Contact>()
                 .ForMember(dest => dest.Id, opt => opt.Ignore())
                 .ForMember(dest =>
                                dest.PhoneNumbers,
                                        opt => opt.MapFrom(src =>
                                                            src.PhoneNumbers.Select(id =>
                                                            new PhoneNumber
                                                            {
                                                                Id = id,
                                                                Active = true
                                                            })))
            .ForMember(dest => dest.Address,
                    opt => opt.MapFrom(src =>
                    new Address
                    {
                        Id = src.Address.HasValue ? (int)src.Address : 0,
                        Active = true
                    }))
                .IgnoreAllSourcePropertiesWithAnInaccessibleSetter();
            });

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
