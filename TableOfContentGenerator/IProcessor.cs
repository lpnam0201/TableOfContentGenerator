using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableOfContentGenerator
{
    public interface IProcessor
    {
        void Process(Options options);
    }
}
