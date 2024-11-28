using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    public bool IsAlive { get; set; } = true;
    private SpriteRenderer spriteRenderer;
    public Bounds SpriteBounds { get { return spriteRenderer.bounds; } }

    private int spriteIndex = 0;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.sprite = sprites[spriteIndex];
    }

    public void ChangeSprite()
    {
        spriteIndex = ++spriteIndex % sprites.Length;
        spriteRenderer.sprite = sprites[spriteIndex];
    }
}
