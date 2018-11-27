using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicGridLayout : MonoBehaviour {

    [SerializeField]
    private int rows;
    [SerializeField]
    private int cols;

    void Start()
    {
        RectTransform parentRect = gameObject.GetComponent<RectTransform>();
        GridLayoutGroup gridLayout = gameObject.GetComponent<GridLayoutGroup>();
        var sizeCols = (parentRect.rect.height - gridLayout.padding.left - gridLayout.padding.left - (cols * gridLayout.spacing.x)) / cols;
        var sizeRows = (parentRect.rect.width  - gridLayout.padding.left - gridLayout.padding.left - (rows * gridLayout.spacing.x)) / rows;
        gridLayout.cellSize = new Vector2(sizeRows, sizeCols);
    }
}
