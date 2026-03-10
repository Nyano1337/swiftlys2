#pragma warning disable CS0649
#pragma warning disable CS0169

using System.Buffers;
using System.Text;
using System.Threading;
using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Core.Natives;

internal static class NativeCore
{

    private unsafe static delegate* unmanaged<byte> _PluginManualLoadState;

    public unsafe static bool PluginManualLoadState()
    {
        var ret = _PluginManualLoadState();
        return ret == 1;
    }

    private unsafe static delegate* unmanaged<byte*, int> _PluginLoadOrder;

    public unsafe static string PluginLoadOrder()
    {
        var ret = _PluginLoadOrder(null);
        return StringAlloc.CreateCSharpString(ret, retBufferPtr =>
        {
            _ = _PluginLoadOrder((byte*)retBufferPtr);
        });
    }

    private unsafe static delegate* unmanaged<byte> _EnableProfilerByDefault;

    public unsafe static bool EnableProfilerByDefault()
    {
        var ret = _EnableProfilerByDefault();
        return ret == 1;
    }

    private unsafe static delegate* unmanaged<byte> _IsMainThread;

    public unsafe static bool IsMainThread()
    {
        var ret = _IsMainThread();
        return ret == 1;
    }
}