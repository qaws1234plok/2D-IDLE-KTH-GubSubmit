using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Task/Target/String", fileName = "StringTarget")] 
public class StringTarget : TaskTarget
{
    [SerializeField] private string value;

    public override object Value => value;

    public override bool IsEqual(object target)
    {
        string targetString = target as string;
        if (targetString == null)
        {
            return false;
        }
        return value == targetString;
    }
}
