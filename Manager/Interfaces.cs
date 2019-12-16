using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH
{
    public interface DataUpdateObserver
    {
        void UpdateData();
    }

    public interface DataInitObserver
    {
        void InitData();
    }
    

}