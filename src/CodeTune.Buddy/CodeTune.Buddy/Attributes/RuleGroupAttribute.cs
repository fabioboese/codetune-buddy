using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeTune.Buddy.Attributes;
public class RuleGroupAttribute : Attribute
{
    public RuleGroupAttribute(int group)
    {
        Group = group;
    }

    public int Group { get; }
}
