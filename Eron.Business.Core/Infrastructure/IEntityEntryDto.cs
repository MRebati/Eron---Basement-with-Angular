namespace Eron.Business.Core.Infrastructure
{
    public interface IEntityEntryDto: IEntityDto
    {
        bool IsUpdateEntry();
    }
}