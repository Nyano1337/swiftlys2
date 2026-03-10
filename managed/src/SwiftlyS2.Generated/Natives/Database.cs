#pragma warning disable CS0649
#pragma warning disable CS0169

using System.Buffers;
using System.Text;
using System.Threading;
using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Core.Natives;

internal static class NativeDatabase
{

    private unsafe static delegate* unmanaged<byte*, int> _GetDefaultDriver;

    public unsafe static string GetDefaultDriver()
    {
        var ret = _GetDefaultDriver(null);
        return StringAlloc.CreateCSharpString(ret, retBufferPtr =>
        {
            _ = _GetDefaultDriver((byte*)retBufferPtr);
        });
    }

    private unsafe static delegate* unmanaged<byte*, int> _GetDefaultConnectionName;

    public unsafe static string GetDefaultConnectionName()
    {
        var ret = _GetDefaultConnectionName(null);
        return StringAlloc.CreateCSharpString(ret, retBufferPtr =>
        {
            _ = _GetDefaultConnectionName((byte*)retBufferPtr);
        });
    }

    private unsafe static delegate* unmanaged<byte*, byte*, int> _GetConnectionDriver;

    public unsafe static string GetConnectionDriver(string connectionName)
    {
        return StringAlloc.CreateCString(connectionName, connectionNameBufferPtr =>
        {
            var ret = _GetConnectionDriver(null, (byte*)connectionNameBufferPtr);
            return StringAlloc.CreateCSharpString(ret, retBufferPtr =>
            {
                _ = _GetConnectionDriver((byte*)retBufferPtr, (byte*)connectionNameBufferPtr);
            });
        });
    }

    private unsafe static delegate* unmanaged<byte*, byte*, int> _GetConnectionHost;

    public unsafe static string GetConnectionHost(string connectionName)
    {
        return StringAlloc.CreateCString(connectionName, connectionNameBufferPtr =>
        {
            var ret = _GetConnectionHost(null, (byte*)connectionNameBufferPtr);
            return StringAlloc.CreateCSharpString(ret, retBufferPtr =>
            {
                _ = _GetConnectionHost((byte*)retBufferPtr, (byte*)connectionNameBufferPtr);
            });
        });
    }

    private unsafe static delegate* unmanaged<byte*, byte*, int> _GetConnectionDatabase;

    public unsafe static string GetConnectionDatabase(string connectionName)
    {
        return StringAlloc.CreateCString(connectionName, connectionNameBufferPtr =>
        {
            var ret = _GetConnectionDatabase(null, (byte*)connectionNameBufferPtr);
            return StringAlloc.CreateCSharpString(ret, retBufferPtr =>
            {
                _ = _GetConnectionDatabase((byte*)retBufferPtr, (byte*)connectionNameBufferPtr);
            });
        });
    }

    private unsafe static delegate* unmanaged<byte*, byte*, int> _GetConnectionUser;

    public unsafe static string GetConnectionUser(string connectionName)
    {
        return StringAlloc.CreateCString(connectionName, connectionNameBufferPtr =>
        {
            var ret = _GetConnectionUser(null, (byte*)connectionNameBufferPtr);
            return StringAlloc.CreateCSharpString(ret, retBufferPtr =>
            {
                _ = _GetConnectionUser((byte*)retBufferPtr, (byte*)connectionNameBufferPtr);
            });
        });
    }

    private unsafe static delegate* unmanaged<byte*, byte*, int> _GetConnectionPass;

    public unsafe static string GetConnectionPass(string connectionName)
    {
        return StringAlloc.CreateCString(connectionName, connectionNameBufferPtr =>
        {
            var ret = _GetConnectionPass(null, (byte*)connectionNameBufferPtr);
            return StringAlloc.CreateCSharpString(ret, retBufferPtr =>
            {
                _ = _GetConnectionPass((byte*)retBufferPtr, (byte*)connectionNameBufferPtr);
            });
        });
    }

    private unsafe static delegate* unmanaged<byte*, uint> _GetConnectionTimeout;

    public unsafe static uint GetConnectionTimeout(string connectionName)
    {
        return StringAlloc.CreateCString(connectionName, connectionNameBufferPtr =>
        {
            var ret = _GetConnectionTimeout((byte*)connectionNameBufferPtr);
            return ret;
        });
    }

    private unsafe static delegate* unmanaged<byte*, ushort> _GetConnectionPort;

    public unsafe static ushort GetConnectionPort(string connectionName)
    {
        return StringAlloc.CreateCString(connectionName, connectionNameBufferPtr =>
        {
            var ret = _GetConnectionPort((byte*)connectionNameBufferPtr);
            return ret;
        });
    }

    private unsafe static delegate* unmanaged<byte*, byte*, int> _GetConnectionRawUri;

    public unsafe static string GetConnectionRawUri(string connectionName)
    {
        return StringAlloc.CreateCString(connectionName, connectionNameBufferPtr =>
        {
            var ret = _GetConnectionRawUri(null, (byte*)connectionNameBufferPtr);
            return StringAlloc.CreateCSharpString(ret, retBufferPtr =>
            {
                _ = _GetConnectionRawUri((byte*)retBufferPtr, (byte*)connectionNameBufferPtr);
            });
        });
    }

    private unsafe static delegate* unmanaged<byte*, byte> _ConnectionExists;

    public unsafe static bool ConnectionExists(string connectionName)
    {
        return StringAlloc.CreateCString(connectionName, connectionNameBufferPtr =>
        {
            var ret = _ConnectionExists((byte*)connectionNameBufferPtr);
            return ret == 1;
        });
    }
}