#pragma warning disable CS0649
#pragma warning disable CS0169

using System.Buffers;
using System.Text;
using System.Threading;
using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Core.Natives;

internal static class NativeEngineHelpers
{

    private unsafe static delegate* unmanaged<byte*, int> _GetIP;

    public unsafe static string GetIP()
    {
        var ret = _GetIP(null);
        return StringAlloc.CreateCSharpString(ret, retBufferPtr =>
        {
            _ = _GetIP((byte*)retBufferPtr);
        });
    }

    private unsafe static delegate* unmanaged<byte*, byte> _IsMapValid;

    /// <summary>
    /// it can be map name, or workshop id
    /// </summary>
    public unsafe static bool IsMapValid(string map_name)
    {
        return StringAlloc.CreateCString(map_name, map_nameBufferPtr =>
        {
            var ret = _IsMapValid((byte*)map_nameBufferPtr);
            return ret == 1;
        });
    }

    private unsafe static delegate* unmanaged<byte*, void> _ExecuteCommand;

    public unsafe static void ExecuteCommand(string command)
    {
        if (!NativeBinding.IsMainThread)
        {
            throw new InvalidOperationException("This method can only be called from the main thread.");
        }
        StringAlloc.CreateCString(command, commandBufferPtr =>
        {
            _ExecuteCommand((byte*)commandBufferPtr);
        });
    }

    private unsafe static delegate* unmanaged<byte*, nint> _FindGameSystemByName;

    public unsafe static nint FindGameSystemByName(string name)
    {
        return StringAlloc.CreateCString(name, nameBufferPtr =>
        {
            var ret = _FindGameSystemByName((byte*)nameBufferPtr);
            return ret;
        });
    }

    private unsafe static delegate* unmanaged<byte*, void> _SendMessageToConsole;

    public unsafe static void SendMessageToConsole(string msg)
    {
        StringAlloc.CreateCString(msg, msgBufferPtr =>
        {
            _SendMessageToConsole((byte*)msgBufferPtr);
        });
    }

    private unsafe static delegate* unmanaged<nint> _GetTraceManager;

    public unsafe static nint GetTraceManager()
    {
        var ret = _GetTraceManager();
        return ret;
    }

    private unsafe static delegate* unmanaged<byte*, int> _GetCurrentGame;

    public unsafe static string GetCurrentGame()
    {
        var ret = _GetCurrentGame(null);
        return StringAlloc.CreateCSharpString(ret, retBufferPtr =>
        {
            _ = _GetCurrentGame((byte*)retBufferPtr);
        });
    }

    private unsafe static delegate* unmanaged<byte*, int> _GetNativeVersion;

    public unsafe static string GetNativeVersion()
    {
        var ret = _GetNativeVersion(null);
        return StringAlloc.CreateCSharpString(ret, retBufferPtr =>
        {
            _ = _GetNativeVersion((byte*)retBufferPtr);
        });
    }

    private unsafe static delegate* unmanaged<byte*, int> _GetMenuSettings;

    public unsafe static string GetMenuSettings()
    {
        var ret = _GetMenuSettings(null);
        return StringAlloc.CreateCSharpString(ret, retBufferPtr =>
        {
            _ = _GetMenuSettings((byte*)retBufferPtr);
        });
    }

    private unsafe static delegate* unmanaged<nint> _GetGlobalVars;

    public unsafe static nint GetGlobalVars()
    {
        var ret = _GetGlobalVars();
        return ret;
    }

    private unsafe static delegate* unmanaged<nint> _GetNetworkGameServer;

    public unsafe static nint GetNetworkGameServer()
    {
        var ret = _GetNetworkGameServer();
        return ret;
    }

    private unsafe static delegate* unmanaged<byte*, int> _GetCSGODirectoryPath;

    public unsafe static string GetCSGODirectoryPath()
    {
        var ret = _GetCSGODirectoryPath(null);
        return StringAlloc.CreateCSharpString(ret, retBufferPtr =>
        {
            _ = _GetCSGODirectoryPath((byte*)retBufferPtr);
        });
    }

    private unsafe static delegate* unmanaged<byte*, int> _GetGameDirectoryPath;

    public unsafe static string GetGameDirectoryPath()
    {
        var ret = _GetGameDirectoryPath(null);
        return StringAlloc.CreateCSharpString(ret, retBufferPtr =>
        {
            _ = _GetGameDirectoryPath((byte*)retBufferPtr);
        });
    }

    private unsafe static delegate* unmanaged<byte*, int> _GetWorkshopId;

    public unsafe static string GetWorkshopId()
    {
        var ret = _GetWorkshopId(null);
        return StringAlloc.CreateCSharpString(ret, retBufferPtr =>
        {
            _ = _GetWorkshopId((byte*)retBufferPtr);
        });
    }

    private unsafe static delegate* unmanaged<byte*, uint, nint, byte, nint, byte, int, ulong, void> _DispatchParticleEffect;

    public unsafe static void DispatchParticleEffect(string particleName, uint attachmentType, nint entity, byte attachmentPoint, nint attachmentName, bool resetAllParticlesOnEntity, int splitScreenSlot, ulong filtermask)
    {
        StringAlloc.CreateCString(particleName, particleNameBufferPtr =>
        {
            _DispatchParticleEffect((byte*)particleNameBufferPtr, attachmentType, entity, attachmentPoint, attachmentName, resetAllParticlesOnEntity ? (byte)1 : (byte)0, splitScreenSlot, filtermask);
        });
    }
}