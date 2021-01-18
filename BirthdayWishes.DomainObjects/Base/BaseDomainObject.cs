using System;
using System.Collections.Generic;
using System.Text;

namespace BirthdayWishes.DomainObjects.Base
{
    public abstract class BaseDomainObject<TIdentity> : DomainObject
    {
        public TIdentity Id { get; set; }
    }
    public abstract class DomainObject { }
}
