using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerControls playerControls;
    [SerializeField] private float moveSpeed;
    private SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        ScreenManager.Instance.AddScreenBoundedObject(this.transform);
    }

    private void Update()
    {
        if (playerControls != null) 
        {
            if (Input.GetKey(playerControls.MoveRight))
            {
                ObjectBehaviour.MoveGameObject(sprite, Vector2.right * (moveSpeed * Time.deltaTime));
            }

            if (Input.GetKey(playerControls.MoveLeft))
            {
                ObjectBehaviour.MoveGameObject(sprite, Vector2.left * (moveSpeed * Time.deltaTime));
            }
        }
    }
}
