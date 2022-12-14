using AutoMapper;
using Grpc.Core;
using StoreService.Data;

namespace StoreService.SyncDataServices.Grpc
{
    public class GrpcStoreService : GrpcStore.GrpcStoreBase
    {
        private readonly IStoreRepo _repository;
        private readonly IMapper _mapper;

        public GrpcStoreService(IStoreRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public override Task<StoreResponse> GetAllStores(GetAllRequest request, ServerCallContext context)
        {
            var response = new StoreResponse();
            var stores = _repository.GetAllStores();

            foreach(var store in stores)
            {
                response.Store.Add(_mapper.Map<GrpcStoreModel>(store));
            }

            return Task.FromResult(response);
        }
    }
}