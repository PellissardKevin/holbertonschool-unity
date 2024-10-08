using TMPro;

using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

/// <summary>
/// Manages the different states of the game.
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Canvas _gameCanvas;
    [SerializeField]
    private GameObject _prefab;
    [SerializeField]
    private GameObject _ammo;
    [SerializeField]
    private int _numberAmmo;
    [SerializeField]
    private int _numberTarget = 5;
    [SerializeField]
    private Button _startButton;
    [SerializeField]
    private Image _backgroundImage;
    [SerializeField]
    private Image _backgroundImage2;
    [SerializeField]
    private TextMeshProUGUI _scoreText;
    [SerializeField]
    private TextMeshProUGUI _ammoLeft;
    private bool _isGameStarted = false;
    public ARPlane Plane { get; set; }
    public int Score { get; set; }

    public int NumberAmmo 
    { 
        get => _numberAmmo;
        set
        {
            if (value < 0) 
                value = 1;
            _numberAmmo = value;
        }
    }

    private void Update()
    {
        if (_isGameStarted)
            _scoreText.text = Score.ToString();
    }

    /// <summary>
    /// Starts the game.
    /// </summary>
    public void StartGame()
    {
        _startButton.gameObject.SetActive(false);
        _backgroundImage.gameObject.SetActive(true);
        _backgroundImage2.gameObject.SetActive(true);

        PlacePrefabsOnNavMesh();
/*        Vector3 centerPosition = Camera.main.ScreenToWorldPoint(Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f, 0))) + Camera.main.transform.forward;
        Instantiate(_ammo, centerPosition, Quaternion.identity);*/

        _ammoLeft.text = NumberAmmo.ToString();
    }

    /// <summary>
    /// Restarts the game.
    /// </summary>
    public void RestartGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    /// <summary>
    /// Quit the application.
    /// </summary>
    public void QuitGame() => Application.Quit();

    private void PlacePrefabsOnNavMesh()
    {
        for (int i = 0; i < _numberTarget; i++)
        {
            GameObject obj = Instantiate(_prefab, Vector3.zero, Quaternion.identity, Plane.transform);
            NavMeshAgent agent = obj.GetComponent<NavMeshAgent>();
            TargetMovement targetMovement = obj.GetComponent<TargetMovement>();

            targetMovement.Plane = Plane;

            Vector3 randomPos = Random.insideUnitSphere * 1.6f;
            randomPos.x += Plane.transform.position.x;
            randomPos.z += Plane.transform.position.z;

            agent.Warp(new Vector3(randomPos.x, Plane.transform.position.y, randomPos.z));
            obj.transform.localPosition -= new Vector3(0, 0.1f, 0);
            agent.enabled = true;
        }
    }
}
