using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TransparentDetection : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] private float transparencyAmount = 0.8f;
    [SerializeField] private float fadeTime = 0.4f;

    //when ontriggerenter with object, change transparency to fade out 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Enemy"))
        {
            if (TryGetComponent<SpriteRenderer>(out var spriteRenderer))
            {
                StartCoroutine(FadeRoutine(spriteRenderer, fadeTime, spriteRenderer.color.a, transparencyAmount));
            }

            if (TryGetComponent<Tilemap>(out var tileMap))
            {
                StartCoroutine(FadeRoutine(tileMap, fadeTime, tileMap.color.a, transparencyAmount));
            }
        }
        
    }

    //when ontriggerexit with object, change transparency to fade in 
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            if (TryGetComponent<SpriteRenderer>(out var spriteRenderer))
            {
                StartCoroutine(FadeRoutine(spriteRenderer, fadeTime, spriteRenderer.color.a, 1f));
            }

            if (TryGetComponent<Tilemap>(out var tileMap))
            {
                StartCoroutine(FadeRoutine(tileMap, fadeTime, tileMap.color.a, 1f));
            }
        }
    }

    //Overloading Fade Routine
    private IEnumerator FadeRoutine(SpriteRenderer spriteRenderer, float fadeTime, float startValue, float transparencyValue)
    {
        float elapsedTime = 0;
        //if elapsedTime < fadeTime, change transparencyt to fade out with elapsedTime/fadeTime
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, transparencyValue, elapsedTime/fadeTime);
            //set the color to stay the same, change the transparency to newAlpha 
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);
            yield return null;
        }
    }

    private IEnumerator FadeRoutine(Tilemap tileMap, float fadeTime, float startValue, float transparencyValue)
    {
        float elapsedTime = 0;
        //if elapsedTime < fadeTime, change transparencyt to fade out with elapsedTime/fadeTime
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, transparencyValue, elapsedTime / fadeTime);
            //set the color to stay the same, change the transparency to newAlpha 
            tileMap.color = new Color(tileMap.color.r, tileMap.color.g, tileMap.color.b, newAlpha);
            yield return null;
        }
    }
}
