﻿namespace TestingInfrastructure.Creators;

public static class Create
{
    private static long _counter = DateTime.UtcNow.Ticks;
    public static readonly Random StaticRandom = new ();
    public static long RandomId() => Interlocked.Increment(ref _counter);
    public static double RandomDouble() => StaticRandom.NextDouble();
    public static decimal RandomDecimal() => (decimal)StaticRandom.NextDouble();
}