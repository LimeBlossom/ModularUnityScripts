using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogAnimationController : MonoBehaviour
{
    [System.Serializable]
    public struct DialogCharacter
    {
        public GameObject obj;
        public string name;
    }

    public enum Direction
    {
        LEFT,
        RIGHT
    }

    [SerializeField] private List<DialogCharacter> characters;
    [SerializeField] private GameObject studioLeft;
    [SerializeField] private GameObject studioRight;

    public GameObject SpawnCharacter(string name, Direction dir)
    {
        foreach(DialogCharacter character in characters)
        {
            if(name == character.name)
            {
                GameObject studio = studioRight;
                if(dir == Direction.LEFT)
                {
                    studio = studioLeft;
                }

                return Instantiate(character.obj, studio.transform, false);
            }
        }
        return null;
    }

    public void DestroyCharacter(Direction dir)
    {
        if(dir == Direction.LEFT)
        {
            if(studioLeft != null &&
                studioLeft.transform != null &&
                studioLeft.transform.childCount > 0 &&
                studioLeft.transform.GetChild(0) != null)
            {
                DestroyImmediate(studioLeft.transform.GetChild(0).gameObject);
            }
        }
        if (dir == Direction.RIGHT)
        {
            if (studioRight != null &&
                studioRight.transform != null &&
                studioLeft.transform.childCount > 0 &&
                studioRight.transform.GetChild(0) != null)
            {
                DestroyImmediate(studioRight.transform.GetChild(0).gameObject);
            }
        }
    }

    public void AnimateCharacter(string animation, Direction dir, bool crossFade = true)
    {
        if(dir == Direction.LEFT)
        {
            if(crossFade)
            {
                studioLeft.transform.GetChild(0).GetComponentInChildren<Animator>().
                    CrossFadeInFixedTime(animation, 1f);
            }
            else
            {
                studioLeft.transform.GetChild(0).GetComponentInChildren<Animator>().Play(animation);
            }
        }
        if(dir == Direction.RIGHT)
        {
            if (crossFade)
            {
                studioRight.transform.GetChild(0).GetComponentInChildren<Animator>().
                    CrossFadeInFixedTime(animation, 1f);
            }
            else
            {
                studioRight.transform.GetChild(0).GetComponentInChildren<Animator>().Play(animation);
            }
        }
    }
}
