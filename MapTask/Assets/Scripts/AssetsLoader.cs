using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Networking;

public class AssetsLoader : MonoBehaviour
{
    private string _bundleURL = "https://getfile.dokpub.com/yandex/get/https://disk.yandex.ru/d/vWklOpvJR8KE1w";

    private GameObject[] _bundlePrefabs;

    public GameObject[] BundlePrefabs => _bundlePrefabs;

    public IEnumerator NetworkLoadAssets()
    {
        while (!Caching.ready)
        {
            yield return null;
        }

        var request = UnityWebRequestAssetBundle.GetAssetBundle(_bundleURL);

        yield return request.SendWebRequest();

        var bundle = DownloadHandlerAssetBundle.GetContent(request);

        if (request.result == UnityWebRequest.Result.Success)
        {
            _bundlePrefabs = bundle.LoadAllAssets<GameObject>();
        }
    }
}
