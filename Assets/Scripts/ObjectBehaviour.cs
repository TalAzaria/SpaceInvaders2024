using UnityEngine;

public static class ObjectBehaviour
{
    public static void MoveGameObject(SpriteRenderer obj, Vector2 vector)
    {
        obj.transform.position = new Vector2(obj.transform.position.x + vector.x, obj.transform.position.y + vector.y);
        ScreenManager.Instance.HorizontalLimitToScreen(obj);
    }
}
