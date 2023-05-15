using System;

namespace DoomEnemies {
    public class SpriteController : MonoBehaviour, ILifeBehavior {
        public SpriteRenderer target;
        public Animation[] animations;
        private Animation activateAnimation;
        private AnimationFrame activeFrame;
        private int index = 0;
        private float stopwatch = 0f;
        public CharacterBody body;
        public delegate void AnimationEvent(AnimationType type);
        public event AnimationEvent OnAnimationEvent;

        public void Start() {
            for (int i = 0; i < animations.Length; i++) {
                animations[i].Initialize();
            }
            
            activateAnimation = animations[0]; // should always be idle
            activeFrame = activateAnimation.GetFrame(0);
        }
        public void FixedUpdate() {
            stopwatch += Time.fixedDeltaTime;
            if (stopwatch >= activeFrame.delay) {
                stopwatch = 0f;
                index++;

                if (index > activateAnimation.FrameIndexes.Length - 1) {
                    if (activateAnimation.Type == AnimationType.Death) {
                        OnAnimationEvent?.Invoke(activateAnimation.Type);
                        return; // dont advance anims past death
                    }
                    index = 0;
                    OnAnimationEvent?.Invoke(activateAnimation.Type);
                    activateAnimation = PickNextAnimation();
                    activeFrame = activateAnimation.GetFrame(0);
                }
                else {
                    activeFrame = activateAnimation.GetFrame(index);
                }
            }

            if (activateAnimation.Type == AnimationType.Death) {
                target.sprite = activeFrame.Frames[0];
                return; // death anims dont angle
            }

            Vector3 spriteForward = body ? BackstabManager.GetBodyForward(body).Value : base.transform.forward;
            Vector3 cameraForward = CameraRigController.instancesList[0].sceneCam.transform.forward;
            float angle = Vector2.SignedAngle(new(spriteForward.x, spriteForward.z), new(cameraForward.x, cameraForward.z));

            int spr = (int)(((angle + 180) / 45) % 8);

            target.sprite = activeFrame.Frames[spr];
        }

        public Animation PickNextAnimation() {
            if (body && !body.GetNotMoving()) {
                return animations.FirstOrDefault(x => x.Type == AnimationType.Walk) ?? animations[0];
            }

            return animations[0];
        }

        public void SetNextAnimation(AnimationType type) {
            activateAnimation = animations.FirstOrDefault(x => x.Type == type) ?? animations[0];
            activeFrame = activateAnimation.GetFrame(0);
            index = 0;
            stopwatch = 0f;
        }

        public void OnDeathStart() {
            EntityStateMachine esm = EntityStateMachine.FindByCustomName(base.gameObject, "Body");
            EntityState state = null;
            if (base.gameObject.name.Contains("CacodemonBody")) {
                state = new States.Cacodemon.CacoDeath();
            }
            else {
                state = new States.BaseDeathState();
            }

            esm.SetNextState(state);
        }
    }

    [Serializable]
    public class Animation {
        public string Name;
        public string Identifier;
        public string[] FrameIndexes;
        public AnimationType Type;
        public float cycleDelay = 0.3f;
        //
        internal List<List<Sprite>> sprites = new();
        
        public void Initialize() {
            for (int i = 0; i < FrameIndexes.Length; i++) {
                List<Sprite> frames = new();
                if (Type == AnimationType.Death) {
                    frames.Add(Main.bundle.LoadAsset<Sprite>(Identifier + FrameIndexes[i] + "0.png"));
                    sprites.Add(frames);
                    continue;
                }

                for (int j = 0; j < 8; j++) {
                    Sprite spr = Main.bundle.LoadAsset<Sprite>(Identifier + FrameIndexes[i] + (j + 1) + ".png");
                    frames.Add(spr);
                }
                sprites.Add(frames);
            }
        }

        public AnimationFrame GetFrame(int index) {
            AnimationFrame frame = new();
            frame.Name = Name;
            frame.Frames = sprites[index].ToArray();
            frame.delay = cycleDelay;
            return frame;
        }
    }

    public class AnimationFrame {
        public string Name;
        public Sprite[] Frames;
        public float delay;
    }

    [Serializable]
    public enum AnimationType {
        Idle,
        Walk,
        Melee,
        Missile,
        Death
    }
}