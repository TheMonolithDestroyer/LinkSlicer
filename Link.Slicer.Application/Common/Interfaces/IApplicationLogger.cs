using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Slicer.Application.Common.Interfaces
{
    public interface IApplicationLogger<T>
    {
        void LogInformation(string message, Exception inner);
        void LogWarning(string message, Exception inner);
        void LogError(string message, Exception inner);
    }
}
