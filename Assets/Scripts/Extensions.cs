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

    public static ResultBehavior GetResultBehavior(this GameObject gameObject)
    {
        return gameObject?.GetComponent("ResultBehavior") as ResultBehavior;
    }
}
