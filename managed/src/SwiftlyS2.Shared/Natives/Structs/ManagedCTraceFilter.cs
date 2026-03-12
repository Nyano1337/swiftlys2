namespace SwiftlyS2.Shared.Natives;

public class ManagedCTraceFilter : IDisposable
{
    private CTraceFilter _filter;
    private bool _disposed;

    public bool IterateEntities {
        set { _filter.IterateEntities = value; }
    }

    public RnQueryShapeAttr_t QueryShapeAttributes {
        get { return _filter.QueryShapeAttributes; }
        set { _filter.QueryShapeAttributes = value; }
    }

    public unsafe delegate* unmanaged< CTraceFilter*, nint, byte > ShouldHitEntity {
        set { _filter.ShouldHitEntity = value; }
    }

    public ManagedCTraceFilter()
    {
        _filter = new CTraceFilter();
    }

    public ManagedCTraceFilter( bool checkIgnoredEntities )
    {
        _filter = new CTraceFilter(checkIgnoredEntities);
    }

    ~ManagedCTraceFilter()
    {
        Dispose(false);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose( bool disposing )
    {
        if (_disposed) return;

        _filter.Dispose();

        _disposed = true;
    }

    private void ThrowIfDisposed()
    {
        if (_disposed) throw new ObjectDisposedException(nameof(ManagedCTraceFilter));
    }

    public ref CTraceFilter Value { get { ThrowIfDisposed(); return ref _filter; } }
}