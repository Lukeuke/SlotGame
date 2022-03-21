using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class GameManagerHttp : MonoBehaviour
{
    public static GameManagerHttp Instance;
    
    private string url = "http://localhost:8000";
    
    private CustomItemsDto _customItemsDto = new CustomItemsDto();

    public void GetWinningItemDto()
    {
        StartCoroutine(IGetWinningItemDto(2));
    }

    public IEnumerator IGetWinningItemDto(int index)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(this.url + "/play"))
        {
            yield return www.SendWebRequest();
            
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.isDone)
                {
                    Debug.Log(www.downloadHandler.text);
                    _customItemsDto = JsonUtility.FromJson<CustomItemsDto>(www.downloadHandler.text);
                    Debug.Log(_customItemsDto.winningItem.dimensions[0]);
                    yield return _customItemsDto.winningItem.dimensions[index];
                }
            }
        }
    }
}
