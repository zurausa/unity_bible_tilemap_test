using UnityEngine;
using System.Collections;
using System;

// Tilemapのデモ用のコードです。
// 実際のゲームを想定したコード・構成にはなっていないので注意してください。
// 詳しくは、README.mdを見てください。
public class Coin : MonoBehaviour
{
    public bool IsCollected { get; private set; }

    private Action<Coin> onCollectedCoin;

    public void SetCallback(Action<Coin> onCollectedCoin)
    {
        this.onCollectedCoin = onCollectedCoin;
    }

    public void Collect()
    {
        if (!IsCollected)
        {
            IsCollected = true;
            if (this.onCollectedCoin != null)
            {
                this.onCollectedCoin(this);
            }
            StartCoroutine(CollectCoroutine());
        }
    }

    private IEnumerator CollectCoroutine()
    {
        this.onCollectedCoin = null;
        GetComponentInChildren<Animator>().SetTrigger("Hide");
        yield return new WaitForSeconds(1.0F);
        Destroy(gameObject);
    }
}
