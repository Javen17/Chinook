using System;
using System.Collections.Generic;
using System.Text;

namespace Chinook.BusinessLogic
{
    public interface IConfigurable
    {
        /// <summary>
        /// Gets a value indicating whether this <see cref="IConfigurable"/> is configured.
        /// </summary>
        /// <remarks></remarks>
        bool Configured { get; }

        /// <summary>
        /// Configures this instance.
        /// </summary>
        /// <remarks></remarks>
        void Configure();

    }
}
