using TMPro;
using UnityEngine;

public class SketchyText : MonoBehaviour
{
    public TMP_Text textComponent;
    public float shakeAmount = 2f;
    public float speed = 0.1f;

    void Update()
    {
        textComponent.ForceMeshUpdate();
        var textInfo = textComponent.textInfo;

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            if (!textInfo.characterInfo[i].isVisible)
                continue;

            int vertexIndex = textInfo.characterInfo[i].vertexIndex;
            Vector3[] vertices = textInfo.meshInfo[textInfo.characterInfo[i].materialReferenceIndex].vertices;

            // Apply some random shake to each letter for a rough look
            float shakeX = Mathf.PerlinNoise(Time.time * speed + i * 0.1f, 0) * shakeAmount;
            float shakeY = Mathf.PerlinNoise(0, Time.time * speed + i * 0.1f) * shakeAmount;

            // Modify vertices for each character
            for (int j = 0; j < 4; j++)
            {
                vertices[vertexIndex + j] += new Vector3(shakeX, shakeY, 0);
            }
        }

        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            textInfo.meshInfo[i].mesh.vertices = textInfo.meshInfo[i].vertices;
            textComponent.UpdateGeometry(textInfo.meshInfo[i].mesh, i);
        }
    }
}