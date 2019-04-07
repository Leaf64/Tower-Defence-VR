using UnityEngine;
    public class BloodSplash : MonoBehaviour
    {
    
        RaycastHit hit;//all
        public LayerMask layer;//all
       // public Brush brush;//ink
       // public Camera cam;//ink
        public Material decalMaterial; //decal
        public float maxSize = 0.8f;//decal
        public float minSize = 1f;//decal
        public float destroyTime = 10f;//decal
        public float destroySpeed = 0.1f;//decal
    public EnemySpawner spawner;
    public float HP = 100f;
        // Use this for initialization
        void Start()
        {
            // Cursor.visible = false;
            // Screen.lockCursor = true;
            MagnetSensor.OnCardboardTrigger += Splash;
        }

        // Update is called once per frame
        void Update()
        {
        //  if (Input.GetKeyDown(KeyCode.Mouse0)||Input.touches.Length>0)
        //  {
        //      Splash();
        //   }
        if (HP <= 0)
        {
            spawner.EnemyCounter--;
            Splash();
            Destroy(gameObject);
        }

    }
    public void TakeDamage(float damage)
    {
        HP -= damage;
        
    }

        public void Splash()
        {
        if (Physics.Raycast(transform.position, -transform.up, out hit, 10, layer))
        {
            STB.ADAOPS.GenericMeshDecal actualDecal = STB.ADAOPS.DecalInGameManager.DECAL_INGAME_MANAGER.CreateNewMeshDecal(decalMaterial, hit.transform, hit.point, hit.normal, Random.Range(minSize, maxSize), new Vector2(0, 360), false, layer);//decal

            actualDecal.SetDestroyable(true, destroyTime, destroySpeed);//decal
                                                                        //      InkCanvas canvas = hit.transform.gameObject.GetComponent<InkCanvas>();//ink
                                                                        //      if (canvas != null)//ink
                                                                        //          canvas.Paint(brush, hit.point,cam);//ink
        }
        }
    
}