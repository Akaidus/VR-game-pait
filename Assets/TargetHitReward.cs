using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHitReward : MonoBehaviour
{
    [SerializeField] GameObject reward;
    [SerializeField] Target[] targets;
    bool allTargetsHit;
    int hitCount;

    bool hasDispensedReward;
    // Start is called before the first frame update
    void Start()
    {
        reward.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(allTargetsHit) return;

        foreach (var target in targets)
        {
            var targetHit = target.isHit;
            if (targetHit)
            {
                hitCount++;
            }
            else
            {
                hitCount = 0;
            }
        }
        if (hitCount >= targets.Length)
        {
            DispenseReward();
        }
        print(hitCount);
    }

    void DispenseReward()
    {
        if(hasDispensedReward) return;
        reward.SetActive(true);
        allTargetsHit = true;
        hasDispensedReward = true;
    }
}