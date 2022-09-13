using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XamarinBarCode_Test.Services
{
    public interface IMessageService
    {
        Task ShowAsync(string message, Action handler = null);
    }
}
