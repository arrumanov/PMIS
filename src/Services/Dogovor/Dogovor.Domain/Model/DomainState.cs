namespace Dogovor.Domain.Model
{
    public enum DomainState
    {
        NEW = 1,
        FROM_DB,
        ADD_RELATION,
        REMOVE_RELATION
    }
}