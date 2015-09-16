using UnityEngine;

public class Unit : MonoBehaviour
{
    public bool Draggable;

    private Rigidbody2D _rigidbody;
    private Vector2 _position;
    private bool _dragging;

    public bool Dragging
    {
        get
        {
            return _dragging;
        }
        set
        {
            if (Draggable)
            {
                _dragging = value;
                if (_dragging)
                {
                    Rigidbody.isKinematic = false;
                }
            }
        }
    }

    public Rigidbody2D Rigidbody
    {
        get
        {
            if (_rigidbody == null)
            {
                _rigidbody = GetComponent<Rigidbody2D>();
            }
            return _rigidbody;
        }
    }



    void Awake()
    {
        _position = transform.position;
    }

    void FixedUpdate()
    {
        var rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.MovePosition(_position);
        if (!Dragging)
        {
            _position = transform.position;
            rigidBody.MovePosition(_position);        
            Rigidbody.isKinematic = true;
        }
    }

    public void Move(Vector2 newPos)
    {
        if (Dragging)
        {
            _position = newPos;
        }
    }


}
