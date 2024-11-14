using UnityEngine;
using TMPro;

public class ScoreUpdater : MonoBehaviour
{
	[SerializeField] private TextMeshPro _score;
	public int Score { get; set; }

	void Update() => _score.text = Score.ToString();
}
