using System.Collections;
using UnityEngine;

public class BulletTracer : MonoBehaviour
{
    [SerializeField] private float lifeTime = 0.05f;

    private LineRenderer lr;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    public void Init(Vector3 start, Vector3 end)
    {
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        StartCoroutine(KillSoon());
    }

    private IEnumerator KillSoon()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
