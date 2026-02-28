using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CCSUsrMsg_ItemDrop : ITypedProtobuf<CCSUsrMsg_ItemDrop>
{
    static CCSUsrMsg_ItemDrop ITypedProtobuf<CCSUsrMsg_ItemDrop>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_ItemDropImpl(handle, isManuallyAllocated);

    public long Itemid { get; set; }
    public bool Death { get; set; }
}