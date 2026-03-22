using DreamStay.Domain.Models;
using DreamStayWebApi.Models.Rooms;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace DreamStayWebApi.Controllers;

[Authorize]
[ApiController]
public sealed class RoomController(
	HostelContext context,
	ILogger<RoomController> logger,
	IValidator<Models.Rooms.Room> roomValidator,
	IValidator<Models.Rooms.UpdateRoom> updateRoomValidator
	) : ControllerBase
{
	private readonly HostelContext _context = context;
	private readonly ILogger _logger = logger;
	private readonly IValidator<Models.Rooms.Room> _roomValidator = roomValidator;
	private readonly IValidator<Models.Rooms.UpdateRoom> _updateRoomValidator = updateRoomValidator;

	[HttpGet("rooms")]
	public IActionResult GetRooms()
	{
		try
		{
			var rooms = _context.Rooms.
				AsEnumerable().
				DistinctBy(room => room.RoomType);

			return Ok(rooms);
		}
		catch (Exception ex)
		{
			_logger.LogError("An error occurs while fetching rooms. Error message: {message}", ex.Message);
			return StatusCode((int)HttpStatusCode.InternalServerError);
		}
	}

	[HttpGet("rooms/{id}")]
	public async Task<IActionResult> GetRoom(int id)
	{
		try
		{
			var room = await _context.Rooms.SingleOrDefaultAsync(room => room.IdNumber == id);
			if (room is null) return NotFound();
			return Ok(room);
		}
		catch (Exception ex)
		{
			_logger.LogError("An error occurs while fetching room with id {id}. Error message: {message}", id, ex.Message);
			return StatusCode((int)HttpStatusCode.InternalServerError);
		}
	}

	[HttpPost("rooms")]
	public async Task<ActionResult<DreamStay.Domain.Models.Room>> CreateRoom(Models.Rooms.Room room)
	{
		try
		{
			bool isRequestInvalid =
				string.IsNullOrWhiteSpace(room.Photo) ||
				room.PricePerDay is 0m or decimal.Zero ||
				string.IsNullOrWhiteSpace(room.RoomType);

			if (isRequestInvalid)
			{
				return BadRequest();
			}

			var validationResult = _roomValidator.Validate(room);
			if (!validationResult.IsValid) return UnprocessableEntity();

			var domainRoom = new DreamStay.Domain.Models.Room
			{
				IdNumber = room.IdNumber,
				Photo = room.Photo,
				RoomType = room.RoomType,
				PricePerDay = room.PricePerDay,
				Bookings = room.Bookings
			};
			_context.Rooms.Add(domainRoom);
			await _context.SaveChangesAsync();

			return Created();
		}
		catch (Exception ex)
		{
			_logger.LogError("An error occurs while creating the room. Error message: {message}", ex.Message);
			return StatusCode((int)HttpStatusCode.InternalServerError);
		}
	}

	[HttpPatch("rooms/{id}")]
	public async Task<IActionResult> PatchRoom(int id, [FromBody] UpdateRoom model)
	{
		try 
		{
			var domainRoom = await _context.Rooms.FindAsync(id);
			if (domainRoom is null) return NotFound();

			var validationResult = _updateRoomValidator.Validate(model);
			var isInvalidRequest = !validationResult.IsValid ||
				model.IdNumber.HasValue ||
				model.PricePerDay.HasValue && model.PricePerDay <= decimal.Zero ||
				model.Bookings is not null;

			if (isInvalidRequest)
			{
				return BadRequest();
			}

			var roomEntry = new DreamStay.Domain.Models.Room
			{
				IdNumber = domainRoom.IdNumber,
				Photo = model.Photo ?? domainRoom.Photo,
				RoomType = model.RoomType ?? domainRoom.RoomType,
				PricePerDay = model.PricePerDay ?? domainRoom.PricePerDay,
				Bookings = domainRoom.Bookings
			};
			_context.Entry(roomEntry).State = EntityState.Modified;
			await _context.SaveChangesAsync();

			return Ok();
		}
		catch (Exception ex)
		{
			_logger.LogError("An error occurs while updating the room. Error message: {message}", ex.Message);
			return StatusCode((int)HttpStatusCode.InternalServerError);
		}
	}

	[HttpPut("rooms/{id}")]
	public async Task<IActionResult> UpdateRoom(int id, Models.Rooms.Room room)
	{
		try
		{
			var roomEntry = await _context.Rooms.FindAsync(id);
			if (roomEntry is null) return NotFound();
			bool isRequestInvalid =
				string.IsNullOrWhiteSpace(room.Photo) ||
				room.PricePerDay is 0m or decimal.Zero ||
				string.IsNullOrWhiteSpace(room.RoomType);

			if (isRequestInvalid) return BadRequest();

			var domainRoom = new DreamStay.Domain.Models.Room
			{
				IdNumber = id,
				Photo = room.Photo,
				RoomType = room.RoomType,
				PricePerDay = room.PricePerDay,
				Bookings = room.Bookings
			};
			_context.Entry(domainRoom).State = EntityState.Modified;
			await _context.SaveChangesAsync();

			return Ok();
		}
		catch(Exception ex)
		{
			_logger.LogError("An error occurs while updating the room. Error message: {message}", ex.Message);
			return StatusCode((int)HttpStatusCode.InternalServerError);
		}
	}

	[HttpDelete("rooms/{id}")]
	public async Task<IActionResult> DeleteRoom(int id)
	{
		try
		{
			var room = await _context.Rooms.FindAsync(id);
			if (room is null) return NotFound();
			_context.Rooms.Remove(room);
			await _context.SaveChangesAsync();
			return Ok();
		}
		catch (Exception ex)
		{
			_logger.LogError("An error occurs while deleting room with id {id}. Error message: {message}", id, ex.Message);
			return StatusCode((int)HttpStatusCode.InternalServerError);
		}
	}
}
