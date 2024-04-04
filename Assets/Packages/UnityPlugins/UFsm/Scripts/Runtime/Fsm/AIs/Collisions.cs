namespace GoldSprite.UFsm {
    public class Collisions {
        private AIFsm fsm;
        public Collisions(AIFsm fsm) { this.fsm = fsm; }


        public bool IsOutOfLandArea()
        {
            var collBounds = fsm.ctrlFsm.Props.BodyCollider.bounds;
            var b2 = fsm.Props.LandArea;
            var rect = b2;
            return collBounds.min.x < rect.min.x || collBounds.max.x > rect.max.x || collBounds.min.y < rect.min.y || collBounds.max.y > rect.max.y;
        }
    }
}
