using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH
{

    public class UISizeChangerBtn : UIButton
    {
        public override void Click()
        {
            TableSize.SizeChange();
        }
    }
}