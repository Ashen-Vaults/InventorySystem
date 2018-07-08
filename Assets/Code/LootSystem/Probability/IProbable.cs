using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProbable 
{
	List<int> SelectIds(List<float> weights, RangedInt range);
}
