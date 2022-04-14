using Domain.Models;
using Domain.Validators;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Validators;

public class RoomValidator : IRoomValidator
{
    private static InGameUser GetUserWithCheck(Room room, Guid userUid)
    {
        var user = room.FindUserByUid(userUid);
        if (user is null)
        {
            throw new ValidationException("User not found in room!");
        }
        return user;
    }

    private static void CheckIsHost(InGameUser user)
    {
        if (!user.IsHost)
        {
            throw new ValidationException("User is not host!");
        }
    }

    public void CanUserEnter(Room room, Guid userUid)
    {
        if (room.FindUserByUid(userUid) is not null)
        {
            throw new ValidationException("User already in room!");
        }
    }

    public void CanUserLeave(Room room, Guid userUid)
    {
        if (room.FindUserByUid(userUid) is null)
        {
            throw new ValidationException("User not found in room!");
        }
    }

    public void CanUserPause(Room room, Guid userUid)
    {
        var user = GetUserWithCheck(room, userUid);
        CheckIsHost(user);
    }

    public void CanUserResume(Room room, Guid userUid)
    {
        var user = GetUserWithCheck(room, userUid);
        CheckIsHost(user);
    }

    public void CanUserStart(Room room, Guid userUid)
    {
        var user = GetUserWithCheck(room, userUid);
        CheckIsHost(user);
    }

    public void CanUserUpdate(Room room, Guid userUid)
    {
        var user = GetUserWithCheck(room, userUid);
        CheckIsHost(user);
    }
}
