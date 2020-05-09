using TheFantasticIsland.DataScript;
using TheFantasticIsland.Helper;
using UnityEngine;

[CreateAssetMenu(menuName = "TheFantasticIsland/Population/Animal", fileName = "NewAnimal")]
public class Animal : Population
{
    [SerializeField]
    private Condition _Condition = null;

    public Condition Condition => _Condition;
}