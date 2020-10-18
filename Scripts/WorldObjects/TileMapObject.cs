using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.WorldObjects
{
    class TileMapObject : WorldFiniteObject
    {


        //public void SetAsChildOfGrid()
        //{
        //    child = new ChildOfGrid();
        //}

        //protected override void Destroy()
        //{
        //    if(child != null)
        //        child.CheckIfGridShouldDestroy(gameObject);

        //    base.Destroy();
        //}

        //private void Start()
        //{
        //    base.Start();
        //    m_Size = GetComponent<Tilemap>().size.x;
        //}



        //protected override bool IsObjectLeftOfLeftBound(float screenWidth)
        //{
        //    //return transform.position.x * 2 + m_Size < -screenWidth - 1;
        //    return transform.position.x + m_Size / 2 < -screenWidth / 2 - 1;
        //}

        //private void Update()
        //{
        //    base.Update();
        //    if (GetComponent<Renderer>().IsVisibleFrom(Camera.main)) Debug.Log("Visible");
        //    else Debug.Log("Not visible");
        //}

    }
}
