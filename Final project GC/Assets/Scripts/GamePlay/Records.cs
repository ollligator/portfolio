using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class Records
{
    public const string FileName = "Records.json";
    public static Table table
    {
        get
        {
            if (_table == null)
            {
                _table = GetTable();
            }

            return _table;
        }
    }
    private static Table _table;

    public static void SaveRecord(float Points)
    {
        Row newRow = new Row(Points);
        table.AddRow(newRow);
        string path = Path.Combine(Application.dataPath, FileName);
        string JSON = JsonUtility.ToJson(table);
        File.WriteAllText(path, JSON);
    }

    public static Row[] GetRecords()
    {
        return table.rows;
    }

    private static Table GetTable()
    {
        string path = Path.Combine(Application.dataPath, FileName);

        if (!File.Exists(path))
        {
            string JSON = JsonUtility.ToJson(new Table());
            File.WriteAllText(path, JSON);
        }

        return JsonUtility.FromJson<Table>(File.ReadAllText(path));
    }

    [Serializable]
    public struct Row
    {
        public float value;

        public Row(float value)
        {
            this.value = value;
        }
    }

    [Serializable]
    public class Table
    {
        public const int MaxRowCount = 5;
        public Row[] rows;

        public void AddRow(Row newRow)
        {
            List<Row> newRows = new List<Row>();

            // Для первого значения
            if (rows.Length == 0)
            {
                newRows.Add(newRow);
                rows = newRows.ToArray();
                return;
            }

            // Если значение больше значения из последей строки, то добавляем в таблицу
            if (newRow.value > rows[rows.Length - 1].value)
            {
                bool added = false;
                // Определяем порядкоывй id для новой строки
                for (int i = 0; i < rows.Length; i++)
                {
                    // если превысило макс количество строк, то прерываем добавлением
                    if (i + 1 >= MaxRowCount) break;

                    if (rows[i].value < newRow.value && !added)
                    {
                        newRows.Add(newRow);
                        newRows.Add(rows[i]);
                        added = true;
                        continue;
                    }

                    newRows.Add(rows[i]);
                }

                rows = newRows.ToArray();
            }
            // Если таблица еще не заполнена
            else if (rows.Length < MaxRowCount)
            {
                for (int i = 0; i < rows.Length; i++)
                {
                    newRows.Add(rows[i]);
                }

                newRows.Add(newRow);
                rows = newRows.ToArray();
            }
        }
    }
}