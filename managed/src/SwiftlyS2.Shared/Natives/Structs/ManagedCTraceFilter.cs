using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.Natives;

public class ManagedCTraceFilter : IDisposable
{
    private CTraceFilterCustom _filter;
    private bool _disposed;

    public bool IterateEntities {
        set { _filter.Data.IterateEntities = value; }
    }

    public RnQueryShapeAttr_t QueryShapeAttributes {
        get { return _filter.Data.QueryShapeAttributes; }
        set { _filter.Data.QueryShapeAttributes = value; }
    }

    public Func<CEntityInstance, bool> ShouldHitEntity {
        set { _filter.Callback = value; }
    }

    public ManagedCTraceFilter()
    {
        _filter = new CTraceFilterCustom();
    }

    public ManagedCTraceFilter( bool checkIgnoredEntities )
    {
        _filter = new CTraceFilterCustom(checkIgnoredEntities);
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

        _filter.Data.Dispose();

        _disposed = true;
    }

    private void ThrowIfDisposed()
    {
        if (_disposed) throw new ObjectDisposedException(nameof(ManagedCTraceFilter));
    }

    public ref CTraceFilter Value { get { ThrowIfDisposed(); return ref _filter.Data; } }
}