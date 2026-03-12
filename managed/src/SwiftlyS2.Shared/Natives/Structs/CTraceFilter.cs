using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SwiftlyS2.Core.EntitySystem;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.Natives;

[StructLayout(LayoutKind.Explicit, Pack = 8, Size = 72)]
public unsafe struct CTraceFilter : IDisposable
{
    private static readonly bool IsWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

    [FieldOffset(0x0)]
    private CTraceFilterVTable* _pVTable;

    [FieldOffset(0x8)]
    public RnQueryShapeAttr_t QueryShapeAttributes;

    [FieldOffset(0x3A)]
    [MarshalAs(UnmanagedType.U1)]
    private bool _IterateEntities_Linux;

    [FieldOffset(0x40)]
    [MarshalAs(UnmanagedType.U1)]
    private bool _IterateEntities_Win;

    /// <summary>
    /// If true then calls ShouldHitEntity for each hit entity.
    /// <para>Default value: <see langword="true"/></para>
    /// </summary>
    public bool IterateEntities {
        readonly get => IsWindows ? _IterateEntities_Win : _IterateEntities_Linux;
        set {
            _IterateEntities_Linux = value;
            _IterateEntities_Win = value;
        }
    }

    public delegate* unmanaged< CTraceFilter*, nint, byte > ShouldHitEntity {
        set {
            if (IsWindows)
                _pVTable->pShouldHitEntity_Win = value;
            else
                _pVTable->pShouldHitEntity_Linux = value;
        }
    }

    /// <summary>
    /// Please use <see cref="ManagedCTraceFilter"/> instead to construct it.
    /// If you really want to use this, you should call <see cref="Dispose"/> after you are done with it.
    /// </summary>
    public CTraceFilter() : this(true) { }

    /// <summary>
    /// Please use <see cref="ManagedCTraceFilter"/> instead to construct it.
    /// If you really want to use this, you should call <see cref="Dispose"/> after you are done with it.
    /// </summary>
    public CTraceFilter( bool checkIgnoredEntities )
    {
        EnsureValid();
        ShouldHitEntity = checkIgnoredEntities ? &CTraceFilterVTable.ShouldHitEntity_CheckIgnoredEntities : &CTraceFilterVTable.ShouldHitEntity_Always;
        QueryShapeAttributes = new RnQueryShapeAttr_t();

        IterateEntities = true;
    }

    public void EnsureValid()
    {
        if (this._pVTable == null)
        {
            _pVTable = (CTraceFilterVTable*)NativeMemory.Alloc((nuint)sizeof(CTraceFilterVTable));

            if (IsWindows)
            {
                _pVTable->pDestructor_Win = &CTraceFilterVTable.Destructor;
            }
            else
            {
                _pVTable->pDestructor_Linux = &CTraceFilterVTable.Destructor;
                _pVTable->pDestructor2_Linux = &CTraceFilterVTable.Destructor;
            }
        }
    }

    public bool ShouldIgnoreEntity( CEntityInstance ent )
    {
        return ent == null || !ent.IsValid || QueryShapeAttributes.EntityIdsToIgnore[0] == ent.Index || QueryShapeAttributes.EntityIdsToIgnore[1] == ent.Index;
    }

    public void Dispose()
    {
        if (_pVTable != null)
        {
            NativeMemory.Free(_pVTable);
            _pVTable = null;
        }
    }
}

[StructLayout(LayoutKind.Explicit)]
internal struct CTraceFilterCustom
{
    [FieldOffset(0)]
    internal CTraceFilter Data;

    [FieldOffset(72)]
    internal bool CheckIgnoredEntities;

    [FieldOffset(80)]
    internal Func<CEntityInstance, bool>? Callback = null;

    public CTraceFilterCustom() : this(true) { }

    public CTraceFilterCustom( bool checkIgnoredEntities )
    {
        CheckIgnoredEntities = checkIgnoredEntities;
        Data = new CTraceFilter(checkIgnoredEntities);
        unsafe { Data.ShouldHitEntity = &CTraceFilterVTable.ShouldHitEntity_Custom; }
    }
}

[StructLayout(LayoutKind.Explicit)]
internal unsafe struct CTraceFilterVTable
{
    // Win
    [FieldOffset(0)]
    internal delegate* unmanaged< CTraceFilter*, void > pDestructor_Win;

    [FieldOffset(8)]
    internal delegate* unmanaged< CTraceFilter*, nint, byte > pShouldHitEntity_Win;

    // Linux
    [FieldOffset(0)]
    internal delegate* unmanaged< CTraceFilter*, void > pDestructor_Linux;

    [FieldOffset(8)]
    internal delegate* unmanaged< CTraceFilter*, void > pDestructor2_Linux;

    [FieldOffset(16)]
    internal delegate* unmanaged< CTraceFilter*, nint, byte > pShouldHitEntity_Linux;

    [UnmanagedCallersOnly]
    internal static void Destructor( CTraceFilter* filter )
    {
        // do nothing
    }

    [UnmanagedCallersOnly]
    internal static byte ShouldHitEntity_Always( CTraceFilter* filter, nint entity )
    {
        return 1;
    }

    [UnmanagedCallersOnly]
    internal static byte ShouldHitEntity_CheckIgnoredEntities( CTraceFilter* filter, nint entity )
    {
        var ent = EntityManager.GetEntityByAddress(entity) as CBaseEntity ?? Helper.AsSchema<CBaseEntity>(entity);

        return filter->ShouldIgnoreEntity(ent) ? (byte)0 : (byte)1;
    }

    [UnmanagedCallersOnly]
    internal static byte ShouldHitEntity_Custom( CTraceFilter* filter, nint entity )
    {
        var ent = EntityManager.GetEntityByAddress(entity) as CBaseEntity ?? Helper.AsSchema<CBaseEntity>(entity);
        var customFilter = Unsafe.AsRef<CTraceFilterCustom>(filter);
        var hit = !customFilter.CheckIgnoredEntities || !customFilter.Data.ShouldIgnoreEntity(ent);

        if (hit && customFilter.Callback != null)
        {
            hit = customFilter.Callback!.Invoke(ent);
        }

        return hit ? (byte)1 : (byte)0;
    }
}
