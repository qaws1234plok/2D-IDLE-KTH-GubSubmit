using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Reward/Gold", fileName = "RewardGold")]
public class currentReward : Reward
{
    [Header("Áö±Þ °ñµå ·®")]
    [SerializeField] private int amount;

    public override void Give(Quest quest)
    {
        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>();
        player.AddGold(amount);
    }
}
