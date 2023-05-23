﻿using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace MissionPossible.Application.Common.Exceptions
{
    public class SystemValidationException : Exception
    {
        public SystemValidationException()
            : base("One or more validation failures have occurred.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public SystemValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }

        public IDictionary<string, string[]> Errors { get; }
    }
}