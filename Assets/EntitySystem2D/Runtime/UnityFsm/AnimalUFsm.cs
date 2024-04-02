using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoldSprite.UFsm {
    public class AnimalUFsm : MonoBehaviour {
        public BaseUProps props;
        public BaseFsm fsm;


        private void Start()
        {
            props = GetComponent<BaseUProps>();
            fsm = new BaseFsm(props);
        }

        [ContextMenu("Move")]
        public void Move()
        {
            fsm.Command(BaseFsmCommand.Move, new Vector2(1, 0));
        }

        private void Update()
        {
            fsm.Update();
        }

        private void FixedUpdate()
        {
            fsm.FixedUpdate();
        }
    }
}
