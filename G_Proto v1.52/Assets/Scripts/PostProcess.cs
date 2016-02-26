using UnityEngine;
using System.Collections;

public class PostProcess : MonoBehaviour
{

  public float intensity;
  private Material mat;

  // Use this for initialization
  void Awake()
  {
    mat = new Material(Shader.Find("Hidden/CamBW"));
  }

  // Update is called once per frame
  void OnRenderImage(RenderTexture source, RenderTexture destination)
  {
    if (intensity == 0)
    {
      Graphics.Blit(source, destination);
      return;
    }

    mat.SetFloat("_bwBlend", intensity);
    Graphics.Blit(source, destination, mat);

  }
}