using UnityEngine;
using Vuforia;

public class SimpleBarcodeScanner : MonoBehaviour
{
    public TMPro.TextMeshProUGUI barcodeAsText;
    public BarcodeBehaviour mBarcodeBehaviour;

    // Update is called once per frame
    void Update()
    {
        if (mBarcodeBehaviour != null && mBarcodeBehaviour.InstanceData != null)
        {
            barcodeAsText.text = mBarcodeBehaviour.InstanceData.Text;
        }
        else
        {
            barcodeAsText.text = "";
        }
    }
}
