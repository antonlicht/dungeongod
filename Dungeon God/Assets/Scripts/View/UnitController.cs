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
            _activeUnit = hit.collider.GetComponent<Unit>();
            _activeUnit.Dragging = true;
            _offset = (Vector2)_activeUnit.transform.position - hit.point;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {        
        if (_activeUnit)
        {
            int layerMask = LayerMask.GetMask("BackgroundCollider");
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
        if (_activeUnit)
        {
            _activeUnit.Dragging = false;
            _activeUnit = null;
        }      
    }
}
