using System;
using System.Collections.Generic;
using System.Text;
using BirthdayWishes.Contract;
using BirthdayWishes.Contract.DependencyInjection;
using BirthdayWishes.SmtpClient.Settings;

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
