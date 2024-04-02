using GoldSprite.UFsm;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoldSprite.EntitySystem2D {
    public class EntityUFsm : BaseFsm {

        private void Start()
        {
            InitFsm(Props);
            Begin();
        }
    }
}
