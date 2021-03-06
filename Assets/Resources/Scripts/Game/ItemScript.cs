﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemScript : MonoBehaviour
{
	private static int activePopup = 0;

    private bool alreadyMoved = false;
    private Transform _transform;
    private SpriteRenderer _spriteRenderer;
    private Transform popups;
    private Transform[] popup, references;
    private Collider2D _collider2D;
	
	void Start()
	{
        _transform = transform;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider2D = GetComponent<Collider2D>();
        popups = GameObject.FindWithTag("Popups").transform;
        popup = new Transform[popups.childCount];
        for (int i = 0; i < popups.childCount; i++)
            popup[i] = popups.GetChild(i);
        references = new Transform[popups.childCount];
        for (int i = 0; i < popups.childCount; i++)
            references[i] = popup[i].GetChild(0);
    }
	
	
	private void OnTriggerEnter2D(Collider2D colidedObj)
	{
		if(colidedObj.tag == "Bob")
		{
            FindObjectOfType<ScoreManager>().AddPoints(10); //Hardcoded pra testar
			CheckIfEqual();
			CloseNext();
		}
		else if(colidedObj.tag == "Collector")
			Destroy(gameObject);
	}

	private void CheckIfEqual()
	{
		alreadyMoved = false;
		for(int childIndex = 0; childIndex < popups.childCount; childIndex++)
		{
			if(references[childIndex].childCount > 0)
			{
				if(references[childIndex].GetChild(0).name.Contains(this.name))
				{
					popup[childIndex].GetComponent<PopupScript>().AddMultiplier(1);
					alreadyMoved = true;
					Destroy(gameObject);
					break;
				}
			}
		}

		for(int childIndex = 0; childIndex < popups.childCount; childIndex++)
		{
			if(!alreadyMoved)
			{
				if(references[childIndex].childCount <= 0)
				{
					StartCoroutine(popup[activePopup].GetComponent<PopupScript>().Open());
					MoveItem(activePopup);
					activePopup++;
					if(activePopup >= popups.childCount)
						activePopup = 0;
					break;
				}
			}
		}
	}

	private void MoveItem(int i)
	{
        _collider2D.enabled = false;
		_spriteRenderer.sortingLayerName = "UI";
		_spriteRenderer.sortingOrder = 20;
        _transform.parent = popup[i].GetChild(0);
        _transform.position = _transform.parent.position;
        _transform.localScale = new Vector2(1f, 1f);
	}

	private void CloseNext()
	{
			StartCoroutine(popups.transform.GetChild(activePopup).GetComponent<PopupScript>().Close());
	}
}