using System.Collections;
using TMPro;
using UnityEngine;

public class RebindUI : MonoBehaviour
{
    [SerializeField] private string _bindLabel;
    [SerializeField] private TextMeshProUGUI _keyTMP;


    private void Start()
    {
        UpdateKeyLabel();
    }

    public void StartToRebind(string actionToRebing)
    {
        StartCoroutine(RebindKey(actionToRebing));
    }

    private IEnumerator RebindKey(string actionToRebing)
    {
        while (!Input.anyKeyDown)
        {
            yield return null;
        }

        foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKey(keyCode))
            {
                CustomInputSystem.RebindKey(actionToRebing, keyCode);
                break;
            }
        }

        UpdateKeyLabel();
    }

    private void UpdateKeyLabel()
    {
        _keyTMP.text = CustomInputSystem.GetBoundKey(_bindLabel).ToString();
    }
}
