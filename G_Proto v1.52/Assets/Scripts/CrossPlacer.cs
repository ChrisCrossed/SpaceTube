using UnityEngine;
using System.Collections;

public class CrossPlacer : PipeItemGenerator
{

    public GameObject[] itemPrefabs;

    public override void GenerateItems(Pipe pipe)
    {
        float start = pipe.pipeSegmentCount + 0.5f;
        //float direction = Random.value < 0.5f ? 1f : -1f;

        //float angleStep = pipe.CurveAngle / pipe.CurveSegmentCount;

        float direction = 0.30f;

        float angleStep = 2.4f;
        for (int i = 0; i < 1; i++)
        {
            if(i>1)
            {

                PipeItem item = Instantiate<PipeItem>(
                itemPrefabs[Random.Range(0, itemPrefabs.Length)].GetComponent<PipeItem>());
                float pipeRotation =
                    (start + i * direction) * 360f / pipe.pipeSegmentCount;
                item.Position(pipe, angleStep, pipeRotation);

            }
            else
            {

                PipeItem item = Instantiate<PipeItem>(
                itemPrefabs[Random.Range(0, itemPrefabs.Length)].GetComponent<PipeItem>());
                float pipeRotation =
                    ((start * 7.5f) + i * direction) * 360f / pipe.pipeSegmentCount;
                item.Position(pipe, angleStep, pipeRotation);

            }

        }
    }
}
