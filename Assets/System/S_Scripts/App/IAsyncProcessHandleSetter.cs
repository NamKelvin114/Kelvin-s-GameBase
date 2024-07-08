using System;

namespace Kelvin
{
    internal interface IAsyncProcessHandleSetter
    {
        void Complete(object result);

        void Error(Exception ex);
    }
}