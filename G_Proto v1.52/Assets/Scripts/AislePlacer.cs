using UnityEngine;
using System.Collections;

public class AislePlacer : PipeItemGenerator
{

    public GameObject[] itemPrefabs;

    public override void GenerateItems(Pipe pipe)
    {
        float start = (Random.Range(0, pipe.pipeSegmentCount) + 0.5f);

        float direction = 0.75f;

        float angleStep = 2.4f;
        for (int i = 0; i < 4; i++)
        {
            PipeItem item = Instantiate<PipeItem>(
                itemPrefabs[Random.Range(0, itemPrefabs.Length)].GetComponent<PipeItem>());
            float pipeRotation =
                (start + direction) * 360f / 60;//pipe.pipeSegmentCount;
            item.Position(pipe, i * angleStep, pipeRotation);
        }
    }
}
