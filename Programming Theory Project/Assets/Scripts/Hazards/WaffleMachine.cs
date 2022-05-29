using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Had to remember how to use animations for this one. Unity asset Waffle Iron.
//Hazard zone (similar to water) manually added to inside of waffle iron to handle player damage.
public class WaffleMachine : MonoBehaviour
{
    //public bool waffleOpen = false;
    Animator waffleAnim;
    [SerializeField] float pauseTime;

    private void Start()
    {
        waffleAnim = GetComponent<Animator>();
        waffleAnim.SetBool("waffleOpen", false);
        StartCoroutine(WaffleTime());
    }


    private IEnumerator WaffleTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(pauseTime);
            waffleAnim.SetBool("waffleOpen", true);
            yield return new WaitForSeconds(pauseTime);
            waffleAnim.SetBool("waffleOpen", false);
        }
    }
}
