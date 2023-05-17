using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour, IInteractable {
  public ItemsData item;
  public string GetInteractPromp(){
    return string.Format("Pickup {0}", item.ItemName);
  }
  public void OnInteract(){
    Destroy(gameObject);
  }
}
