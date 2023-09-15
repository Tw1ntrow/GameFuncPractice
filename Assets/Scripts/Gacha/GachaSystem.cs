using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GachaSystem : MonoBehaviour
{
    [System.Serializable]
    public class GachaItem
    {
        public string name;
        public Rarity rarity;
    }

    [System.Serializable]
    public class RarityProbability
    {
        public Rarity rarity;
        [Range(0, 1)] public float probability;
    }

    public enum Rarity
    {
        Common,
        Uncommon,
        Rare,
        UltraRare,
        Legendary
    }

    [SerializeField]
    private List<GachaItem> items;
    
    private List<RarityProbability> rarityProbabilities = 
    new List<RarityProbability>()
        {
        new RarityProbability() { rarity = Rarity.Common, probability = 0.7f },
        new RarityProbability() { rarity = Rarity.Uncommon, probability = 0.2f },
        new RarityProbability() { rarity = Rarity.Rare, probability = 0.08f },
        new RarityProbability() { rarity = Rarity.UltraRare, probability = 0.019f },
        new RarityProbability() { rarity = Rarity.Legendary, probability = 0.001f }
    };

    [SerializeField]
    private Text resultText;

    private List<float> cumulativeRarityProbabilities = new List<float>();

    private void Start()
    {
        // 累積確率テーブルを作成
        float totalProbability = 0;
        foreach (var rarityProb in rarityProbabilities)
        {
            totalProbability += rarityProb.probability;
            cumulativeRarityProbabilities.Add(totalProbability);
        }
    }

    public void DrawGacha()
    {
        float randRarity = Random.value;
        Rarity drawnRarity = Rarity.Common;
        for (int i = 0; i < cumulativeRarityProbabilities.Count; i++)
        {
            if (randRarity <= cumulativeRarityProbabilities[i])
            {
                drawnRarity = rarityProbabilities[i].rarity;
                break;
            }
        }

        List<GachaItem> possibleItems = items.FindAll(item => item.rarity == drawnRarity);
        GachaItem drawnItem = possibleItems[Random.Range(0, possibleItems.Count)];

        DisplayResult(drawnItem);
    }

    private void DisplayResult(GachaItem item)
    {
        resultText.text = "結果:" + item.name;

        switch (item.rarity)
        {
            case Rarity.Common:
                resultText.color = Color.white;
                break;
            case Rarity.Uncommon:
                resultText.color = Color.green;
                break;
            case Rarity.Rare:
                resultText.color = Color.blue;
                break;
            case Rarity.UltraRare:
                resultText.color = Color.magenta;
                break;
            case Rarity.Legendary:
                resultText.color = Color.yellow;
                break;
        }
    }

    public void OnGachaButtonClicked()
    {
        DrawGacha();
    }
}