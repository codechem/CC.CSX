// These tests mutate process-global render state (RenderOptions.Indent, FragmentCache.Enabled),
// so they must not run concurrently across test classes.
[assembly: Xunit.CollectionBehavior(DisableTestParallelization = true)]
