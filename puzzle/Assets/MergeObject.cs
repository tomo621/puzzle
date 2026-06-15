using UnityEngine;

public class MergeObject : MonoBehaviour
{
    [Header("このオブジェクトのレベル (1, 2, 3...)")]
    public int objectLevel;

    [Header("進化した後のプレハブ（空欄なら進化しない）")]
    public GameObject nextLevelPrefab;

    private bool hasMerged = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasMerged) return;

        MergeObject other = collision.gameObject.GetComponent<MergeObject>();

        if (other != null && other.objectLevel == this.objectLevel)
        {
            
            if (this.objectLevel == 3) return;

            if (this.gameObject.GetInstanceID() > other.gameObject.GetInstanceID())
            {
                this.hasMerged = true;
                other.hasMerged = true;

                if (nextLevelPrefab != null)
                {
                    Vector2 spawnPos = (transform.position + other.transform.position) / 2f;
                    Instantiate(nextLevelPrefab, spawnPos, Quaternion.identity);
                }

                //
                if (ScoreManager.instance != null)
                {
                    ScoreManager.instance.AddScore(10);
                }

                Destroy(this.gameObject);
                Destroy(other.gameObject);
            }
        }
    }
}