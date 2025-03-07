using UnityEngine;

public class CheckNest : MonoBehaviour
{
    public bool isWin = false;
    public GameObject winUI;

    void Update()
    {
        if (Check())
        {
            isWin = true;
        }
        OnGUI();
    }

    // Check all remaining nest in the scene
    public static bool Check()
    {
        // Find all nest in the scene
        Nest[] nests = FindObjectsByType<Nest>(FindObjectsSortMode.None);

        // If there is no nest, player win
        return nests.Length == 0;
    }

    void OnGUI()
    {
        if (isWin)
        {
            winUI.SetActive(true);
        }
    }
}
