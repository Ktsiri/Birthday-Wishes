using System;
using System.Collections.Generic;
using System.Text;

namespace BirthdayWishes.DomainObjects.Base
{
    public abstract class BaseActiveDomainObject<TIdentity> : BaseDomainObject<TIdentity>
    {
        public bool IsActive { get; set; }
    }
}
