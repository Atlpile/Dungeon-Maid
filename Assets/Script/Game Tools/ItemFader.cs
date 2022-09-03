using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFader : MonoBehaviour
{
    private SpriteRenderer sp;
    public float fadeSpeed = 0.05f;

    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        StartCoroutine(Fade());
    }

    public IEnumerator Fade()
    {
        while (sp.color.a > 0)
        {
            sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, sp.color.a - fadeSpeed);
            yield return new WaitForFixedUpdate();
        }

    }


}
