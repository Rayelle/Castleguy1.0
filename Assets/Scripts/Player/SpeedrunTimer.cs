using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class SpeedrunTimer : ScriptableObject
{
    private float time=0;

    public float Time { get => time; set => time = value; }
}
