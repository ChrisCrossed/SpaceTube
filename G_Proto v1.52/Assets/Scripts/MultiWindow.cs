using UnityEngine;
using System.Collections;

public class MultiWindow : PipeItemGenerator
{
    public int HowManyWindows = 4;
    public GameObject[] itemPrefabs;

    public override void GenerateItems(Pipe pipe)
    {
        float start = (Random.Range(0, pipe.pipeSegmentCount) + 0.5f);

        float direction = 0.75f;

        float angleStep = 2.4f;

        for(int j = 0; j <= HowManyWindows; j++)
        {

            for (int i = 0; i < pipe.CurveSegmentCount + (j + 2) ; i++)
            {
                PipeItem item = Instantiate<PipeItem>(
                    itemPrefabs[Random.Range(0, itemPrefabs.Length)].GetComponent<PipeItem>());
                float pipeRotation =
                    (start + i * direction) * 360f / pipe.pipeSegmentCount;
                item.Position(pipe, angleStep, pipeRotation);
            }
            angleStep += 3.4f;
            //start -= 0.5f;
        }  
    }
}
