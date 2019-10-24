using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace Week6 {

    public enum TransitionParameter {
        Select,
        Move,
        Jump,
        JumpForward,
        ForceTransition,
        Grounded,
        Attack,
        Crouch,
        Idle,
        Dash,
        MoveForward,
        MoveBackward,
        Spell,
        TransitionIndex,
        MoveLeft,
        MoveRight,
    }

    public class CharacterControl : MonoBehaviour
    {

        public Sprite icon;
        public Animator SkinnedMeshAnimator;
        public SpellCheck spellCheck;
        public AnimationProgress animationProgress;
        public RuntimeAnimatorController playerDeathAnimator;

        public PlayableCharacterType playableCharacterType;

        public bool MoveUp;
        public bool MoveDown;
        public bool MoveRight;
        public bool MoveLeft;
        public bool Idle;

        public bool Crouch;

        public bool Jump;
        public bool JumpForward;

        public bool Grounded;

        public bool Attack;
        public bool Spell;
        public bool Dash;
        public bool Attack_Normal;

        public EdgeChecker edgeChecker;

        public GameObject colliderEdgePrefab;
        public List<GameObject> BottomSpheres = new List<GameObject>();
        public List<GameObject> FrontSpheres = new List<GameObject>();
        public List<Collider> RagdollParts = new List<Collider>();

        public GameObject handPoint;
        public GameObject firePoint;

        private List<TriggerDetector> TriggerDetectors = new List<TriggerDetector>();
        private Dictionary<string, GameObject> ChildObjects = new Dictionary<string, GameObject>();

        private Rigidbody rigid;

        public int currentHP;
        public int maxHP=100;
        public float HPPct = 1;
        public bool alive;

        public Rigidbody RIGID_BODY {
            get {
                if (rigid == null) {
                    rigid = GetComponent<Rigidbody>();
                }
                return rigid;
            }

        }

        private void Awake()
        {
            alive = true;
            SkinnedMeshAnimator = GetComponentInChildren<Animator>();
            maxHP = 100;
            currentHP = 100;
            bool SwitchBack = false;
            if (SceneManager.GetActiveScene().name =="Main") {
                if (!IsFacingFoward())
                {
                    SwitchBack = true;
                }
                FaceForwad(true);


                if (SwitchBack) {
                    FaceForwad(false);
                }
 
            }
            SetRagdollParts();
            SetColliderSpheres();
            edgeChecker = GetComponentInChildren<EdgeChecker>();
            animationProgress= GetComponentInChildren<AnimationProgress>();
            RegisterCharacter();
        }

        private void RegisterCharacter() {
            if (!CharacterManager.Instance.Characters.Contains(this)) {
                CharacterManager.Instance.Characters.Add(this);
            }
        }

        private void Update()
        {
            if (MoveRight && transform.forward.z > 0f){
                spellCheck.GetInput(TransitionParameter.MoveForward);
                spellCheck.GetInput(TransitionParameter.MoveRight);
            }
            if(MoveLeft && transform.forward.z < 0f)
            {
                spellCheck.GetInput(TransitionParameter.MoveForward);
                spellCheck.GetInput(TransitionParameter.MoveLeft);
            }

            if (Crouch) {
                spellCheck.GetInput(TransitionParameter.Crouch);
            }

            if (Attack) {
                spellCheck.GetInput(TransitionParameter.Attack);
            }

            if (Idle)
            {
                spellCheck.GetInput(TransitionParameter.Idle);
            }

            if (tag == "Player")
            {
                if (transform.position.y <= -3)
                {
                    transform.position = FindObjectOfType<PlayerSpawn>().transform.position;
                    currentHP -= 40;
                    if (currentHP <= 0) {
                        SkinnedMeshAnimator.runtimeAnimatorController = playerDeathAnimator;
                        GetComponent<ManualInput>().enabled = false;
                        GetComponent<CharacterControl>().enabled = false;
                        alive = false;
                        BlackScreenMain blackScreen = FindObjectOfType<BlackScreenMain>();
                        blackScreen.sceneName = "LoseScene";
                        blackScreen.GoToNextScene();
                    }
                }
            }
        }

        public List<TriggerDetector> GetAllTriggers() {
            if (TriggerDetectors.Count == 0){
                TriggerDetector[] arr = this.gameObject.GetComponentsInChildren<TriggerDetector>();

                foreach (TriggerDetector d in arr) {
                    TriggerDetectors.Add(d);
                }
            }

            return TriggerDetectors;
        }

        public float GetHPPct() {
            HPPct = Mathf.Clamp01((float)currentHP / maxHP);
            return HPPct;
        }

        public void SetRagdollParts() {
            RagdollParts.Clear();

            Collider[] colliders = this.gameObject.GetComponentsInChildren<Collider>();

            foreach (Collider c in colliders) {
                if (c.gameObject != this.gameObject)
                {
                    c.isTrigger = true;
                    RagdollParts.Add(c);

                    if (c.GetComponent<TriggerDetector>() == null) {
                        c.gameObject.AddComponent<TriggerDetector>();
                    }  
                }
            }
        }

        //has been replaced by death animation
        //public void TurnOnRagdoll() {
        //    RIGID_BODY.useGravity = false;
        //    RIGID_BODY.velocity = Vector3.zero;
        //    this.gameObject.GetComponent<BoxCollider>().enabled=false;
        //    SkinnedMeshAnimator.enabled = false;
        //    SkinnedMeshAnimator.avatar = null;
        //    foreach (Collider c in RagdollParts) {
        //        c.isTrigger = false;
        //        c.attachedRigidbody.velocity = Vector3.zero;
        //    }
        //}


        private void SetColliderSpheres() {
            BoxCollider box = GetComponent<BoxCollider>();

            float bottom = box.bounds.center.y - box.bounds.extents.y;
            float top = box.bounds.center.y + box.bounds.extents.y;
            float front = box.bounds.center.z + box.bounds.extents.z;
            float back = box.bounds.center.z - box.bounds.extents.z;

            GameObject bottomFront = CreateEdgeSphere(new Vector3(0f, bottom, front));
            GameObject bottomBack = CreateEdgeSphere(new Vector3(0f, bottom, back));
            GameObject topFront = CreateEdgeSphere(new Vector3(0f, top, front));


            bottomFront.transform.parent = this.transform;
            bottomBack.transform.parent = this.transform;
            topFront.transform.parent = this.transform;

            BottomSpheres.Add(bottomFront);
            BottomSpheres.Add(bottomBack);

            FrontSpheres.Add(topFront);
            FrontSpheres.Add(bottomFront);

            float horSec = (bottomFront.transform.position - bottomBack.transform.position).magnitude / 5;
            CreateMiddleSphere(bottomFront, -this.transform.forward, horSec, 4, BottomSpheres);

            float verSec = (bottomFront.transform.position - topFront.transform.position).magnitude / 10;
            CreateMiddleSphere(bottomFront, this.transform.up, verSec, 9, FrontSpheres);
        }

        public void CreateMiddleSphere(GameObject start,Vector3 dir,float sec,int interations, List<GameObject> spheresList) {
            for (int i = 0; i < interations; i++)
            {
                Vector3 pos = start.transform.position + (dir * sec * (i + 1));

                GameObject newObj = CreateEdgeSphere(pos);
                newObj.transform.parent = this.transform;
                spheresList.Add(newObj);
            }
        }

        public GameObject CreateEdgeSphere(Vector3 pos) {
            GameObject obj = Instantiate(colliderEdgePrefab, pos, Quaternion.identity);
            return obj;
        }

        public void MoveForward(float speed, float SpeedGraph) {
            transform.Translate(Vector3.forward * speed * SpeedGraph * Time.deltaTime);
        }

        public void FaceForwad(bool forward) {
            if (forward)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            else {
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
        }


        public bool IsFacingFoward() {
            if (transform.forward.z > 0f)
            {
                return true;
            }
            else {
                return false;
            }
        }

        public Collider GetBodyPart(string name) {
            foreach (Collider c in RagdollParts) {
                if (c.name.Contains(name)) {
                    return c;
                }
            }
            return null;
        }

        public GameObject GetChildObj(string name) {

            if (ChildObjects.ContainsKey(name)) {
                return ChildObjects[name];
            }

            Transform[] arr = this.gameObject.GetComponentsInChildren<Transform>();

            foreach (Transform t in arr) {

                if (t.gameObject.name.Equals(name)) {
                    return t.gameObject;
                }
            }
            return null;
        }
    }
}
