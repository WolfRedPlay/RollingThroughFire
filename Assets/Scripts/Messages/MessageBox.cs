using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class MessageBox : MonoBehaviour
{
    [Tooltip("Text object for message content")]
    [SerializeField] TMP_Text _textField;

    RectTransform _viewportTransform;
    RectTransform _transform;

    Message _messageData;
    bool _isActive;

    private void Start()
    {
        _transform = GetComponent<RectTransform>();
    }

    public void Initialize(Message messageData, RectTransform viewport)
    {
        _messageData = messageData;
        _textField.text = _messageData.Text;
        _viewportTransform = viewport;
        _isActive = false;
        StartCoroutine(Activate());
    }

    IEnumerator Activate()
    {
        yield return new WaitForSeconds(1f);
        _isActive = true;
    }

    public void CheckIfMessageRead()
    {
        if (!_messageData.IsRead)
        {
            Vector3[] viewPortCorners = new Vector3[4];
            _viewportTransform.GetLocalCorners(viewPortCorners);

            Rect viewportRect = CreateRectFromPoints(viewPortCorners);

            Vector3[] corners = new Vector3[4];
            _transform.GetLocalCorners(corners);

            List<Vector3> cornersToCheck = FindCornersToCheck(corners);

            foreach (Vector3 corner in cornersToCheck)
            {
                Vector3 viewportLocalPoint = _viewportTransform.InverseTransformPoint(corner);

                if (viewportRect.Contains(viewportLocalPoint))
                {
                    _messageData.IsRead = true;
                    Debug.Log("Message read");
                    break;
                }
            }
        }
        
    }

    List<Vector3> FindCornersToCheck(Vector3[] corners)
    {

        float minY = Mathf.Min(corners[0].y, corners[1].y, corners[2].y, corners[3].y);

        List<Vector3> bottomPoints = new List<Vector3>();

        foreach (var corner in corners)
        {

            if (Mathf.Approximately(minY, corner.y)) bottomPoints.Add(corner);
        }

        Vector3[] bottomCorners = bottomPoints.ToArray();

        _transform.TransformPoints(bottomCorners);

        return bottomCorners.ToList();
    }



    Rect CreateRectFromPoints(Vector3[] points)
    {
        float minX = Mathf.Min(points[0].x, points[1].x, points[2].x, points[3].x);
        float maxX = Mathf.Max(points[0].x, points[1].x, points[2].x, points[3].x);
        float minY = Mathf.Min(points[0].y, points[1].y, points[2].y, points[3].y);
        float maxY = Mathf.Max(points[0].y, points[1].y, points[2].y, points[3].y);

        return new Rect(minX, minY, maxX - minX, maxY - minY);
    }


    private void OnDrawGizmos()
    {
        Vector3[] worldViewPortCorners = new Vector3[4];
        _viewportTransform.GetWorldCorners(worldViewPortCorners);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(worldViewPortCorners[0], worldViewPortCorners[1]);
        Gizmos.DrawLine(worldViewPortCorners[1], worldViewPortCorners[2]);
        Gizmos.DrawLine(worldViewPortCorners[2], worldViewPortCorners[3]);
        Gizmos.DrawLine(worldViewPortCorners[3], worldViewPortCorners[0]);



        Vector3[] viewPortCorners = new Vector3[4];
        _viewportTransform.GetLocalCorners(viewPortCorners);


        Rect viewportRect = CreateRectFromPoints(viewPortCorners);

        Vector3[] corners = new Vector3[4];
        _transform.GetLocalCorners(corners);

        List<Vector3> cornersToCheck = FindCornersToCheck(corners);



        bool checker = false;
        foreach (Vector3 corner in cornersToCheck)
        {
            Vector3 viewportLocalPoint = _viewportTransform.InverseTransformPoint(corner);


            checker = false;
            if (viewportRect.Contains(viewportLocalPoint))
            {
                checker = true;
            }
        }
        if (!checker) Gizmos.color = Color.red;
        else Gizmos.color = Color.green;
        Gizmos.DrawLine(cornersToCheck[0], cornersToCheck[1]);


    }
}
