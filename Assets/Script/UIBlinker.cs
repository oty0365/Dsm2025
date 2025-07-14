using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIBlinker : MonoBehaviour
{
    public Text levelUpText;

    public void BlinkTwice()
    {
        StartCoroutine(BlinkCoroutine());
    }

    IEnumerator BlinkCoroutine()
    {
        for (int i = 0; i < 2; i++)
        {
            levelUpText.enabled = true;
            yield return new WaitForSeconds(0.2f);

            levelUpText.enabled = false;
            yield return new WaitForSeconds(0.2f);
        }
    }
}
