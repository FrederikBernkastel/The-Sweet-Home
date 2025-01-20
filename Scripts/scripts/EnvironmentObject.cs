using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnvironmentObjectBind
{
    Top, Down, Left, Right
}
public enum EnvironmentObjectFace
{
    Top, Down, Left, Right, Forward, Back
}
public class EnvironmentObject : MonoBehaviour
{
    public EnvironmentObjectBind Bind;
    public EnvironmentObjectFace Face;
    public Vector3 OffsetBind;
    [Range(0, 1)] public float PositionBind;
}
