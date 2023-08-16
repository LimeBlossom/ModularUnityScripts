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
            DestroyImmediate(studioLeft.transform.GetChild(0));
        }
        if (dir == Direction.RIGHT)
        {
            DestroyImmediate(studioRight.transform.GetChild(0));
        }
    }

    public void AnimateCharacter(string animation, Direction dir)
    {
        if(dir == Direction.LEFT)
        {
            studioLeft.transform.GetChild(0).GetComponentInChildren<Animator>().
                CrossFadeInFixedTime(animation, 1f);
        }
        if(dir == Direction.RIGHT)
        {
            studioRight.transform.GetChild(0).GetComponentInChildren<Animator>().
                CrossFadeInFixedTime(animation, 1f);
        }
    }
}
