using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryTeaser : StoryElement
{
    private bool m_Show = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_Show)
        {
            StopAllCoroutines();
            StartCoroutine(ShowTeaser());
            m_Show = true;
        }
    }

    IEnumerator ShowTeaser()
    {
        yield return new WaitForSeconds(1.25f);
        FindObjectOfType<Narrator>().StartDialog(Dialog);
    }

    public void NextScene()
    {
        FindObjectOfType<LoadScene>().LoadLevel();
    }
}
