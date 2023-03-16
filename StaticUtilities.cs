using System.Collections;
using System.Linq;
using UnityEngine;

public static class Finder
{
    public static GameObject FindInChildren(this GameObject go, string name)
    {
        return (from x in go.GetComponentsInChildren<Transform>()
                where x.gameObject.name == name
                select x.gameObject).First();
    }
}

public static class CoroutineUtils
{
    public static IEnumerator WaitAndDo(float time, System.Action action, bool unscaledTime = false)
    {
        if (unscaledTime)
        {
            yield return new WaitForSecondsRealtime(time);
        }
        else
        {
            yield return new WaitForSeconds(time);
        }
        action();
    }
}
