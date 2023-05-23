namespace MissionPossible.Domain.Common
{
    public interface IIdentifiable<T> 
    {
         T Id { get; }
    }
}