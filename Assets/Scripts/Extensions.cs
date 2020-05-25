using UnityEngine;

public static class Extensions
{
    public static PieceBehavior GetPieceBehavior(this GameObject gameObject)
    {
        return gameObject?.GetComponent("PieceBehavior") as PieceBehavior;
    }

    public static TargetPointBehavior GetTargetPointBehavior(this GameObject gameObject)
    {
        return gameObject?.GetComponent("TargetPointBehavior") as TargetPointBehavior;
    }
}
