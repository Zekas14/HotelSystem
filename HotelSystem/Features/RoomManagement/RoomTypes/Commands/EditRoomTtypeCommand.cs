using HotelSystem.Data.Enums;
using HotelSystem.Data.Repository;
using HotelSystem.Features.RoomManagement.RoomTypes.Queries;
using HotelSystem.Models.RoomManagement;
using HotelSystem.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System.Reflection.Metadata.Ecma335;

namespace HotelSystem.Features.RoomManagement.RoomTypes.Commands
{
    public record EditRoomTtypeCommand(int id, string name, double price) : IRequest<ResponseViewModel<bool>>;


    public class EditRoomtypeCommandHandler : IRequestHandler<EditRoomTtypeCommand, ResponseViewModel<bool>>
    {
        readonly IRepository<RoomType> _repository;
        readonly IMediator _mediator;

        public EditRoomtypeCommandHandler(IMediator mediator, IRepository<RoomType> repository)
        {
            _repository = repository;

            _mediator = mediator;

        }

        public async Task<ResponseViewModel<bool>> Handle(EditRoomTtypeCommand request, CancellationToken cancellationToken)
        {
            
            var valdiation = await valildateRequest(request);
            if (!valdiation.IsSuccess) return valdiation;

            var EditingRoomType = new RoomType {ID= request.id ,  Name = request.name, Price = request.price };

            _repository.SaveInclude(EditingRoomType, nameof(EditingRoomType.Name), nameof(EditingRoomType.Price));
            _repository.SaveChanges();
            return valdiation;
        }

        private async Task<ResponseViewModel<bool>> valildateRequest(EditRoomTtypeCommand editRoomTtype)
        {

            if (string.IsNullOrEmpty(editRoomTtype.name)) return new FaluireResponseViewModel<bool>(ErrorCode.FieldIsEmpty, "name is requird ");

            if (editRoomTtype.price < 0) return new FaluireResponseViewModel<bool>(ErrorCode.InvalidInput, " price must be greater than 0 ");

            var isExist = await _mediator.Send(new IsRoomTypeExistsQuery(editRoomTtype.name));

            if (isExist) return new FaluireResponseViewModel<bool>(ErrorCode.ItemNotFound, "there no item with this name ");

            return new SuccessResponseViewModel<bool>(true);
        }
    }


}
