﻿using System;

namespace App.Common.Validation.Abstractions
{
    public interface IValidator<TEntity, TError>
        where TError : struct, Enum
    {
        ValidationResult<TError> Validate(TEntity entity);
    }
}