using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Detail : MonoBehaviour
{
    public static Detail Singleton { get; private set; }

    private int _partNumber;
    public bool IsFinish { get; set; }
    public float AlphaFinish { get; set; }
    private int _uppedDetails;
    private int _mustDetails;
    private bool _detailHave;
    private int _currentFindDetail = 0;
    private bool _isRepaired;

    private float _yDistance;

    private bool _isKey;

    private bool _isKeyFirth;
    private bool _isKeyNeed;

    private Transform _cameraTransform;

    private void Start()
    {
        Singleton = this;
        _partNumber = PlayerPrefs.GetInt("Part");
        _cameraTransform = Camera.main.transform;

        if (_partNumber == 1)
        {
            _mustDetails = AllObjects.Singleton.FirstDetails.Count;
            for (int i = 0; i < AllObjects.Singleton.FirstDetailsImgs.Length; i++)
            {
                AllObjects.Singleton.FirstDetailsImgs[i].gameObject.SetActive(true);
            }
            AllObjects.Singleton.BgAudio.clip = AllObjects.Singleton.BgPartFirst;
            AllObjects.Singleton.Arrow.SetActive(true);

            AllObjects.Singleton.FirstObjectsParent.SetActive(true);
            AllObjects.Singleton.SecondObjectsParent.SetActive(false);
            AllObjects.Singleton.ThirdObjectsParent.SetActive(false);
            AllObjects.Singleton.MazeObjectsParent.SetActive(false);
	        AllObjects.Singleton.ForestObject.SetActive(true);
            AllObjects.Singleton.FirthObjectsParent.SetActive(false);
            AllObjects.Singleton.FiveObjectsParent.SetActive(false);

            Character.Singleton.Transform.position = AllObjects.Singleton.FirstPartCharPos.transform.position;
            Character.Singleton.SpawnPosition = AllObjects.Singleton.FirstPartCharPos.transform.position;

            AllObjects.Singleton.AnalyticsEvent.OnEvent("1PLoaded");
        }
        else if(_partNumber == 2)
        {
            _mustDetails = AllObjects.Singleton.SecondDetails.Count;
            for (int i = 0; i < AllObjects.Singleton.SecondDetailsImgs.Length; i++)
            {
                AllObjects.Singleton.SecondDetailsImgs[i].gameObject.SetActive(true);
            }
            AllObjects.Singleton.BgAudio.clip = AllObjects.Singleton.BgPartSecond;
            AllObjects.Singleton.Arrow.SetActive(true);

            AllObjects.Singleton.FirstObjectsParent.SetActive(false);
            AllObjects.Singleton.SecondObjectsParent.SetActive(true);
            AllObjects.Singleton.ThirdObjectsParent.SetActive(false);
            AllObjects.Singleton.MazeObjectsParent.SetActive(false);
	        AllObjects.Singleton.ForestObject.SetActive(false);
            AllObjects.Singleton.FirthObjectsParent.SetActive(false);
            AllObjects.Singleton.FiveObjectsParent.SetActive(false);

            Character.Singleton.Transform.position = AllObjects.Singleton.SecondPartCharPos.transform.position;
            Character.Singleton.SpawnPosition = AllObjects.Singleton.SecondPartCharPos.transform.position;

            AllObjects.Singleton.AnalyticsEvent.OnEvent("2PLoaded");
        }
        else if (_partNumber == 3)
        {
            AllObjects.Singleton.BgAudio.clip = AllObjects.Singleton.BgPartThird;

            AllObjects.Singleton.FirstObjectsParent.SetActive(false);
            AllObjects.Singleton.SecondObjectsParent.SetActive(false);
            AllObjects.Singleton.ThirdObjectsParent.SetActive(true);
            AllObjects.Singleton.MazeObjectsParent.SetActive(false);
	        AllObjects.Singleton.ForestObject.SetActive(true);
            AllObjects.Singleton.FirthObjectsParent.SetActive(false);
            AllObjects.Singleton.FiveObjectsParent.SetActive(false);

            Character.Singleton.Transform.position = AllObjects.Singleton.ThirdPartCharPos.transform.position;
            Character.Singleton.SpawnPosition = AllObjects.Singleton.ThirdPartCharPos.transform.position;


            AllObjects.Singleton.AnalyticsEvent.OnEvent("3PLoaded");
        }
        else if (_partNumber == 4)
        {
            AllObjects.Singleton.BgAudio.clip = AllObjects.Singleton.BgPartSecond;

            AllObjects.Singleton.FirstObjectsParent.SetActive(false);
            AllObjects.Singleton.SecondObjectsParent.SetActive(false);
            AllObjects.Singleton.ThirdObjectsParent.SetActive(false);
            AllObjects.Singleton.MazeObjectsParent.SetActive(true);
	        AllObjects.Singleton.ForestObject.SetActive(false);
            AllObjects.Singleton.FirthObjectsParent.SetActive(false);
            AllObjects.Singleton.FiveObjectsParent.SetActive(false);

            Character.Singleton.Transform.position = AllObjects.Singleton.MazePartCharPos.transform.position;
            Character.Singleton.SpawnPosition = AllObjects.Singleton.MazePartCharPos.transform.position;

            AllObjects.Singleton.AnalyticsEvent.OnEvent("MazeLoaded");
        }
        else if (_partNumber == 5)
        {
            AllObjects.Singleton.BgAudio.clip = AllObjects.Singleton.BgPartSecond;

            AllObjects.Singleton.FirstObjectsParent.SetActive(false);
            AllObjects.Singleton.SecondObjectsParent.SetActive(false);
            AllObjects.Singleton.ThirdObjectsParent.SetActive(false);
            AllObjects.Singleton.MazeObjectsParent.SetActive(false);
            AllObjects.Singleton.ForestObject.SetActive(false);
            AllObjects.Singleton.FirthObjectsParent.SetActive(true);
            AllObjects.Singleton.Arrow.SetActive(true);
            AllObjects.Singleton.FiveObjectsParent.SetActive(false);

            Character.Singleton.Transform.position = AllObjects.Singleton.FirthPartCharPos.transform.position;
            Character.Singleton.SpawnPosition = AllObjects.Singleton.FirthPartCharPos.transform.position;

            AllObjects.Singleton.AnalyticsEvent.OnEvent("4PLoaded");
        }
        else if (_partNumber == 6)
        {
            AllObjects.Singleton.BgAudio.clip = AllObjects.Singleton.BgPartFirst;

            AllObjects.Singleton.FirstObjectsParent.SetActive(false);
            AllObjects.Singleton.SecondObjectsParent.SetActive(false);
            AllObjects.Singleton.ThirdObjectsParent.SetActive(false);
            AllObjects.Singleton.MazeObjectsParent.SetActive(false);
            AllObjects.Singleton.ForestObject.SetActive(false);
            AllObjects.Singleton.FirthObjectsParent.SetActive(false);
            AllObjects.Singleton.FiveObjectsParent.SetActive(true);
            AllObjects.Singleton.Arrow.SetActive(true);
            AllObjects.Singleton.CartoonSlider.gameObject.SetActive(true);

            Character.Singleton.Transform.position = AllObjects.Singleton.FivePartCharPos.transform.position;
            Character.Singleton.SpawnPosition = AllObjects.Singleton.FivePartCharPos.transform.position;

            AllObjects.Singleton.AnalyticsEvent.OnEvent("5PLoaded");
        }
        AllObjects.Singleton.BgAudio.Play();
        AllObjects.Singleton.CarLight.Stop();
    }

    private void Update()
    {
        if (_partNumber == 1)
        {
            for (int i = 0; i < AllObjects.Singleton.FirstDetails.Count; i++)
            {
                if (Vector3.Distance(Character.Singleton.Transform.position, AllObjects.Singleton.FirstDetails[i].transform.position) < 3)
                {
                    AllObjects.Singleton.FirstUpBtn[i].SetActive(true);
                }
                else
                {
                    AllObjects.Singleton.FirstUpBtn[i].SetActive(false);
                }
            }

            if (_uppedDetails < _mustDetails)
            {
                if (AllObjects.Singleton.FirstDetails[_currentFindDetail].activeSelf)
                {
                    AllObjects.Singleton.CurrentFindText.text = $"{(int)Vector3.Distance(AllObjects.Singleton.FirstDetails[_currentFindDetail].transform.position, Character.Singleton.Transform.position)}m";

                    Quaternion _lookRotation = Quaternion.LookRotation((AllObjects.Singleton.FirstDetails[_currentFindDetail].transform.position - _cameraTransform.position).normalized);
                    AllObjects.Singleton.Arrow.transform.rotation = Quaternion.Slerp(AllObjects.Singleton.Arrow.transform.rotation, _lookRotation, Time.deltaTime * 5f);
                }
                else
                {
                    _currentFindDetail++;
                }
            }
            else
            {
                AllObjects.Singleton.CurrentFindText.gameObject.SetActive(false);
                Quaternion _lookRotation = Quaternion.LookRotation((AllObjects.Singleton.FirstPartCar.transform.position - _cameraTransform.position).normalized);
                AllObjects.Singleton.Arrow.transform.rotation = Quaternion.Slerp(AllObjects.Singleton.Arrow.transform.rotation, _lookRotation, Time.deltaTime * 5f);
            }

            if (_detailHave && Vector3.Distance(Character.Singleton.Transform.position, AllObjects.Singleton.FirstPartCar.transform.position) < 3)
            {
                AllObjects.Singleton.DetailInstalBtn.SetActive(true);
            }
            else
            {
                AllObjects.Singleton.DetailInstalBtn.SetActive(false);
            }

            if (Vector3.Distance(Character.Singleton.Transform.position, AllObjects.Singleton.FirstPartCar.transform.position) < 3 || Vector3.Distance(Character.Singleton.Transform.position, AllObjects.Singleton.RepairedCar.transform.position) < 3)
            {
                AllObjects.Singleton.StartEngBtn.SetActive(true);
            }
            else
            {
                AllObjects.Singleton.StartEngBtn.SetActive(false);
            }

            if (IsFinish)
            {
                AlphaFinish += Time.deltaTime / 2f;
                AllObjects.Singleton.FirstFinishPanel.color = new Color(AllObjects.Singleton.FirstFinishPanel.color.r, AllObjects.Singleton.FirstFinishPanel.color.g, AllObjects.Singleton.FirstFinishPanel.color.b, AlphaFinish);
            }

            if (Vector3.Distance(Character.Singleton.Transform.position, AllObjects.Singleton.ThatTowerUp.transform.position) < 5)
            {
                AllObjects.Singleton.TowerUpBtn.SetActive(true);
            }
            else
            {
                AllObjects.Singleton.TowerUpBtn.SetActive(false);
            }

            if (Vector3.Distance(Character.Singleton.Transform.position, AllObjects.Singleton.ThatTowerDown.transform.position) < 5)
            {
                AllObjects.Singleton.TowerDownBtn.SetActive(true);
            }
            else
            {
                AllObjects.Singleton.TowerDownBtn.SetActive(false);
            }
        }
        else if (_partNumber == 2)
        {
            for (int i = 0; i < AllObjects.Singleton.SecondDetails.Count; i++)
            {
                if (Vector3.Distance(Character.Singleton.Transform.position, AllObjects.Singleton.SecondDetails[i].transform.position) < 2 && !_detailHave)
                {
                    AllObjects.Singleton.SecondUpBtn[i].SetActive(true);
                }
                else
                {
                    AllObjects.Singleton.SecondUpBtn[i].SetActive(false);
                }
            }

            if (_uppedDetails < _mustDetails)
            {
                if (AllObjects.Singleton.SecondDetails[_currentFindDetail].activeSelf)
                {
                    AllObjects.Singleton.CurrentFindText.text = $"{(int)Vector3.Distance(AllObjects.Singleton.SecondDetails[_currentFindDetail].transform.position, Character.Singleton.Transform.position)}m";
                    _yDistance = Character.Singleton.Transform.position.y - AllObjects.Singleton.SecondDetails[_currentFindDetail].transform.position.y;

                    Quaternion _lookRotation = Quaternion.LookRotation((AllObjects.Singleton.SecondDetails[_currentFindDetail].transform.position - _cameraTransform.position).normalized);
                    AllObjects.Singleton.Arrow.transform.rotation = Quaternion.Slerp(AllObjects.Singleton.Arrow.transform.rotation, _lookRotation, Time.deltaTime * 5f);

                    if (_yDistance < -2)
                    {
                        AllObjects.Singleton.WhatIsFloorDown.SetActive(false);
                        AllObjects.Singleton.WhatIsFloorUp.SetActive(true);
                    }
                    else if (_yDistance > 2)
                    {

                        AllObjects.Singleton.WhatIsFloorDown.SetActive(true);
                        AllObjects.Singleton.WhatIsFloorUp.SetActive(false);
                    }
                    else
                    {
                        AllObjects.Singleton.WhatIsFloorDown.SetActive(false);
                        AllObjects.Singleton.WhatIsFloorUp.SetActive(false);
                    }
                }
                else
                {
                    _currentFindDetail++;
                }
            }
            else
            {
                AllObjects.Singleton.CurrentFindText.gameObject.SetActive(false);
                if (_isKey)
                {
                    Quaternion _lookRotation = Quaternion.LookRotation((AllObjects.Singleton.SecondPartCar.transform.position - _cameraTransform.position).normalized);
                    AllObjects.Singleton.Arrow.transform.rotation = Quaternion.Slerp(AllObjects.Singleton.Arrow.transform.rotation, _lookRotation, Time.deltaTime * 5f);
                }
                else
                {
                    Quaternion _lookRotation = Quaternion.LookRotation((AllObjects.Singleton.KeyObject.transform.position - _cameraTransform.position).normalized);
                    AllObjects.Singleton.Arrow.transform.rotation = Quaternion.Slerp(AllObjects.Singleton.Arrow.transform.rotation, _lookRotation, Time.deltaTime * 5f);
                }
            }

            if (Vector3.Distance(Character.Singleton.Transform.position, AllObjects.Singleton.ThatDoorInPos.transform.position) < 1)
            {
                AllObjects.Singleton.DoorInBtn.SetActive(true);
            }
            else
            {
                AllObjects.Singleton.DoorInBtn.SetActive(false);
            }

            if (Vector3.Distance(Character.Singleton.Transform.position, AllObjects.Singleton.ThatDoorOutPos.transform.position) < 1)
            {
                AllObjects.Singleton.DoorOutBtn.SetActive(true);
            }
            else
            {
                AllObjects.Singleton.DoorOutBtn.SetActive(false);
            }

            if (_isKey)
            {
                AllObjects.Singleton.KeyUpButton.SetActive(false);
            }
            else
            {
                if (Vector3.Distance(Character.Singleton.Transform.position, AllObjects.Singleton.KeyObject.transform.position) < 2)
                {
                    AllObjects.Singleton.KeyUpButton.SetActive(true);
                }
                else
                {
                    AllObjects.Singleton.KeyUpButton.SetActive(false);
                }
            }

            if (Vector3.Distance(Character.Singleton.Transform.position, AllObjects.Singleton.ThatLadderUp.transform.position) < 2)
            {
                AllObjects.Singleton.LadderUpBtn.SetActive(true);
            }
            else
            {
                AllObjects.Singleton.LadderUpBtn.SetActive(false);
            }

            if (Vector3.Distance(Character.Singleton.Transform.position, AllObjects.Singleton.ThatLadderDown.transform.position) < 2)
            {
                AllObjects.Singleton.LadderDownBtn.SetActive(true);
            }
            else
            {
                AllObjects.Singleton.LadderDownBtn.SetActive(false);
            }

            if (Vector3.Distance(Character.Singleton.Transform.position, AllObjects.Singleton.SecondPartCar.transform.position) < 3)
            {
                AllObjects.Singleton.StartEngBtn.SetActive(true);
            }
            else
            {
                AllObjects.Singleton.StartEngBtn.SetActive(false);
            }

            if (IsFinish)
            {
                AlphaFinish += Time.deltaTime / 2f;
                AllObjects.Singleton.SecondFinishPanel.color = new Color(AllObjects.Singleton.SecondFinishPanel.color.r, AllObjects.Singleton.SecondFinishPanel.color.g, AllObjects.Singleton.SecondFinishPanel.color.b, AlphaFinish);
            }
        }
        else if (_partNumber == 4)
        {
            if (Vector3.Distance(Character.Singleton.transform.position, AllObjects.Singleton.MazePartCar.transform.position) < 2)
            {
                AllObjects.Singleton.MazeCarButton.SetActive(true);
            }
            else
            {
                AllObjects.Singleton.MazeCarButton.SetActive(false);
            }

            if (IsFinish)
            {
                AlphaFinish += Time.deltaTime / 2f;
                AllObjects.Singleton.MazeFinishPanel.color = new Color(AllObjects.Singleton.MazeFinishPanel.color.r, AllObjects.Singleton.MazeFinishPanel.color.g, AllObjects.Singleton.MazeFinishPanel.color.b, AlphaFinish);
            }
        }
        else if (_partNumber == 5)
        {
            if (IsFinish)
            {
                AlphaFinish += Time.deltaTime / 2f;
                AllObjects.Singleton.FirthFinishPanel.color = new Color(AllObjects.Singleton.FirthFinishPanel.color.r, AllObjects.Singleton.FirthFinishPanel.color.g, AllObjects.Singleton.FirthFinishPanel.color.b, AlphaFinish);
            }

            if (_isKeyFirth)
            {
                AllObjects.Singleton.CurrentFindText.text = $"{(int)Vector3.Distance(AllObjects.Singleton.FirthCar.transform.position, Character.Singleton.Transform.position)}m";
                _yDistance = Character.Singleton.Transform.position.y - AllObjects.Singleton.FirthCar.transform.position.y;

                Quaternion _lookRotation = Quaternion.LookRotation((AllObjects.Singleton.FirthCar.transform.position - _cameraTransform.position).normalized);
                AllObjects.Singleton.Arrow.transform.rotation = Quaternion.Slerp(AllObjects.Singleton.Arrow.transform.rotation, _lookRotation, Time.deltaTime * 5f);
            }
            else
            {
                if (!_isKeyNeed)
                {
                    AllObjects.Singleton.CurrentFindText.text = $"{(int)Vector3.Distance(AllObjects.Singleton.FirthCar.transform.position, Character.Singleton.Transform.position)}m";
                    _yDistance = Character.Singleton.Transform.position.y - AllObjects.Singleton.FirthCar.transform.position.y;

                    Quaternion _lookRotation = Quaternion.LookRotation((AllObjects.Singleton.FirthCar.transform.position - _cameraTransform.position).normalized);
                    AllObjects.Singleton.Arrow.transform.rotation = Quaternion.Slerp(AllObjects.Singleton.Arrow.transform.rotation, _lookRotation, Time.deltaTime * 5f);
                }
                else
                {
                    AllObjects.Singleton.CurrentFindText.text = $"{(int)Vector3.Distance(AllObjects.Singleton.KeyFirth.transform.position, Character.Singleton.Transform.position)}m";
                    _yDistance = Character.Singleton.Transform.position.y - AllObjects.Singleton.KeyFirth.transform.position.y;

                    Quaternion _lookRotation = Quaternion.LookRotation((AllObjects.Singleton.KeyFirth.transform.position - _cameraTransform.position).normalized);
                    AllObjects.Singleton.Arrow.transform.rotation = Quaternion.Slerp(AllObjects.Singleton.Arrow.transform.rotation, _lookRotation, Time.deltaTime * 5f);
                }
            }

            if (_yDistance < -2)
            {
                AllObjects.Singleton.WhatIsFloorDown.SetActive(false);
                AllObjects.Singleton.WhatIsFloorUp.SetActive(true);
            }
            else if (_yDistance > 2)
            {

                AllObjects.Singleton.WhatIsFloorDown.SetActive(true);
                AllObjects.Singleton.WhatIsFloorUp.SetActive(false);
            }
            else
            {
                AllObjects.Singleton.WhatIsFloorDown.SetActive(false);
                AllObjects.Singleton.WhatIsFloorUp.SetActive(false);
            }
        }
        else if (_partNumber == 6)
        {
            if (IsFinish)
            {
                AlphaFinish += Time.deltaTime / 2f;
                AllObjects.Singleton.FirthFinishPanel.color = new Color(AllObjects.Singleton.FirthFinishPanel.color.r, AllObjects.Singleton.FirthFinishPanel.color.g, AllObjects.Singleton.FirthFinishPanel.color.b, AlphaFinish);
            }

            AllObjects.Singleton.CurrentFindText.text = $"{(int)Vector3.Distance(AllObjects.Singleton.FiveMyHouse.transform.position, Character.Singleton.Transform.position)}m";

            Quaternion _lookRotation = Quaternion.LookRotation((AllObjects.Singleton.FiveMyHouse.transform.position - _cameraTransform.position).normalized);
            AllObjects.Singleton.Arrow.transform.rotation = Quaternion.Slerp(AllObjects.Singleton.Arrow.transform.rotation, _lookRotation, Time.deltaTime * 5f);
        }
    }

    public void GiveUp()
    {
        if (_partNumber == 1)
        {
            for (int i = 0; i < AllObjects.Singleton.FirstDetails.Count; i++)
            {
                if (Vector3.Distance(_cameraTransform.position, AllObjects.Singleton.FirstDetails[i].transform.position) < 5)
                {
                    AllObjects.Singleton.FirstDetails[i].SetActive(false);
                    AllObjects.Singleton.FirstDetails[i].transform.position = new Vector3(5000, 5000, 5000);
                    AllObjects.Singleton.FirstUpBtn[i].SetActive(false);

                    AllObjects.Singleton.FirstDetailsImgs[i].gameObject.SetActive(false);

                    _detailHave = true;
                    _uppedDetails++;

                    if (_uppedDetails == _mustDetails)
                    {
                        AllObjects.Singleton.CarLight.Play();
                    }

                    AllObjects.Singleton.AnalyticsEvent.OnEvent($"{AllObjects.Singleton.FirstDetails[i].name}");
                }
            }
        }
        else if (_partNumber == 2)
        {
            for (int i = 0; i < AllObjects.Singleton.SecondDetails.Count; i++)
            {
                if (Vector3.Distance(_cameraTransform.position, AllObjects.Singleton.SecondDetails[i].transform.position) < 5)
                {
                    AllObjects.Singleton.SecondDetails[i].SetActive(false);
                    AllObjects.Singleton.SecondDetails[i].transform.position = new Vector3(5000, 5000, 5000);
                    AllObjects.Singleton.SecondUpBtn[i].SetActive(false);

                    AllObjects.Singleton.SecondDetailsImgs[i].gameObject.SetActive(false);

                    _uppedDetails++;
                    AllObjects.Singleton.AnalyticsEvent.OnEvent($"{AllObjects.Singleton.SecondDetails[i].name}");
                }
            }
        }
        else if (_partNumber == 5)
        {
            _isKeyFirth = true;
            AllObjects.Singleton.AnalyticsEvent.OnEvent("4PKeyUpped");
        }
    }
    
    public void DetailInstal()
    {
        _detailHave = false;
        AllObjects.Singleton.RepairAudio.Play();


        if (_uppedDetails == _mustDetails)
        {
            StartCoroutine(ChangeCar());
            AllObjects.Singleton.CarLight.Stop();
        }
    }

    public void StartEngine()
    {
        if (_partNumber == 1)
        {
            if (_isRepaired)
            {
                PlayerPrefs.SetInt("Part 2", 1);
                AllObjects.Singleton.GoAudio.Play();
                IsFinish = true;
                StartCoroutine(Finish());

                AllObjects.Singleton.AnalyticsEvent.OnEvent("1PFinish");
            }
            else
            {
                AllObjects.Singleton.FailStartAudio.Play();
            }
        }
        else if (_partNumber == 2)
        {
            if (_uppedDetails == _mustDetails)
            {
                PlayerPrefs.SetInt("Part 3", 1);
                AllObjects.Singleton.GoAudio.Play();
                IsFinish = true;
                StartCoroutine(Finish());

                AllObjects.Singleton.AnalyticsEvent.OnEvent("2PFinish");
            }
            else
            {
                StartCoroutine(NeedToGet());
            }
        }
        else if (_partNumber == 4)
        {
            AllObjects.Singleton.GoAudio.Play();
            IsFinish = true;
            StartCoroutine(Finish());
            AllObjects.Singleton.AnalyticsEvent.OnEvent("MazeFinish");
        }
        else if (_partNumber == 5)
        {
            if (_isKeyFirth)
            {
                AllObjects.Singleton.GoAudio.Play();
                IsFinish = true;
                StartCoroutine(Finish());
                AllObjects.Singleton.AnalyticsEvent.OnEvent("4PFinish");
            }
            else
            {
                StartCoroutine(TextOnTime(AllObjects.Singleton.FirthPartCarText));
                _isKeyNeed = true;
            }
        }
        else if (_partNumber == 6)
        {
            if (AllObjects.Singleton.CartoonsCounter <= 0)
            {
                IsFinish = true;
                StartCoroutine(Finish());
                AllObjects.Singleton.AnalyticsEvent.OnEvent("5PFinish");
            }
            else
            {
                StartCoroutine(TextOnTime(AllObjects.Singleton.FiveNeedToDeadCartoon));
            }
        }
    }

    public void Ladder(GameObject characterNewPosition)
    {
        Character.Singleton.Transform.position = characterNewPosition.transform.position;
    }

    public void Door(GameObject characterNewPosition)
    {
        if (_isKey)
        {
            Character.Singleton.Transform.position = characterNewPosition.transform.position;
            AllObjects.Singleton.DoorOpen.PlayOneShot(AllObjects.Singleton.DoorOpen.clip);
        }
        else
        {
            AllObjects.Singleton.IsKeyNot.PlayOneShot(AllObjects.Singleton.IsKeyNot.clip);
        }
    }

    public void KeyUp()
    {
        _isKey = true;
    }

    IEnumerator ChangeCar()
    {
        yield return new WaitForSeconds(1);
        AllObjects.Singleton.FirstPartCar.SetActive(false);
        AllObjects.Singleton.RepairedCar.SetActive(true);
        _isRepaired = true;
    }

    IEnumerator Finish()
    {
        if (_partNumber == 1)
        {
            AllObjects.Singleton.FirstFinishPanel.gameObject.SetActive(true);
            yield return new WaitForSeconds(3);
            AllObjects.Singleton.FirstFinishText.SetActive(true);
            Character.Singleton.Transform.position = new Vector3(1, -1000, 1);
        }
        else if (_partNumber == 2)
        {
            AllObjects.Singleton.SecondFinishPanel.gameObject.SetActive(true);
            yield return new WaitForSeconds(3);
            AllObjects.Singleton.SecondFinishText.SetActive(true);
            Character.Singleton.Transform.position = new Vector3(1, -1000, 1);
        }
        else if (_partNumber == 4)
        {
            AllObjects.Singleton.MazeFinishPanel.gameObject.SetActive(true);
            yield return new WaitForSeconds(3);
            AllObjects.Singleton.MazeFinishText.SetActive(true);
            Character.Singleton.Transform.position = new Vector3(1, -1000, 1);
        }
        else if (_partNumber == 5)
        {
            AllObjects.Singleton.FirthFinishPanel.gameObject.SetActive(true);
            yield return new WaitForSeconds(3);
            AllObjects.Singleton.FirthFinishText.SetActive(true);
            Character.Singleton.Transform.position = new Vector3(1, -1000, 1);
        }
        else if (_partNumber == 6)
        {
            AllObjects.Singleton.FiveFinishPanel.gameObject.SetActive(true);
            yield return new WaitForSeconds(3);
            AllObjects.Singleton.FiveFinishText.SetActive(true);
            Character.Singleton.Transform.position = new Vector3(1, -1000, 1);
        }
    }

    IEnumerator NeedToGet()
    {
        AllObjects.Singleton.NeetToGetText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        AllObjects.Singleton.NeetToGetText.gameObject.SetActive(false);
    }

    public void TextTime(GameObject text)
    {
        StartCoroutine(TextOnTime(text));
    }

    IEnumerator TextOnTime(GameObject text)
    {
        text.SetActive(true);
        yield return new WaitForSeconds(3);
        text.SetActive(false);
    }
}
