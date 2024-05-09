using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HelpTool : MonoBehaviour
{
    public static GameObject FindNearestGameObject(string tag, GameObject obj)
    {
        var objects = GameObject.FindGameObjectsWithTag(tag);
        return objects.OrderBy(x => Vector3.Distance(x.transform.position, obj.transform.position)).First();
    }
    public static float FindDistance(GameObject first, GameObject second)
    {
        return Vector3.Distance(first.transform.position, second.transform.position);
    }
}
