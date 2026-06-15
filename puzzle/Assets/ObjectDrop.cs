using UnityEngine;

public class ObjectDrop : MonoBehaviour
{
    [Header("落とすオブジェクト（レベル1のプレハブ）")]
    public GameObject dropPrefab;

    [Header("自動で落ちるまでの制限時間（秒）")]
    public float timeLimit = 3.0f;

    private GameObject currentFruit;
    private float timer;

    void Start()
    {
        SpawnNextFruit();
    }

    void Update()
    {
        if (currentFruit == null) return;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 fruitPos = currentFruit.transform.position;
        fruitPos.x = mousePos.x;
        fruitPos.y = 4.0f;
        currentFruit.transform.position = fruitPos;

        timer += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) || timer >= timeLimit)
        {
            DropFruit();
        }
    }

    void SpawnNextFruit()
    {
        timer = 0f;

        if (dropPrefab != null)
        {
            Vector3 spawnPos = new Vector3(0, 4.0f, 0);
            currentFruit = Instantiate(dropPrefab, spawnPos, Quaternion.identity);

            Rigidbody2D rb = currentFruit.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.simulated = false;
            }
        }
    }

    void DropFruit()
    {
        Rigidbody2D rb = currentFruit.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.simulated = true;
        }

        currentFruit = null;

        Invoke("SpawnNextFruit", 1.0f);
    }
}