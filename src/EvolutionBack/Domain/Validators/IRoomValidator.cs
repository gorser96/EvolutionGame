using Domain.Models;

namespace Domain.Validators;

public interface IRoomValidator
{
    public void CanUserStart(Room room, Guid userUid);
    
    public void CanUserPause(Room room, Guid userUid);

    public void CanUserResume(Room room, Guid userUid);

    public void CanUserUpdate(Room room, Guid userUid);

    public void CanUserLeave(Room room, Guid userUid);
    
    public void CanUserEnter(Room room, Guid userUid);
}
