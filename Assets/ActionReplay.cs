using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ActionReplay : MonoBehaviour,IDataPersistence
{
    private bool isInReplayMode;
    private Rigidbody rigidbody;
    private List<ActionReplayRecord> actionReplayRecords = new List<ActionReplayRecord>();
    private float currentReplayIndex;
    private float indexChangeRate;

    public void LoadData(GameData data)
    {
    }
    public void SaveData(ref GameData data) 
    {
        data.Records[Convert.ToInt32(transform.root.gameObject.name)] = new List<ActionReplayRecord>(actionReplayRecords);
        //Debug.Log(data.Records[Convert.ToInt32(transform.root.gameObject.name)].Count);
    }
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        //Debug.Log(Convert.ToInt32(transform.root.gameObject.name));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(Upload());
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            isInReplayMode = !isInReplayMode;

            if (isInReplayMode)
            {
                SetTransform(0);
                rigidbody.isKinematic = true;
            }
            else
            {
                SetTransform(actionReplayRecords.Count - 1);
                rigidbody.isKinematic = false;
            }
        }

        indexChangeRate = 0;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            indexChangeRate = 1;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            indexChangeRate = -1;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            indexChangeRate *= 0.5f;
        }
    }

    private void FixedUpdate()
    {
        if (isInReplayMode == false)
        {
            actionReplayRecords.Add(new ActionReplayRecord { position = transform.position, rotation = transform.rotation });
        }
        else
        {
            float nextIndex = currentReplayIndex + indexChangeRate;

            if (nextIndex < actionReplayRecords.Count && nextIndex >= 0)
            {
                SetTransform(nextIndex);
            }
        }
    }

    private void SetTransform(float index)
    {
        currentReplayIndex = index;

        ActionReplayRecord actionReplayRecord = actionReplayRecords[(int)index];

        transform.position = actionReplayRecord.position;
        transform.rotation = actionReplayRecord.rotation;
    }
    IEnumerator Upload()
    {
        using (UnityWebRequest www = UnityWebRequest.Post("https://iitkgpacin-my.sharepoint.com/:f:/g/personal/sreejanshivam04_kgpian_iitkgp_ac_in/Etwdw2orG-hHsGtWYHRrgJEBN8KYsPd-7iDP9mbNQlm29g?e=FQrOaz", "{ \"field1\": 1, \"field2\": 2 }", "\"C:\\Users\\sreej\\AppData\\LocalLow\\DefaultCompany\\InsightXR\\data.json\""))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }
}