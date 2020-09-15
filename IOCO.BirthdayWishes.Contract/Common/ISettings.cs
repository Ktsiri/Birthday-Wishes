namespace IOCO.BirthdayWishes.Contract.Common
{
    public interface ISettings<out TSettings> where TSettings : class, new()
    {
        TSettings Value { get; }
    }
}
