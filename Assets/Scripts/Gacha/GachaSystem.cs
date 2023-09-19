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
        public bool isPickup; // �s�b�N�A�b�v�A�C�e�����ǂ���
        [Range(0, 1)] public float pickupProbability; // �s�b�N�A�b�v�m��
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
        new RarityProbability() { rarity = Rarity.Legendary, probability = 0.001f },
    };

    [SerializeField]
    private Text resultText;
    [SerializeField]
    private Text multiDrawResultText;
    [SerializeField]
    private int maxDrawsForTendon = 100;
    [SerializeField]
    private GachaItem tendonItem; // �V���A�C�e��

    private int drawCounter = 0;
    private List<float> cumulativeRarityProbabilities = new List<float>();

    private void Start()
    {
        // �ݐϊm���e�[�u�����쐬
        float totalProbability = 0;
        foreach (var rarityProb in rarityProbabilities)
        {
            totalProbability += rarityProb.probability;
            cumulativeRarityProbabilities.Add(totalProbability);
        }
        ResetBox();


    }

    private GachaItem CheckTendonAndDraw()
    {
        drawCounter++;

        if (drawCounter >= maxDrawsForTendon)
        {
            drawCounter = 0; // �J�E���^�[���Z�b�g
            return tendonItem; // �V���A�C�e����Ԃ�
        }

        return DrawSingleGacha();
    }

    private GachaItem CheckPickupAndDraw()
    {
        List<GachaItem> pickupItems = items.FindAll(item => item.isPickup);
        foreach (var pickupItem in pickupItems)
        {
            if (Random.value < pickupItem.pickupProbability)
            {
                return pickupItem;
            }
        }

        return CheckTendonAndDraw();
    }

    public void DrawGacha()
    {
        GachaItem drawnItem = CheckPickupAndDraw();

        resultText.text = "����:" + drawnItem.name;
        resultText.color = GetColorForRarity(drawnItem);
    }

    public void Draw10Gacha()
    {
        List<GachaItem> drawnItems = new List<GachaItem>();
        for (int i = 0; i < 10; i++)
        {
            drawnItems.Add(CheckPickupAndDraw());
        }

        DisplayMultiDrawResult(drawnItems);
    }

    private Color GetColorForRarity(GachaItem item)
    {
        switch (item.rarity)
        {
            case Rarity.Common:
                return Color.white;
            case Rarity.Uncommon:
                return Color.green;
            case Rarity.Rare:
                return Color.blue;
            case Rarity.UltraRare:
                return Color.magenta;
            case Rarity.Legendary:
                return Color.yellow;
            default: 
                return Color.white;
        }
    }

    private GachaItem DrawSingleGacha()
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
        return possibleItems[Random.Range(0, possibleItems.Count)];
    }

    private void DisplayMultiDrawResult(List<GachaItem> drawnItems)
    {
        string resultStr = "10�K�`�� ���ʁI:\n";
        for (int i = 0; i < drawnItems.Count; i++)
        {
            Color color = GetColorForRarity(drawnItems[i]);
            string colorCode = ColorUtility.ToHtmlStringRGB(color);
            resultStr += string.Format("{0}�A��: <color=#{1}>{2}</color>\n", i + 1, colorCode, drawnItems[i].name);
        }
        multiDrawResultText.text = resultStr;
    }

    private List<GachaItem> currentBox = new List<GachaItem>();

    private void ResetBox()
    {
        currentBox = new List<GachaItem>(items); // �����A�C�e�����X�g�Ń{�b�N�X�����Z�b�g
    }

    private GachaItem DrawBoxGacha()
    {
        if (currentBox.Count == 0)
        {
            resultText.text = "�h���b�v�{�b�N�X����ł��I";
            resultText.color = Color.red;
            return null;
        }

        int index = Random.Range(0, currentBox.Count);
        GachaItem drawnItem = currentBox[index];
        currentBox.RemoveAt(index);

        return drawnItem;
    }

    public void OnResetDropBoxGacha()
    {
        resultText.text = "�h���b�v�{�b�N�X�����Z�b�g���܂����I";
        resultText.color = Color.red;
        ResetBox();
    }

    public void OnBoxGachaButtonClicked()
    {
        GachaItem drawnItem = DrawBoxGacha();
        if(drawnItem == null)
        {
            return;
        }
        resultText.text = "�h���b�v�{�b�N�X ����:" + drawnItem.name;
        resultText.color = GetColorForRarity(drawnItem);
    }

    public void On10GachaButtonClicked()
    {
        Draw10Gacha();
    }

    public void OnGachaButtonClicked()
    {
        DrawGacha();
    }
}