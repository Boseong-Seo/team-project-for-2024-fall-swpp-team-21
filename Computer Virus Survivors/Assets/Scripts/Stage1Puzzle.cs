using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Stage1Puzzle : MonoBehaviour
{
    [SerializeField] private GameObject unsolvedPuzzle;
    [SerializeField] private GameObject solvedPuzzle;
    // or use Materials instead

    [SerializeField] private float onGroundPos = 2.5f;
    [SerializeField] private float upSpeed = 1.0f;

    private BoxCollider boxCollider;
    private bool isPlayerNear = false;

    // Enable되면 바닥 아래에서 천천히 위로 올라옴
    private void OnEnable()
    {
        boxCollider = GetComponent<BoxCollider>();
        StartCoroutine(GoUp());
    }

    // 가까이에서 Space 누르면 게임 클리어
    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.Space))
        {
            unsolvedPuzzle.SetActive(false);
            solvedPuzzle.SetActive(true);
            GameManager.instance.GameClear();
        }
    }

    private IEnumerator GoUp()
    {
        while (true)
        {
            transform.position += Time.deltaTime * upSpeed * Vector3.up;
            if (transform.position.y >= onGroundPos)
            {
                boxCollider.enabled = true;
                break;
            }
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Stage1Goal.instance.hasPiece)
        {
            Debug.Log("Press Space to solve the puzzle");
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && Stage1Goal.instance.hasPiece)
        {
            Debug.Log("Player is far away");
            isPlayerNear = false;
        }
    }
}
