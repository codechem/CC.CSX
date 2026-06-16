namespace CC.CSX.Browser.Tests;

public class EventRegistryTests
{
    [Fact]
    public void RegisteredDelegateResolvesById()
    {
        Action handler = () => { };
        var id = EventRegistry.Register("click", handler);
        Assert.Same(handler, EventRegistry.Resolve(id.ToString()));
    }

    [Fact]
    public void CycleSwapInvalidatesPreviousIds()
    {
        Action stale = () => { };
        var staleId = EventRegistry.Register("click", stale);

        EventRegistry.BeginCycle();
        Action fresh = () => { };
        var freshId = EventRegistry.Register("click", fresh);
        // not live until the cycle is committed
        Assert.Null(EventRegistry.Resolve(freshId.ToString()));
        EventRegistry.CommitCycle();

        Assert.Same(fresh, EventRegistry.Resolve(freshId.ToString()));
        Assert.Null(EventRegistry.Resolve(staleId.ToString()));
    }

    [Fact]
    public void NamedActionsSurviveCycles()
    {
        Action handler = () => { };
        EventRegistry.MapAction("counter/reset", handler);
        EventRegistry.BeginCycle();
        EventRegistry.CommitCycle();
        Assert.Same(handler, EventRegistry.Resolve("counter/reset"));
    }

    [Fact]
    public void UnknownKeysResolveToNull()
    {
        Assert.Null(EventRegistry.Resolve("no/such/action"));
        Assert.Null(EventRegistry.Resolve(int.MaxValue.ToString()));
    }

    [Fact]
    public void PendingEventsAreDeduplicated()
    {
        EventRegistry.DrainPendingEvents(); // flush events queued by other tests
        var name = $"evt-{Guid.NewGuid():N}";
        EventRegistry.RequireEvent(name);
        EventRegistry.RequireEvent(name);
        var drained = EventRegistry.DrainPendingEvents();
        Assert.Single(drained, name);
        Assert.Empty(EventRegistry.DrainPendingEvents());
    }
}
