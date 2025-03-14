using UnityEngine;

public class CheckNest : MonoBehaviour
{
    public bool isWin = false;
    public GameObject winUI;
    public GameObject Spawner;
    public GameObject Gun;
    int enemyCount;

    void Update()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (Check())
        {
            Destroy(Spawner);
            
            if(enemyCount == 0) isWin = true;
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
            Destroy(Gun);
            winUI.SetActive(true);
        }
    }
}
