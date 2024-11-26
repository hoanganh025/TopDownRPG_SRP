using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] private Material flashMaterial;
    [SerializeField] private float timeFlash = 0.1f;

    private SpriteRenderer spriteRenderer;
    private Material defaultMaterial;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        //get the default material
        defaultMaterial = spriteRenderer.material;
    }

    public IEnumerator FlashRoutine()
    {
        spriteRenderer.material = flashMaterial;
        yield return new WaitForSeconds(timeFlash);
        spriteRenderer.material = defaultMaterial;
    }
}
