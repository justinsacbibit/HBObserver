using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observer.Commands
{
    public interface ICommand
    {
        string Name { get; }
        IList<string> Parameters { get; }

        void Execute();
    }
}
