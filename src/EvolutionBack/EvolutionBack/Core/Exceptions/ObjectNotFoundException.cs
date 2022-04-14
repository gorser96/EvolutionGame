namespace EvolutionBack.Core;

public class ObjectNotFoundException : Exception
{
    public ObjectNotFoundException(string? message) : base(message)
    {
    }

    public ObjectNotFoundException(Guid uid, string objName) : base($"Object {objName} with uid=[{uid}] not found!")
    {
    }
}
