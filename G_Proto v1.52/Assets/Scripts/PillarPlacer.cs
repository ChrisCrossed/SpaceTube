using UnityEngine;
using System.Collections;

public class PillarPlacer : PipeItemGenerator
{

    public GameObject[] itemPrefabs;

    public override void GenerateItems(Pipe pipe)
    {
        float start = (Random.Range(0, pipe.pipeSegmentCount) + 0.5f);
        //float direction = Random.value < 0.5f ? 1f : -1f;

        //float angleStep = pipe.CurveAngle / pipe.CurveSegmentCount;

        float direction = 0.65f;

        float angleStep = 2.4f;
        for (int i = 0; i < 4; i++)
        {
            PipeItem item = Instantiate<PipeItem>(
                itemPrefabs[Random.Range(0, itemPrefabs.Length)].GetComponent<PipeItem>());
            float pipeRotation =
                (start + i * direction) * 360f / pipe.pipeSegmentCount;
            item.Position(pipe, angleStep, pipeRotation);
        }
    }
}
