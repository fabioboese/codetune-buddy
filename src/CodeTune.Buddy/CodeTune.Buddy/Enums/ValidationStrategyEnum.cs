using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeTune.Buddy.Enums;
public enum ValidationStrategyEnum
{
    Unanimous = 1,      // all rules must evaluate to a TRUE for the final result to be also positive.
    Affirmative = 2,    // at least one reule must evaluate to TRUE for the final result to be also positive.
    Consensus = 3       // the number of TRUEs must be greater than the number of FALSEs. If they are equal the final result will be negative.
}
