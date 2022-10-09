using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Ball : MonoBehaviour
{
    private GameManager gm;
    public Rigidbody rb;
    public Image lvBar;
    private Vector2 _firstPos;
    private Vector2 _secondPos;
    private Vector2 _currentPos;
    public float _moveSpeed;
    public float _currentGroundNumber;
    // Start is called before the first frame update
    void Start()
    {
        
           gm = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Swipe();
        if(gm._groundNumbers!=0)
        lvBar.fillAmount = _currentGroundNumber / gm._groundNumbers;
        if(lvBar.fillAmount==1)
		{
            gm.LevelUpdate();
            
		}
    }
    private void Swipe()
	{
        if (Input.GetMouseButtonDown(0))
		{
            _firstPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

		}
        if (Input.GetMouseButtonUp(0))
        {
            _secondPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            _currentPos = new Vector2(
                _secondPos.x-_firstPos.x,_secondPos.y-_firstPos.y
                );
        }
        _currentPos.Normalize();    
        if(_currentPos.y<0 &&_currentPos.x>-0.5f&&_currentPos.x<0.5f)
		{//Back
            rb.velocity = Vector3.back * _moveSpeed;
		}
        else if(_currentPos.y>0 && _currentPos.x > -0.5f && _currentPos.x < 0.5f)
		{//Forward
            rb.velocity = Vector3.forward * _moveSpeed;
        }
        else if (_currentPos.x < 0 && _currentPos.y > -0.5f && _currentPos.y < 0.5f)
        {//Left
            rb.velocity = Vector3.left * _moveSpeed;
        }
        else if (_currentPos.x > 0 && _currentPos.y > -0.5f && _currentPos.y < 0.5f)
        {//Right
            rb.velocity = Vector3.right * _moveSpeed;
        }
    }
	private void OnCollisionEnter(Collision collision)
	{
        if(collision.gameObject.GetComponent<MeshRenderer>().material.color !=this.GetComponent<MeshRenderer>().material.color)
		{
            if (collision.gameObject.tag == "Ground")
		{
            collision.gameObject.GetComponent<MeshRenderer>().material.color = this.GetComponent<MeshRenderer>().material.color;
            Constraints();
                _currentGroundNumber++;
            }
               
		}
        

	}
    private void Constraints()
	{
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
	}
}
