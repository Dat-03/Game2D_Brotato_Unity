using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "All Character", menuName = "Character/CharactersData")]
public class AllPlayerData_SO : ScriptableObject
{
    public List<PlayerData_SO> characters;

    // Phương thức để thêm một nhân vật mới vào danh sách
    public void AddCharacter(PlayerData_SO newCharacter)
    {
        characters.Add(newCharacter);
    }

    // Phương thức để lấy dữ liệu nhân vật từ API backend dựa trên id_charactor
    public IEnumerator FetchCharacterDataById(int id_charactor, System.Action<PlayerData> callback)
    {
        foreach (PlayerData_SO characterSO in characters)
        {
            if (characterSO.id == id_charactor)
            {
                // Gọi phương thức FetchCharacterDataById từ PlayerData_SO và chờ cho đến khi hoàn thành
                yield return characterSO.FetchCharacterDataById(id_charactor, callback);
                yield break;
            }
        }
        Debug.LogError("Character with id " + id_charactor + " not found!");
    }
}
