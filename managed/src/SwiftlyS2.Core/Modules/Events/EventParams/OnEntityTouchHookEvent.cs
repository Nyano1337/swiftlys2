using SwiftlyS2.Shared.Events;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.Events;

internal class OnEntityStartTouchEvent : IOnEntityStartTouchEvent
{
  public required CBaseEntity Entity { get; init; }

  public required CBaseEntity OtherEntity { get; init; }
}

internal class OnEntityTouchEvent : IOnEntityTouchEvent
{
  public required CBaseEntity Entity { get; init; }

  public required CBaseEntity OtherEntity { get; init; }
}

internal class OnEntityEndTouchEvent : IOnEntityEndTouchEvent
{
  public required CBaseEntity Entity { get; init; }

  public required CBaseEntity OtherEntity { get; init; }
}