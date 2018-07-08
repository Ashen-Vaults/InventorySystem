using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteProbable : IProbable
{
    public List<int> SelectIds(List<float> weights, RangedInt range)
    {
		List<int> selections = new List<int>();
        float total = 0f;
		float amount = Random.Range(range.start, range.end);
		for(int i=0; i<weights.Count; i++)
		{
			total += weights[i];
			if(amount <= total)
			{
				selections.Add(i);
			}
		}
		return selections;
    }
}
