using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeTune.Buddy.Interfaces;
public interface IValidationException
{
    IRule[] ViolatedRules { get; }
}
