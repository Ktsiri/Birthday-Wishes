using IOCO.BirthdayWishes.Contract.Common;
using Microsoft.Extensions.Options;

namespace IOCO.BirthdayWishes.Common.Services
{
    public sealed class SettingsOptions<TSettings> : ISettings<TSettings>
        where TSettings : class, new()
    {
        public TSettings Value { get; }

        public SettingsOptions(IOptions<TSettings> options)
        {
            Value = options.Value;
        }
    }
}
