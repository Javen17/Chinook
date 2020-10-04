using System;
using System.Collections.Generic;
using System.Text;

namespace Chinook.BusinessLogic
{
    public interface IObjectFactory
    {
        T Resolve<T>() where T : class;
    }
}
