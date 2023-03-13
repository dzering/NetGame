using System.Text;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _MyGame.Scripts.Controllers.Game
{
    public class GameOver : MonoBehaviourPun
    {
        [SerializeField] private GameOverPanelUI _gameOverPanel;
        
        private void Awake()
        {
            _gameOverPanel = GameObject.Instantiate(_gameOverPanel);
            _gameOverPanel.ExitButton.onClick.AddListener(Exit);
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            _gameOverPanel.gameObject.SetActive(false);
        }


        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                PhotonView photonView = col.gameObject.GetPhotonView();

                string winer = $"Winner: {photonView.Owner.NickName}\n";
                PlayerScore score = photonView.gameObject.GetComponent<PlayerScore>();
                score.Score += 1000;
                string playerScore = $"Score: {score.Score}";
            
                var result = new StringBuilder(winer);
                result.Append(score.Score);
                OpenPanel(result.ToString());
                Time.timeScale = 0f;  
            }
           

        }

        private void OpenPanel(string winner)
        {
            _gameOverPanel.gameObject.SetActive(true);
            _gameOverPanel.ResultText.text = winner;
            Cursor.visible = true;
  
        }

        private void Exit()
        {
            Time.timeScale = 1f;
            PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.LocalPlayer);
            PhotonNetwork.DestroyAll();
            PhotonNetwork.LeaveRoom();
            
            Destroy(GameObject.Find("Root"));
            Destroy(GameObject.Find("Menu Canvas(Clone)"));

            SceneManager.LoadScene("MainMenu");
            
        }

        private void OnDestroy()
        {
            GameObject.Destroy(PhotonRoomManager.Instance);
            _gameOverPanel.ExitButton.onClick.RemoveAllListeners();
        }
    }
}