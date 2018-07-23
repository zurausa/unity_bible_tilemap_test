using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Tilemapのデモ用のコードです。
// 実際のゲームを想定したコード・構成にはなっていないので注意してください。
// 詳しくは、README.mdを見てください。
public class DemoSceneManager : MonoBehaviour
{
    [SerializeField] Text timeText;
    [SerializeField] Text coinText;
    [SerializeField] CanvasGroup resultGroup;
    [SerializeField] CanvasGroup startGroup;
    [SerializeField] Text resultText;

    float time;
    bool hasBegun;
    bool hasFinished;

    private void Awake()
    {
        this.timeText.text = timeToText(this.time);
        this.coinText.text = coinCountsToText(FindObjectsOfType<Coin>().Count(it => !it.IsCollected));

        foreach (var coin in FindObjectsOfType<Coin>())
        {
            coin.SetCallback(OnCollectedCoin);
        }
    }

    private void Update()
    {
        if (!this.hasBegun && Input.GetKeyDown(KeyCode.Space))
        {
            this.hasBegun = true;
            this.startGroup.GetComponent<Animator>().Play("Hide");
        }

        if (this.hasFinished && Input.GetKeyDown(KeyCode.Space))
        {
            ReloadScene();
        }

        var isPlaying = this.hasBegun && !this.hasFinished;

        if (isPlaying)
        {
            this.time += Time.deltaTime;
            this.timeText.text = timeToText(time);
        }
    }

    private void OnCollectedCoin(Coin coin)
    {
        var remainCoinCounts = FindObjectsOfType<Coin>().Count(it => !it.IsCollected);
        this.coinText.text = coinCountsToText(remainCoinCounts);

        if (remainCoinCounts == 0)
        {
            this.hasFinished = true;
            this.resultText.text = string.Format("{0:0.0}", time);
            this.resultGroup.GetComponent<Animator>().Play("Show");
        }
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private string coinCountsToText(int remainCoinCounts)
    {
        return string.Format("残りコイン:{0}枚", remainCoinCounts);
    }

    private string timeToText(float time)
    {
        return string.Format("時間:{0:0.0}秒", time);
    }
}
