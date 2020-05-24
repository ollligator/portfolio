using System.Collections.Generic;
using UnityEngine;

public class UIRecordsTable : MonoBehaviour
{
    public GameObject rowPrefab;
    public GameObject panel;
    public Vector3 rowStep;
    public Transform startPosition;

    public void ShowTable()
    {
        Renderer(Records.GetRecords());
        panel.SetActive(true);
    }

    public void HideTable()
    {
        panel.SetActive(false);

        // int count = uiRows.Length;
        //for (int i = 0; i < uiRows.Length; i++)
        //{
        //    Destroy(uiRows[i]);
        //}

        uiRows = null;
    }

    private GameObject[] uiRows;
    private void Renderer(Records.Row[] rows)
    {
        GameObject go;
        List<GameObject> uiRowsList = new List<GameObject>();

        if (rows.Length == 0)
        {
            go = Instantiate(rowPrefab, startPosition.position, Quaternion.identity, panel.transform);
            go.GetComponentInChildren<TMPro.TextMeshProUGUI>().SetText("You don't have records yet...");
            uiRowsList.Add(go);
            uiRows = uiRowsList.ToArray();
            return;
        }

        for (int i = 0; i < rows.Length; i++)
        {
            go = Instantiate(rowPrefab, startPosition.position + rowStep * i, Quaternion.identity, panel.transform);
            go.GetComponentInChildren<TMPro.TextMeshProUGUI>().SetText((i + 1).ToString() + ". " + rows[i].value.ToString());
            uiRowsList.Add(go);
        }

        uiRows = uiRowsList.ToArray();
    }
}