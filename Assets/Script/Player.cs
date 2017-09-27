using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public float jumpForce = 7.5f;

    public Rigidbody2D rb;
    public SpriteRenderer sr;

    public string currentColor;

    //public Color colorCyan, colorYellow, colorMagenta, colorPink;

    public List<Color> colorList;

    void Start()
    {
        SetRandomColor();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (transform.position.y <= 0)
        {
            GameOver();
        }*/

        if (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0))
            rb.velocity = Vector2.up * jumpForce;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "End")
        {
            Time.timeScale = 0;
        }

        if (col.tag == "ColorChanger")
        {
            SwitchColor();
            Destroy(col.gameObject);
            return;
        }

        if (col.tag != currentColor)
            GameOver();
    }

    void GameOver()
    {
        //Should be a call to the gameOver Scene later.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void SetRandomColor()
    {
        int index = Random.Range(0, colorList.Count);
        sr.color = colorList[index];
        MapTagToColor();
    }

    void SwitchColor()
    {
        List<Color> restColor = new List<Color>();
        for (int i = 0; i < colorList.Count - 1; i++)
            if (!colorList[i].Equals(sr.color))
                restColor.Add(colorList[i]);
        int index = Random.Range(0, restColor.Count);
        sr.color = restColor[index];
        MapTagToColor();
    }

    private void MapTagToColor()
    {
        if (sr.color == colorList[0])
            currentColor = "Cyan";
        else if (sr.color == colorList[1])
            currentColor = "Yellow";
        else if (sr.color == colorList[2])
            currentColor = "Magenta";
        else
            currentColor = "Pink";
        return;
    }
}
