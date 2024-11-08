using UnityEngine;

public class CreatingTarget : MonoBehaviour
{
    // フォルダ内のマテリアルを格納する変数
    private Material[] materials;

    void Start()
    {
        // フォルダパスからすべてのマテリアルをロード
        materials = Resources.LoadAll<Material>("img_and_material");

        // マテリアルがロードできた場合のみ3秒ごとに `SpawnTarget` メソッドを呼び出す
        if (materials.Length > 0)
        {
            InvokeRepeating("SpawnTarget", 0f, 1f);
        }
        else
        {
            Debug.LogWarning("指定フォルダ内にマテリアルが見つかりません。パスを確認してください。");
        }
    }

    // ターゲットを生成するメソッド
    void SpawnTarget()
    {
        // target プレハブをロード
        GameObject targetPrefab = (GameObject)Resources.Load("target");

        if (targetPrefab != null)
        {
            // ランダムで1つのマテリアルを選ぶ
            Material selectedMaterial = materials[Random.Range(0, materials.Length)];

            // target のインスタンスを生成
            GameObject targetInstance = Instantiate(targetPrefab, new Vector3(-30.0f, 0.0f, -1f), Quaternion.identity);

            // Renderer コンポーネントを取得してマテリアルを適用
            Renderer renderer = targetInstance.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material = selectedMaterial;
            }
            else
            {
                Debug.LogWarning("Rendererが見つかりません。このスクリプトをRenderer付きのオブジェクトにアタッチしてください。");
            }
        }
        else
        {
            Debug.LogWarning("target プレハブが見つかりません。Resources フォルダに target プレハブが存在するか確認してください。");
        }
    }
}
