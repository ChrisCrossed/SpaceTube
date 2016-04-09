/*******************************  SpaceTube  *********************************
Author: Daya Singh
Contributors:
Course: GAM350
Game:   G_Proto
Date:   4/8/16
File:   PostProcess.cs

Description:
Handles Post processing for camera

Current Problems:


Copyright (C) 2016 DigiPen Institute of Technology.
Reproduction or disclosure of this file or its contents without the prior
written consent of DigiPen Institute of Technology is prohibited.
******************************************************************************/

using UnityEngine;
using System.Collections;

public class PostProcess : MonoBehaviour
{

  public float intensity;
  private Material mat;
  private bool blending;

  // Use this for initialization
  void Awake()
  {
    mat = new Material(Shader.Find("Hidden/CamBW"));
    blending = false;
  }

  public void StartBlend()
  {
    blending = true;
  }

  // Update is called once per frame
  void OnRenderImage(RenderTexture source, RenderTexture destination)
  {
    if (intensity == 0)
    {
      Graphics.Blit(source, destination);
      return;
    }

    if(blending)
    {
      intensity += Time.deltaTime;
      if(intensity > 1.0f)
      {
        intensity = 1.0f;
      }
    }

    mat.SetFloat("_bwBlend", intensity);
    Graphics.Blit(source, destination, mat);

  }
}