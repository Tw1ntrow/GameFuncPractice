using UnityEngine;

namespace Assets.Scripts.BuckButton
{

    public class BackButtonManager : MonoBehaviour
    {
        private static BackButtonManager _instance;
        private CommonDialog currentDialog;

        public static BackButtonManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject go = new GameObject("BackButtonManager");
                    _instance = go.AddComponent<BackButtonManager>();
                }
                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                HandleBackButton();
            }
        }

        private void HandleBackButton()
        {

            if (currentDialog != null)
            {
                currentDialog.HandleBackButton();
            }
        }

        public void SetCurrentDialog(CommonDialog dialog)
        {
            currentDialog = dialog;
        }

        public void ClearCurrentDialog()
        {
            currentDialog = null;
        }
    }

}