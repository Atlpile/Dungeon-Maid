using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public E_KeyType keyType;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            switch (keyType)
            {
                case E_KeyType.Red:
                    GameManager.Instance.hasRedKey = true;
                    break;
                case E_KeyType.Blue:
                    GameManager.Instance.hasBlueKey = true;
                    break;
                case E_KeyType.Yellow:
                    GameManager.Instance.hasYellowKey = true;
                    break;
                case E_KeyType.Green:
                    GameManager.Instance.hasGreenKey = true;
                    break;
            }

            AudioManager.Instance.SoundPlay("item_key");
            Destroy(gameObject);
        }
    }


}
