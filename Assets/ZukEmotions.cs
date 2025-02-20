using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class ZukEmotions : MonoBehaviour
{
    public List<Sprite> emotions = new List<Sprite>();
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    [YarnCommand("Express")]
    public void Express(string emotion)
    {
        if(emotions != null)
        {
            foreach (var sprite in emotions)
            {
                if (sprite.name == emotion)
                {
                    sr.sprite = sprite;
                }
            }
        }
    }

}
