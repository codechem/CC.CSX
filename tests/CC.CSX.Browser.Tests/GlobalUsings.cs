global using Xunit;
global using CC.CSX;
global using CC.CSX.Browser;

// the event registry is process-global static state, so keep test classes sequential
[assembly: CollectionBehavior(DisableTestParallelization = true)]
