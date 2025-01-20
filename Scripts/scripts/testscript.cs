using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[ExecuteInEditMode]
public class testscript : MonoBehaviour
{
    public EnvironmentObject[] EnvironmentObjectList;

    private BoxCollider boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
    }
    void Update()
    {
        foreach (var obj in EnvironmentObjectList)
        {
            if (obj != null)
            {
                Vector3 position_face = new();
                Vector2 position_bind = new();
                Vector2 size_face = new();

                Vector3 size =  boxCollider.bounds.size * 0.5f;

                switch (obj.Bind)
                {
                    case EnvironmentObjectBind.Top:
                        position_bind = new(0, 1);
                        break;
                    case EnvironmentObjectBind.Down:
                        position_bind = new(0, -1);
                        break;
                    case EnvironmentObjectBind.Left:
                        position_bind = new(-1, 0);
                        break;
                    case EnvironmentObjectBind.Right:
                        position_bind = new(1, 0);
                        break;
                }
                Vector2 vb = new();
                switch (obj.Face)
                {
                    case EnvironmentObjectFace.Top:
                        position_face = new(0, size.y, 0);
                        size_face = new Vector2(size.x, size.z) * 2;
                        vb = new Vector2(size_face.x * position_bind.y, size_face.y * position_bind.x);
                        vb = vb * 0.5f - vb * obj.PositionBind;
                        position_face = position_face + new Vector3(size.x * position_bind.x + vb.x, 0, size.z * position_bind.y + vb.y);
                        break;
                    case EnvironmentObjectFace.Down:
                        position_face = new(0, -size.y, 0);
                        size_face = new Vector2(size.x, size.z) * 2;
                        vb = new Vector2(size_face.x * position_bind.y, size_face.y * position_bind.x);
                        vb = vb * 0.5f - vb * obj.PositionBind;
                        position_face = position_face + new Vector3(size.x * position_bind.x + vb.x, 0, size.z * position_bind.y + vb.y);
                        break;
                    case EnvironmentObjectFace.Left:
                        position_face = new(-size.x, 0, 0);
                        size_face = new Vector2(size.y, size.z) * 2;
                        vb = new Vector2(size_face.x * position_bind.x, size_face.y * position_bind.y);
                        vb = vb * 0.5f - vb * obj.PositionBind;
                        position_face = position_face + new Vector3(0, size.y * position_bind.y + vb.x, -size.z * position_bind.x + vb.y);
                        break;
                    case EnvironmentObjectFace.Right:
                        position_face = new(size.x, 0, 0);
                        size_face = new Vector2(size.y, size.z) * 2;
                        vb = new Vector2(size_face.x * position_bind.x, size_face.y * position_bind.y);
                        vb = vb * 0.5f - vb * obj.PositionBind;
                        position_face = position_face + new Vector3(0, size.y * position_bind.y + vb.x, size.z * position_bind.x + vb.y);
                        break;
                    case EnvironmentObjectFace.Forward:
                        position_face = new(0, 0, size.z);
                        size_face = new Vector2(size.x, size.y) * 2;
                        vb = new Vector2(size_face.x * position_bind.y, size_face.y * position_bind.x);
                        vb = vb * 0.5f - vb * obj.PositionBind;
                        position_face = position_face + new Vector3(-size.x * position_bind.x + vb.x, size.y * position_bind.y + vb.y, 0);
                        break;
                    case EnvironmentObjectFace.Back:
                        position_face = new(0, 0, -size.z);
                        size_face = new Vector2(size.x, size.y) * 2;
                        vb = new Vector2(size_face.x * position_bind.y, size_face.y * position_bind.x);
                        vb = vb * 0.5f - vb * obj.PositionBind;
                        position_face = position_face + new Vector3(size.x * position_bind.x + vb.x, size.y * position_bind.y + vb.y, 0);
                        break;
                }

                obj.transform.position = boxCollider.bounds.center + position_face + obj.OffsetBind;
            }
        }
    }
}
