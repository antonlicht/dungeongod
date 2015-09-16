using UnityEngine;

public class Unit : MonoBehaviour
{
    public bool Draggable;

    private Vector2 _position;

    void Awake()
    {
        _position = transform.position;
    }

    public void Move(Vector2 newPos)
    {
        _position = newPos;
    }

    void FixedUpdate()
    {
        var rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.MovePosition(_position);
    }
}
