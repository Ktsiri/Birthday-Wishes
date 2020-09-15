using System;
using System.Collections.Generic;
using System.Text;
using IOCO.BirthdayWishes.DomainObjects.Base;
using IOCO.BirthdayWishes.Dto.Enumerations;

namespace IOCO.BirthdayWishes.DomainObjects
{
    public class MessageQueue : BaseDomainObject<Guid>
    {
        public string SystemUniqueId { get; set; }
        public string SourceRawJson { get; set; }
        public bool IsBusyProcessing { get; set; }
        public int RetryCount { get; set; }
        public MessageStatusEnum MessageStatus { get; set; }
        public MessageTypeEnum MessageType { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
