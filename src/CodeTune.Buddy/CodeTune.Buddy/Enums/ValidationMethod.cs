using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeTune.Buddy.Enums;
public enum ValidationMethod
{
    SkipEarly = 1,          // stops the evaluation of the rules when the results already observed are enough to determine the final decision
    EvaluateAll = 2         // execute all the rules, no matter the evaluated results, and calculate the final decision after all
}
