using AutoMapper;
using EmployeeService.Models;
using Grpc.Net.Client;
using StoreService;

namespace EmployeeService.SyncDataServices.Grpc
{
    public class StoreDataClient : IStoreDataClient
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public StoreDataClient(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public IEnumerable<Store> ReturnAllStores()
        {
            Console.WriteLine($"--> Calling GRPC Service {_configuration["GrpcStore"]}");
            var channel = GrpcChannel.ForAddress(_configuration["GrpcStore"]);
            var client = new GrpcStore.GrpcStoreClient(channel);
            var request = new GetAllRequest();

            try
            {
                var reply = client.GetAllStores(request);
                return _mapper.Map<IEnumerable<Store>>(reply.Store);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Couldnot call GRPC Server {ex.Message}");
                return null;
            }
        }
    }
}