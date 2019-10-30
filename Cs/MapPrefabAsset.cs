using UnityEngine;

[CreateAssetMenu(menuName = "MyGame/CreateMapPrefabAsset", fileName = "MapPrefabData")]
public class MapPrefabAsset : ScriptableObject
{
    public string FileName = "MapPrefabData";
    [Header("Rateが多いほうが出現率が上がります")]
    [Header("同名が複数存在する場合ランダムで生成時にランダムで選ばれます(未テスト)")]
    public MapPrefabData[] array = null;

    public NameListArray Name;

    public MapPrefabAsset()
    {
        Name = new NameListArray(this);
    }

    public MapTileBace CreateTile(int type, Vector2Int pos, Transform parent)
    {
        if(type < 0 || type >= array.Length){return null;}
        MapTileBace s = Instantiate(array[type].Prefab, parent).GetComponent<MapTileBace>();
        s.transform.position = MapManager.ConvertPos(pos);
        return s;
    }

    // 名前からランダムで番号を取得
    public int GetRPNum(string _name)
    {
        int number = 0;
        for(int i = 0; i < array.Length; i++)
        {
            if(array[i].Name == _name){number += array[i].Rate;}
        }


        number = Random.Range(0, number + 1);

        for(int i = 0; i < array.Length; i++)
        {
            if(array[i].Name == _name){
                number -= array[i].Rate;
                if(number <= 0)
                {
                    return i;
                }
            }
        }
        return -1;
    }

    public class NameListArray
    {
        string[] array;
        MapPrefabAsset data;

        public NameListArray(MapPrefabAsset _data)
        {
            data = _data;
        }

        public void reset()
        {   // 名前が重複しないリストを作成。
            string[] listA = new string[data.array.Length];
            int num = 0;
            for(int i = 0; i < data.array.Length; i++)
            {
                int j = 0;
                while(j < num)
                {
                    if(listA[j] == data.array[i].Name){break;}
                    j++;
                }
                if(num == j)
                {
                    num++;
                    listA[j] = data.array[i].Name;
                }
            }

            array = new string[num];
            for(int i = 0; i < num; i++)
            {
                array[i] = listA[i];
            }
        }

        public string this[int _num]
        {
            get {
                if(array == null){reset();}
                return array[_num];
            }
        }

        public int Length
        {
            get {
                if(array == null){reset();}
                return array.Length;
            }
        }
    }
}

[System.Serializable]
public class MapPrefabData
{
    public string Name = "none";
    public GameObject Prefab = null;
    [Range(0, 100)]
    public int Rate = 1;
}