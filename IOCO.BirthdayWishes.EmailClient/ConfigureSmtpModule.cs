using System;
using System.Collections.Generic;
using System.Text;
using IOCO.BirthdayWishes.Contract;
using IOCO.BirthdayWishes.Contract.DependencyInjection;
using IOCO.BirthdayWishes.SmtpClient.Settings;

namespace IOCO.BirthdayWishes.SmtpClient
{
    public class ConfigureSmtpModule : IConfigureDependency
    {
        private readonly IRegisterClass _registerClass;

        public ConfigureSmtpModule(IRegisterClass registerClass)
        {
            _registerClass = registerClass;
        }

        public void RegisterDependency()
        {
            _registerClass.ConfigureSettings<EmailSettings>("EmailSettings");
        }
    }
}
