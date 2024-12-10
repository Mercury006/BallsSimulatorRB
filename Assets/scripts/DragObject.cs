using UnityEngine;

public class Drag : MonoBehaviour
{
    private Vector3 mOffset;

    private float mZcoord;
    void OnMouseDown()
    {
        mZcoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        //Store offset = gameobject world pos - mouseworld pos
        mOffset = gameObject.transform.position - GetMouseWorldPos();
    }

    private Vector3 GetMouseWorldPos()
    {
        //pixelCoordinates (x,y)
        Vector3 mousePoint = Input.mousePosition;
        //returnMousePos z
        mousePoint.z = mZcoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
        //mousePoint.y = mOffset.y;
        //mousePoint.x = mOffset.x;
        //return mousePoint;
    }

    void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + mOffset;
    }
}
