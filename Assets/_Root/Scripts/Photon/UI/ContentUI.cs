using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Serialization;

public class ContentUI : MonoBehaviour
{
    [SerializeField] private Transform _placeForContent;
    [FormerlySerializedAs("_prefab")] [SerializeField] private ItemContent _itemPrefab;
    private readonly LinkedList<ItemContent> _itemsContent = new LinkedList<ItemContent>();

    private PhotonLauncherLB _lbc;

    public void Init(PhotonLauncherLB lbc)
    {
        _lbc = lbc;
    }
    
    public void UpdateContent(List<RoomInfo> roomsList)
    {
        if(roomsList.Count == _itemsContent.Count && _itemsContent.Count == 0)
            return;
        
        if(roomsList.Count > _itemsContent.Count)
        {
            var delta = roomsList.Count - _itemsContent.Count;
            
            //instantiate item
            
            for (int i = 0; i < delta; i++)
            {
                var item = Instantiate(_itemPrefab);
                item.transform.SetParent(_placeForContent.transform);
                _itemsContent.AddLast(item);
                item.OnConnectRoom += _lbc.JoinRoom;
            }
        }
        if (roomsList.Count < _itemsContent.Count)
        {
            var delta = roomsList.Count - _itemsContent.Count;
            for (int i = 0; i < delta; i++)
            {
                var itemContent = _itemsContent.Last.Value;
                _itemsContent.RemoveLast();
                GameObject.Destroy(itemContent);
            }
        }
        
        DataUpdate(roomsList);
    }

    private void DataUpdate(List<RoomInfo> roomsList)
    {
        var count = 0;
        foreach (var itemContent in _itemsContent)
        {
            if (roomsList[count] != null)
            {
                itemContent.UpdateText(roomsList[count].Name);
                count++;
            }
        }
    }
}
