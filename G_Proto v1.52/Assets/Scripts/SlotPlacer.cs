using UnityEngine;
using System.Collections;

public class SlotPlacer : PipeItemGenerator
{

    public GameObject[] itemPrefabs;

    public override void GenerateItems(Pipe pipe)
    {
        float start = (Random.Range(0, pipe.pipeSegmentCount) + 0.5f);

        float direction = 0.75f;

        float angleStep = 2.4f;
        for (int i = 0; i < 1; i++)
        {
            PipeItem item = Instantiate<PipeItem>(
                itemPrefabs[Random.Range(0, itemPrefabs.Length)].GetComponent<PipeItem>());
            float pipeRotation =
                (start + i * direction) * 360f / pipe.pipeSegmentCount;
            item.Position(pipe, angleStep, pipeRotation);
        }
    }
}
