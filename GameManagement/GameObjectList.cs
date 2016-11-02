using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class GameObjectList : GameObject
{
    protected List<GameObject> children;
    protected List<GameObject> removeList;
    protected List<GameObject> addList;

    public GameObjectList(int layer = 0, string id = "") : base(layer, id)
    {
        removeList = new List<GameObject>();
        addList = new List<GameObject>();
        children = new List<GameObject>();
    }

    public List<GameObject> Children
    {
        get { return children; }
    }

    public void Add(GameObject obj) {
        obj.Parent = this;
        for (int i = 0; i < children.Count; i++) {
            if (children[i].Layer > obj.Layer) {
                children.Insert(i, obj);
                return;
            }
        }
        children.Add(obj);
    }

    public void AddNew(GameObject obj)
    {
        addList.Add(obj);
    }

    public void Remove(GameObject obj)
    {
        removeList.Add(obj);
    }
    
    public void RemoveFlaggedObjects() {
        foreach (GameObject obj in removeList) {
            children.Remove(obj);
            obj.Parent = null;
        }
        removeList.Clear();
}
    public void AddFlaggedObjects() {
        foreach(GameObject obj in addList) {
            Add(obj);
        }
        addList.Clear();
    }


    public GameObject Find(string id)
    {
        foreach (GameObject obj in children)
        {
            if (obj.Id == id)
            {
                return obj;
            }
            if (obj is GameObjectList)
            {
                GameObjectList objList = obj as GameObjectList;
                GameObject subObj = objList.Find(id);
                if (subObj != null)
                {
                    return subObj;
                }
            }
        }
        return null;
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        for (int i = children.Count - 1; i >= 0; i--)
        {
            children[i].HandleInput(inputHelper);
        }
    }

    public override void Update(GameTime gameTime)
    {
        foreach (GameObject obj in children)
        {
            obj.Update(gameTime);
        }
        RemoveFlaggedObjects();
        AddFlaggedObjects();
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (!visible)
        {
            return;
        }
        List<GameObject>.Enumerator e = children.GetEnumerator();
        while (e.MoveNext())
        {
            e.Current.Draw(gameTime, spriteBatch);
        }
    }

    public override void Reset()
    {
        base.Reset();
        foreach (GameObject obj in children)
        {
            obj.Reset();
        }
    }
}
