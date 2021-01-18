using System;
using System.Collections.Generic;
using System.Text;

namespace BirthdayWishes.DomainObjects.Base
{
    public abstract class BaseLookupDomainObject<TIdentity> : BaseActiveDomainObject<TIdentity>
    {
        public string Description { get; set; }
    }
}
