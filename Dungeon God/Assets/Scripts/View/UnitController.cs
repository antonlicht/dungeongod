using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class UnitController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Unit _activeUnit;
    private Vector2 _offset;
    public void OnBeginDrag(PointerEventData eventData)
    {
        var ray = Camera.main.ScreenPointToRay(eventData.position);
        var hit = Physics2D.GetRayIntersectionAll(ray).FirstOrDefault(h => h.collider.GetComponent<Unit>() != null && h.collider.GetComponent<Unit>().Draggable);
        if(hit != default(RaycastHit2D))
        {
            Debug.Log(hit.collider.name);
            _activeUnit = hit.collider.GetComponent<Unit>();
            _activeUnit.GetComponent<Rigidbody2D>().isKinematic = false;
            _offset = (Vector2)_activeUnit.transform.position - hit.point;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        int layerMask = LayerMask.GetMask("BackgroundCollider");
        if (_activeUnit)
        {
            var ray = Camera.main.ScreenPointToRay(eventData.position);
            var hit = Physics2D.GetRayIntersection(ray, 1000f, layerMask);
            if (hit)
            {
                _activeUnit.Move(hit.point + _offset);
            }
            
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _activeUnit.GetComponent<Rigidbody2D>().isKinematic = true;
        _activeUnit = null;
    }
}
