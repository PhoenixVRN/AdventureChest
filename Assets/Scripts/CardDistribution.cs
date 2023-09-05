using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDistribution : MonoBehaviour
{
    public List<GameObject> AllHeroCard;
    public List<GameObject> AllEmamyCard;

    public GameObject containerHero;
    public GameObject containerEnamy;

    public List<GameObject> DistributionCard(int count, bool player)
    {
        List<GameObject> DistributionCard = new List<GameObject>();
        var allCard = player ? AllHeroCard : AllEmamyCard;
        var container = player ? containerHero : containerEnamy;
        for (int i = 0; i < count; i++)
        {
            var card = Instantiate(allCard[Random.Range(0, allCard.Count)], container.transform);
            DistributionCard.Add(card);
        }
        return DistributionCard;
    }
}
