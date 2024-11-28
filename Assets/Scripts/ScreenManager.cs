using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager Instance { get; private set; }
    public Action<Vector2> OnScreenResolutionChanged;
    public Bounds ScreenWorldBounds;
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
        ScreenWorldBounds.extents = new Vector2(
            Vector2.Distance(Camera.main.ViewportToWorldPoint(new Vector2(1, 0.5f)), Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 0.5f))),
            Vector2.Distance(Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 1)), Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 0.5f))));
        ScreenWorldBounds.center = Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 0.5f));
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
                (obj.transform.position.x - ScreenWorldBounds.min.x) / (ScreenWorldBounds.max.x - ScreenWorldBounds.min.x),
                (obj.transform.position.y - ScreenWorldBounds.min.y) / (ScreenWorldBounds.max.y - ScreenWorldBounds.min.y));

            obj.transform.position = Camera.main.ViewportToWorldPoint(oldObjViewportPosition);
            obj.transform.position = Vector3.Scale(obj.transform.position, new Vector3(1, 1, 0));
        }
    }

    public void HorizontalLimitToScreen(SpriteRenderer sprite)
    {
        if (sprite.bounds.min.x < ScreenWorldBounds.min.x)
            sprite.transform.position = new Vector2(ScreenWorldBounds.min.x + sprite.bounds.extents.x, sprite.transform.position.y);
        
        if (sprite.bounds.max.x > ScreenWorldBounds.max.x)
            sprite.transform.position = new Vector2(ScreenWorldBounds.max.x - sprite.bounds.extents.x, sprite.transform.position.y);
    }
}
