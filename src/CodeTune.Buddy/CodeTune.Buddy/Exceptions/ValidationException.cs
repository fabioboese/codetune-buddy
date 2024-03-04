using CodeTune.Buddy.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeTune.Buddy.Exceptions;
public class ValidationException<T> : Exception, IValidationException where T : IEntityModel
{
    public IRule[] ViolatedRules { get; }

    public ValidationException(IRule[] brokenRules) :
        base(String.Join(Environment.NewLine, brokenRules.Select(x => x.Name)))
    {
        ViolatedRules = brokenRules;
    }

}
