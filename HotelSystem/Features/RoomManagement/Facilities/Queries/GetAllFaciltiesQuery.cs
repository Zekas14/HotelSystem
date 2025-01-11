using AutoMapper.QueryableExtensions;
using HotelSystem.Data.Repository;
using HotelSystem.Features.RoomManagement.RoomTypes.Queries;
using HotelSystem.Helper;
using HotelSystem.Models.RoomManagement;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelSystem.Features.RoomManagement.Facilities.Queries
{
    public record GetFaciltesQuery() : IRequest<IEnumerable<FaciltyDTO>>;

    public class GetFaciltiesQueryHandler : IRequestHandler<GetFaciltesQuery, IEnumerable<FaciltyDTO>>
    {
        readonly IRepository<Facility> _repository;
        public GetFaciltiesQueryHandler(IRepository<Facility> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<FaciltyDTO>> Handle(GetFaciltesQuery request, CancellationToken cancellationToken)
        {
            var faciltes = _repository.GetAll();
            if (faciltes == null) return Enumerable.Empty<FaciltyDTO>();

            var faciltymapping = await faciltes.ProjectTo<FaciltyDTO>().ToListAsync();

            return faciltymapping ;
        }
    }
}
