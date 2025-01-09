using HotelSystem.Data.Repository;
using HotelSystem.Models.RoomManagement;
using HotelSystem.ViewModels.RoomManagment.Rooms;
using HotelSystem.ViewModels.RoomReservations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PredicateExtensions;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace HotelSystem.Features.RoomReservations
{
    public record GetRoomsQuery(int? roomTypeID = null, double? fromAmount = null, double? toAmount = null) : IRequest<IEnumerable<RoomViewModel>>;

    public class GetRoomsQueryHandler : IRequestHandler<GetRoomsQuery, IEnumerable<RoomViewModel>>
    {
        IRepository<Room> _repository;
        public GetRoomsQueryHandler(IRepository<Room> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<RoomViewModel>> Handle(GetRoomsQuery request, CancellationToken cancellationToken)
        {
            var predicate = BuildPredicate(request);

            var rooms = await _repository.Get(predicate)
                .Select(x => new RoomViewModel
                { 
                    ID = x.ID,
                    RoomTypeID = x.RoomTypeID,
                    RoomTypeName = x.RoomType.Name,
                    BasicPrice = x.RoomType.Price,
                }).ToListAsync();

            return rooms;
        }

        private Expression<Func<Room, bool>> BuildPredicate(GetRoomsQuery request)
        {
            var predicate = PredicateExtensions.PredicateExtensions.Begin<Room>(true);

            predicate.And(x => !request.roomTypeID.HasValue || x.RoomTypeID == request.roomTypeID.Value);

            predicate.And(x => !request.fromAmount.HasValue || x.RoomType.Price >= request.fromAmount.Value);

            predicate.And(x => !request.toAmount.HasValue || x.RoomType.Price >= request.toAmount.Value);

            return predicate;
        }
    }
}
