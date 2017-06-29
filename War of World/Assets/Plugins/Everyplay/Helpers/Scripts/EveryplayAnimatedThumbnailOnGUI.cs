using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class EveryplayAnimatedThumbnailOnGUI : MonoBehaviour
{
    public Texture defaultTexture;
    public Rect pixelInset = new Rect(10, 10, 256, 196);
    private int currentIndex;
    private bool transitionInProgress;
    private float blend;
    private Texture bottomTexture;
    private Vector2 bottomTextureScale;
    private Vector2 topTextureScale;
    private Texture topTexture;

    void Awake()
    {
        bottomTexture = defaultTexture;
    }

    void Start()
    {

    }

    void OnDestroy()
    {
        StopTransitions();
    }

    void OnDisable()
    {
        StopTransitions();
    }

    void ResetThumbnail()
    {
        currentIndex = -1;

        StopTransitions();

        blend = 0.0f;
        bottomTextureScale = Vector2.one;
        bottomTexture = defaultTexture;
    }

    private IEnumerator CrossfadeTransition()
    {
        while (blend < 1.0f && transitionInProgress)
        {
            blend += 0.1f;
            yield return new WaitForSeconds(1 / 40.0f);
        }

        bottomTexture = topTexture;
        bottomTextureScale = topTextureScale;

        blend = 0.0f;

        transitionInProgress = false;
    }

    private void StopTransitions()
    {
        transitionInProgress = false;
        StopAllCoroutines();
    }

    void Update()
    {
        
     
    }

    void OnGUI()
    {
        if (Event.current.type.Equals(EventType.Repaint))
        {
            if (bottomTexture)
            {
                GUI.DrawTextureWithTexCoords(new Rect(pixelInset.x, pixelInset.y, pixelInset.width, pixelInset.height), bottomTexture, new Rect(0, 0, bottomTextureScale.x, bottomTextureScale.y));
            }
            if (topTexture && blend > 0.0f)
            {
                Color oldGuiColor = GUI.color;
                GUI.color = new Color(oldGuiColor.r, oldGuiColor.g, oldGuiColor.b, blend);
                GUI.DrawTextureWithTexCoords(new Rect(pixelInset.x, pixelInset.y, pixelInset.width, pixelInset.height), topTexture, new Rect(0, 0, topTextureScale.x, topTextureScale.y));
                GUI.color = oldGuiColor;
            }
        }
    }
}
