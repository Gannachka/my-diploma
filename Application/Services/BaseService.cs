namespace Application.Services
{
    using AutoMapper;
    using Persistence;

    public class BaseService
    {
        public readonly CovidHelperContext context;
        public readonly IMapper mapper;
        public BaseService(CovidHelperContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
    }
}
