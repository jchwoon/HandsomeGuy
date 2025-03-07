using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseScene : MonoBehaviour
{
    public UIScene UI { get; protected set; }

    private bool _Initialized = false;
    void Start()
    {
        if (Main.ResourceManager.Loaded)
        {
            //Main.Data.Initialize();
            //Main.Game.Initialize();
            Initialize();
        }
        else
        {
            Main.ResourceManager.LoadAllAsync<UnityEngine.Object>("PreLoad", (key, count, totalCount) => {
                Debug.Log($"[GameScene] Load asset {key} ({count}/{totalCount})");
                if (count >= totalCount)
                {
                    Main.ResourceManager.Loaded = true;
                    //Main.Data.Initialize();
                    //Main.Game.Initialize();
                    Initialize();
                }
            });
        }

    }

    protected virtual bool Initialize()
    {
        if (_Initialized) return false;

        //Main.SceneManager.CurrentScene = this;

        Object obj = GameObject.FindObjectOfType<EventSystem>();
        if (obj == null) Main.ResourceManager.Instantiate("EventSystem.prefab").name = "@EventSystem";

        _Initialized = true;
        return true;
    }
}
