using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CCSUsrMsg_VoteFailed : ITypedProtobuf<CCSUsrMsg_VoteFailed>
{
    static CCSUsrMsg_VoteFailed ITypedProtobuf<CCSUsrMsg_VoteFailed>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_VoteFailedImpl(handle, isManuallyAllocated);

    public int Team { get; set; }
    public int Reason { get; set; }
}