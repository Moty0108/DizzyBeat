using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundAreaSC : ScriptableObject
{
    public List<Vector2> vectors;

    public void CreateVector(List<Vector2> _vectors)
    {
        vectors = _vectors;
    }

    public List<Vector2> GetVector()
    {
        return vectors;
    }
}
