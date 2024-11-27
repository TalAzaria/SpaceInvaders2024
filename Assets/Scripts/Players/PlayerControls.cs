using UnityEngine;

[CreateAssetMenu(fileName = "PlayerControls", menuName = "Scriptable Objects/PlayerControls")]
public class PlayerControls : ScriptableObject
{
    public KeyCode MoveLeft;
    public KeyCode MoveRight;
    public KeyCode Shoot;
}
