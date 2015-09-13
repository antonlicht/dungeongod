using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class UnitController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Unit _activeUnit;
    private Vector3 _lastPos;
    public void OnBeginDrag(PointerEventData eventData)
    {
        var worldPos = Camera.main.ScreenToWorldPoint(eventData.position);
        var hits = Physics2D.CircleCastAll(worldPos, .5f, Vector2.zero,100);
        var hit = hits.FirstOrDefault(h => h.collider.GetComponent<Unit>() != null && h.collider.GetComponent<Unit>().Draggable);
        if(hit != default(RaycastHit2D))
        {
            _activeUnit = hit.collider.GetComponent<Unit>();
            _lastPos = worldPos;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_activeUnit)
        {
            var worldPos = Camera.main.ScreenToWorldPoint(eventData.position);
            _activeUnit.transform.Translate(worldPos - _lastPos);
            _lastPos = worldPos;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _activeUnit = null;
    }
}
