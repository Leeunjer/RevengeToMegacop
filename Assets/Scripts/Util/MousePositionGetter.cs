
using UnityEngine;

public static class MousePositionGetter
{
    private static Camera mainCamera;

    public static Vector3 GetMousePositionInWorld(Vector3 target)
    {
        if (mainCamera == null) mainCamera = Camera.main;

        Plane groundPlane = new Plane(Vector3.up, new Vector3(0, target.y, 0));

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (groundPlane.Raycast(ray, out float enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);

            hitPoint.y = target.y;

            return hitPoint;
        }

        return Vector3.zero;
    }
}