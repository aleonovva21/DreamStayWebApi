using DreamStayWebApi.Models.Rooms;
using FluentValidation;

namespace DreamStayWebApi.Validators;

public sealed class RoomValidator : AbstractValidator<Room>
{
	public RoomValidator()
	{
		RuleFor(x => x.Photo).NotEmpty().WithMessage("Please specify a room photo");
		RuleFor(x => x.Photo).Length(10, 50).WithMessage("Room type is too long. Expected more than 9 and less than 51");
		RuleFor(x => x.RoomType).NotEmpty().WithMessage("Please specify a room type");
		RuleFor(x => x.RoomType).Length(5, 15).WithMessage("Room type is too long. Expected more than 4 and less than 16");
		RuleFor(x => x.PricePerDay).GreaterThan(0);
	}
}

public sealed class UpdateRoomValidator : AbstractValidator<UpdateRoom>
{
	public UpdateRoomValidator()
	{
		RuleFor(x => x.Photo).Length(10, 50).WithMessage("Room photo is too long. Expected more than 9 and less than 51");
		RuleFor(x => x.RoomType).Length(5, 15).WithMessage("Room type is too long. Expected more than 4 and less than 16");
	}
}
