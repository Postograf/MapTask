using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class ModelSpawner : MonoBehaviour
{
    [SerializeField] private float _maxYDistantionToMarker = 0.5f;
    [SerializeField] private AssetsLoader _assetsLoader;
    [SerializeField] private OnlineMapsMarkerManager _markerManager;
    [SerializeField] private OnlineMapsMarker3DManager _marker3DManager;
    [SerializeField] private OnlineMapsControlBase _control;

    private GameObject[] _prefabs = new GameObject[0];

    private IEnumerator Start()
    {
        yield return StartCoroutine(_assetsLoader.NetworkLoadAssets());

        _prefabs = _assetsLoader.BundlePrefabs;
    }

    private void Update()
    {
        if (Input.touchCount > 0 && _prefabs.Length > 0)
        {
            var touchCoords = _control.GetCoords(Input.GetTouch(0).position);

            var marker = new OnlineMapsMarker();

            var aboveMarker = _markerManager
                .items
                .Any(
                    item => {
                        marker = item;
                        return (item.position - touchCoords)
                        .sqrMagnitude <= _maxYDistantionToMarker * _maxYDistantionToMarker;
                    }
                );

            var marker3DAlreadyExist = _marker3DManager
                .items
                .Any(
                    item => item.position == marker.position
                );

            if (touchCoords != null 
                && aboveMarker
                && !marker3DAlreadyExist)
            {
                _marker3DManager.Create(marker.position.x, marker.position.y, _prefabs.First()); ;
            }
        }
    }
}
