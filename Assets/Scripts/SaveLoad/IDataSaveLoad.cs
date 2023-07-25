using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataSaveLoad
{
    void LoadData(GameData data);
    void SaveData(ref GameData data);
}
