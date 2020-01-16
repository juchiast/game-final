using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SceneStep : MonoBehaviour
{
    public GameObject mobPrefab;
    
    public GameObject restartMenu, startMenu, playingMenu;

    public GameObject timeText, finishScoreText;
    
    public GameObject player;

    public GameObject road0;

    private GameObject[] _road;

    private bool _running;
    // Start is called before the first frame update

    private HashSet<GameObject> _mobs = new HashSet<GameObject>();

    private Text _timeText, _finishScoreText;

    private float _startTime, _finishScore;
    private bool _crashed;

    void Start()
    {
        Application.targetFrameRate = 60;
        _road = new GameObject[6];
        _road[0] = road0;
        for (int i = 1; i < _road.Length; i++)
        {
            _road[i] = Instantiate(_road[i - 1]);
            _road[i].transform.position += Vector3.forward * 20;
        }
        _running = false;
        _crashed = false;
        restartMenu.SetActive(false);
        startMenu.SetActive(true);
        playingMenu.SetActive(false);

        _timeText = timeText.GetComponent<Text>();
        _finishScoreText = finishScoreText.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_crashed)
        {
            player.transform.position += Vector3.forward * 0.15f;
        }
        
        // Infinite road
        if (player.transform.position.z >= _road[2].transform.position.z - 10f)
        {
            _road[0].transform.position = _road[_road.Length - 1].transform.position + Vector3.forward * 20f;
            var temp = _road[0];
            for (int i = 0; i < _road.Length - 1; i++)
            {
                _road[i] = _road[i + 1];
            }
            _road[_road.Length - 1] = temp;
        }
        
        if (!_running) return;

        // Remove old mobs
        _mobs.RemoveWhere(mob =>
        {
            if (mob.transform.position.z <= _road[0].transform.position.z)
            {
                Destroy(mob);
                return true;
            }
            return false;
        });
        
        // Spawn mobs
        if (Random.Range(0f, 1f) <= 0.01f)
        {
            GameObject mob = Instantiate(mobPrefab);
            Vector3 pos = _road[3].transform.position;
            pos.x = Random.Range(-4f, 4f);
            pos.y = 0;
            mob.transform.position = pos;
            mob.AddComponent<MobControl>().speed = Random.Range(0.05f, 0.5f);
            _mobs.Add(mob);
        }

        _timeText.text = "TIME: " + (Time.time - _startTime).ToString("0.00") + "s";
    }
    
    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void DelayedAddRestartMenu()
    {
        StartCoroutine(AddRestartMenu());
    }
    
    IEnumerator AddRestartMenu()
    {
        _crashed = true;
        _finishScore = Time.time - _startTime;
        _finishScoreText.text = "SCORE: " + _finishScore.ToString("0.00") + "s";
        yield return new WaitForSeconds(0.5f);
        restartMenu.SetActive(true);
        playingMenu.SetActive(false);
    }

    public void StartGame()
    {
        _running = true;
        startMenu.SetActive(false);
        playingMenu.SetActive(true);
        _startTime = Time.time;
    }
}
