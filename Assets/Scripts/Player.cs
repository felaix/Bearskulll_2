using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    enum CursorType {None,Move,Attack,Interact, UI}
    [System.Serializable]
    struct CursorMapping
    {
        public CursorType type;
        public Texture2D texture;
        public Vector2 hotspot;
    }
    [SerializeField] CursorMapping[] CursorMappings = null;
    [SerializeField] AudioClip _clickSFX;

    [SerializeField] GameObject _clickFX_GO;
    [SerializeField] Health _health;
    private RaycastHit _hit;

    private Fighter _fighter;


    private void OnEnable()
    {
        _fighter = GetComponent<Fighter>();
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(1))
        {
            ActivateBlock();
            Invoke(nameof(DeactivateBlock), 1f);
        }
        if (Input.GetMouseButtonUp(0))
            AudioManager.instance.PlayEffect(_clickSFX);

            if (!_health.isDead)
        {
            if (InteractWithUI()) return;
            if (CombatOnClick()) return;
            if (InteractOnClick()) return;
            if (MovementOnClick()) return;
        }
        SetCursor(CursorType.None);
    }

    private void DeactivateBlock() => _fighter.TriggerBlock(false);
    private void ActivateBlock() => _fighter.TriggerBlock(true);

    private static Ray GetMouseRay()
    {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }

    private bool MovementOnClick()
    {
        if (Input.GetMouseButtonUp(0))
            Instantiate(_clickFX_GO.transform, _hit.point, Quaternion.identity);

        bool hasHit = Physics.Raycast(GetMouseRay(), out _hit);
        if (hasHit)
        {
            if (Input.GetMouseButton(0))
            {
                GetComponent<Movement>().StartMoveAction(_hit.point, 1);
            }
            SetCursor(CursorType.Move);
            return true;
        }
        return false;
    }

    private bool InteractOnClick()
    {
        RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
        foreach (var hit in hits)
        {
            Interactable interactOB = hit.transform.GetComponent<Interactable>();
            if (interactOB == null) continue;
            if (interactOB.gameObject == this.gameObject) continue;
            if (Input.GetMouseButtonDown(0))
            {
                GetComponent<Movement>().StartMoveAction(interactOB.transform.position, 1);
            }
            SetCursor(CursorType.Interact);
            return true;
        }
        return false;
        
    }
    private bool InteractWithUI()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            SetCursor(CursorType.UI);
            return true;
        }
        return false;
    }

    private bool CombatOnClick()
    {
        RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
        foreach (var hit in hits)
        {
            Targetable target = hit.transform.GetComponent<Targetable>();
            if (target == null) continue;
            if (target.gameObject == this.gameObject) continue;

            if (Input.GetMouseButtonDown(0))
            {
                GetComponent<Fighter>().Attack(target.gameObject);
            }
            SetCursor(CursorType.Attack);
            return true;
        }
        return false;
    }


private CursorMapping GetCursorMapping(CursorType type)
    {
        foreach (CursorMapping mapping in CursorMappings)
        { 
            if(mapping.type == type)
            {
                return mapping;
            }

        }
        return CursorMappings[0];
    }

    private void SetCursor(CursorType type)
    {
        CursorMapping mapping = GetCursorMapping(type);
        Cursor.SetCursor(mapping.texture, mapping.hotspot, CursorMode.Auto);

    }

}

