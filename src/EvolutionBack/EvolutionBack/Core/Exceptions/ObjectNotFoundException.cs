namespace EvolutionBack.Core;

public class ObjectNotFoundException : Exception
{
    public ObjectNotFoundException()
    {
    }

    public ObjectNotFoundException(Guid uid, string objName) : base($"Object {objName} with uid=[{uid}] not found!")
    {
    }

    public ObjectNotFoundException(string? message) : base(message)
    {
    }
}
