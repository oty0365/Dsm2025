using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class AugmentSet
{
    public TextMeshProUGUI augName;
    public Image augImage;
    public TextMeshProUGUI augDesc;
}
public class AugmentUI : MonoBehaviour
{
    [SerializeField] private GameObject augmentPanel;
    [SerializeField] AugmentSet[] augmentSet;
    private AugmentData[] augmentDatas;

    private void Start()
    {
        augmentPanel.SetActive(false);
        AugmentManager.Instance.setUi+=UpdateUI;
    }

    private void UpdateUI(AugmentData[] datas)
    {
        augmentDatas = datas;
        for (var i = 0; i < augmentSet.Length; i++)
        {
            augmentSet[i].augName.text = datas[i].augmentName;
            augmentSet[i].augImage.sprite = datas[i].augmentSprite;
            augmentSet[i].augDesc.text = datas[i].augmentDescription;
        }
        augmentPanel.SetActive(true);
    }

    public void AugmentSelection(int index)
    {
        AugmentManager.Instance.Execute(augmentDatas[index].augmentType);
        augmentPanel.SetActive(false);
    }
}
