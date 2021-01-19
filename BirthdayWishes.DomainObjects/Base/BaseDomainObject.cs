namespace BirthdayWishes.DomainObjects.Base
{
    public abstract class BaseDomainObject<TIdentity> : DomainObject
    {
        public TIdentity Id { get; set; }
    }
    public abstract class DomainObject { }
}
