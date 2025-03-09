using UnityEngine;
using UnityEngine.UI;

public class TilePlacement : MonoBehaviour
{
    [SerializeField] bool isOccupied = false;
    public Button buttonListener;

    private void Start()
    {
        buttonListener.onClick.AddListener(P);
    }

    private void OnDestroy()
    {
        buttonListener.onClick.RemoveListener(P);
    }

    public void P()
    {
        isOccupied = true;
    }
}
