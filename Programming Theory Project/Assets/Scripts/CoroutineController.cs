using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineController : MonoBehaviour
{
    // method input guide:
    // obj is the Gameobject, set in child scripts.
    // delay/delayTime deals with initial delay (for the forks, etc)
    // pause/pauseTime deals with pauses between movements
    // rotate/rotateAngle deals with rotating object between movements (not rotation animation like pickups)
    // patrolA/patrolB/moveTime deal with movement
    public IEnumerator MoveTimer(GameObject obj, bool delay, float delayTime, bool pause, float pauseTime, bool rotate, Vector3 rotateAngle, Vector3 patrolA, Vector3 patrolB, float moveTime)
    {

        if (delay == true)
        {
            yield return new WaitForSeconds(delayTime);
        }

        while (true)
        {
            if (rotate)
            {
                obj.transform.Rotate(rotateAngle);
            }
            yield return StartCoroutine(MoveObject(obj.transform, patrolA, patrolB, moveTime));
            if (pause)
            {
                yield return new WaitForSeconds(pauseTime);
            }
            if (rotate)
            {
                obj.transform.Rotate(rotateAngle);
            }
            yield return StartCoroutine(MoveObject(obj.transform, patrolB, patrolA, moveTime));
            if (pause)
            {
                yield return new WaitForSeconds(pauseTime);
            }
        }
    }


    protected IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
    {
        var i = 0.0f;
        var rate = 1.0f / time;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            thisTransform.position = Vector3.Lerp(startPos, endPos, i);
            yield return null;
        }
    }
}
