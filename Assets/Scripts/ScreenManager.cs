using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager Instance { get; private set; }
    public Action<Vector2> OnScreenResolutionChanged;
    private Bounds screenWorldBounds;
    private Vector2 screedResolution;
    private List<Transform> screenBoundedObjects = new();

    private void Awake()
    {
        Instance = this;
        screedResolution.x = Screen.width;
        screedResolution.y = Screen.height;
        SetScreenWorldBounds();
    }

    private void Update()
    {
        if (Screen.width != screedResolution.x ||
            Screen.height != screedResolution.y)
        {
            MoveAllScreenBoundedObjects();
            screedResolution.x = Screen.width;
            screedResolution.y = Screen.height;
            SetScreenWorldBounds();
            OnScreenResolutionChanged?.Invoke(screedResolution);
        }
    }

    private void SetScreenWorldBounds()
    {
        screenWorldBounds.extents = new Vector2(
            Vector2.Distance(Camera.main.ViewportToWorldPoint(new Vector2(1, 0.5f)), Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 0.5f))),
            Vector2.Distance(Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 1)), Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 0.5f))));
        screenWorldBounds.center = Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 0.5f));
    }

    public void AddScreenBoundedObject(Transform obj)
    {
        screenBoundedObjects.Add(obj);
    }
    public void RemoveScreenBoundedObject(Transform obj)
    {
        screenBoundedObjects.Remove(obj);
    }

    private void MoveAllScreenBoundedObjects()
    {
        foreach (Transform obj in screenBoundedObjects)
        {
            Vector2 oldObjViewportPosition = new Vector2(
                (obj.transform.position.x - screenWorldBounds.min.x) / (screenWorldBounds.max.x - screenWorldBounds.min.x),
                (obj.transform.position.y - screenWorldBounds.min.y) / (screenWorldBounds.max.y - screenWorldBounds.min.y));

            obj.transform.position = Camera.main.ViewportToWorldPoint(oldObjViewportPosition);
            obj.transform.position = Vector3.Scale(obj.transform.position, new Vector3(1, 1, 0));
        }
    }

    public void HorizontalLimitToScreen(SpriteRenderer sprite)
    {
        if (sprite.bounds.min.x < screenWorldBounds.min.x)
            sprite.transform.position = new Vector2(screenWorldBounds.min.x + sprite.bounds.extents.x, sprite.transform.position.y);
        
        if (sprite.bounds.max.x > screenWorldBounds.max.x)
            sprite.transform.position = new Vector2(screenWorldBounds.max.x - sprite.bounds.extents.x, sprite.transform.position.y);
    }
}
