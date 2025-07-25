using NUnit.Framework;

// Enable parallel execution at assembly level
[assembly: Parallelizable(ParallelScope.Fixtures)]

// Limit to 5 concurrent test workers
[assembly: LevelOfParallelism(5)]