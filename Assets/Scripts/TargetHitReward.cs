using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHitReward : MonoBehaviour
{
    [SerializeField] GameObject reward;

    [SerializeField] bool isRewardDisappear;
    [SerializeField] Target[] targets;
    bool allTargetsHit;
    int hitCount;

    bool hasDispensedReward;
    // Start is called before the first frame update
    void Start()
    {
        reward.SetActive(isRewardDisappear);
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
    }

    void DispenseReward()
    {
        if(hasDispensedReward) return;
        reward.SetActive(!isRewardDisappear);
        allTargetsHit = true;
        hasDispensedReward = true;
    }
}
