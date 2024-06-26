﻿using System.Runtime.Serialization;

namespace Homework.PriceCalculator.Domain.Exceptions;

public class InfrastructureException :Exception
{
    public InfrastructureException()
    {
    }

    protected InfrastructureException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public InfrastructureException(string? message) : base(message)
    {
    }

    public InfrastructureException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}