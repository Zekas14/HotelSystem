using HotelSystem.Data.Enums;
using HotelSystem.Data.Repository;
using HotelSystem.Helper;
using HotelSystem.Models.RoomManagement;
using HotelSystem.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelSystem.Features.RoomManagement.Facilities.Queries
{
    public record GetFaciltyDetilesQuery(int id) : IRequest<ResponseViewModel<FaciltyDTO>>;

    public class GetFaciltyQueryHandler : IRequestHandler<GetFaciltyDetilesQuery, ResponseViewModel<FaciltyDTO>>
    {
        readonly IRepository<Facility> _repository;
        public GetFaciltyQueryHandler(IRepository<Facility> repository)
        {
            _repository = repository;
        }

        public async Task<ResponseViewModel<FaciltyDTO>> Handle(GetFaciltyDetilesQuery request, CancellationToken cancellationToken)
        {
            var Faciltes =await _repository.Get(c=> c.ID == request.id).FirstOrDefaultAsync();
           
            if (Faciltes == null) return new FaluireResponseViewModel<FaciltyDTO>(ErrorCode.ItemNotFound ,"there is no facilty with id")  ;

            var FaciltyMapping = Faciltes.MapTo<FaciltyDTO>();

            return new SuccessResponseViewModel<FaciltyDTO>(FaciltyMapping);
        }
    }
}
