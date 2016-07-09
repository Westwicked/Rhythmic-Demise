﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

public class ResourceManagement : MonoBehaviour {

    public Sprite cancerKnightSprite, cancerArcherSprite, cancerPriestSprite;
    public Sprite diabeticKnightSprite, diabeticArcherSprite, diabeticPriestSprite;
    public Image slot1, slot2, slot3;
    private Text noneText1, noneText2, noneText3;
    public Canvas chooseCanvas, mainCanvas;
    public Text resourceText, energyText;
    public Text countText1, countText2, countText3;
    public UnityEngine.UI.Button playButton;
    private int slotClicked;
    
    private Sprite originalSprite1, originalSprite2, originalSprite3;

    /*Choose canvas component*/
    public Text knightLevel, knightAttack, knightDefense, archerLevel, archerAttack, archerDefense, priestLevel, priestAttack, priestDefense;
    public Text knightResource, archerResource, priestResource;
    public UnityEngine.UI.Button knightLeader, archerLeader, priestLeader;

    public Image chooseKnightSprite, chooseArcherSprite, choosePriestSprite;
	void Start () {

        slotClicked = 0;
        StartMain();
        StartChoose();
        
        InitMain();
        InitChoose();
        chooseCanvas.enabled = false;
    }

    public void StartMain()
    {

        mainCanvas = mainCanvas.GetComponent<Canvas>();
        slot1 = slot1.GetComponent<Image>();
        slot2 = slot2.GetComponent<Image>();
        slot3 = slot3.GetComponent<Image>();

        noneText1 = slot1.GetComponentInChildren<Text>();
        noneText2 = slot2.GetComponentInChildren<Text>();
        noneText3 = slot3.GetComponentInChildren<Text>();

        resourceText = resourceText.GetComponent<Text>();
        energyText = energyText.GetComponent<Text>();

        chooseCanvas = chooseCanvas.GetComponent<Canvas>();

        countText1 = countText1.GetComponent<Text>();
        countText2 = countText2.GetComponent<Text>();
        countText3 = countText3.GetComponent<Text>();

        playButton = playButton.GetComponent<UnityEngine.UI.Button>();
        
        originalSprite1 = slot1.sprite;
        originalSprite2 = slot2.sprite;
        originalSprite3 = slot3.sprite;
        
    }

    public void InitMain()
    {
        //check with playerdata and initialize accordingly
        resourceText.text = PlayerData.playerdata.totalResource.ToString();
        energyText.text = PlayerData.playerdata.totalEnergy.ToString();

        for(int i = 0; i < PlayerData.playerdata.troopSelected.Count; i++)
        {
            if(PlayerData.playerdata.troopSelected[i].troop.job != Enums.JobType.None)
            {
                if (i == 0)
                    noneText1.enabled = false;
                else if (i == 1)
                    noneText2.enabled = false;
                else
                    noneText3.enabled = false;

                switch (PlayerData.playerdata.troopSelected[i].troop.job)
                {
                    case Enums.JobType.Knight:
                        if (PlayerData.playerdata.pathogenType == Enums.CharacterType.Cancer)
                        {
                            if (i == 0)
                                slot1.sprite = cancerKnightSprite;
                            else if (i == 1)
                                slot2.sprite = cancerKnightSprite;
                            else
                                slot3.sprite = cancerKnightSprite;
                        }
                        else
                        {
                            if (i == 0)
                                slot1.sprite = diabeticKnightSprite;
                            else if (i == 1)
                                slot2.sprite = diabeticKnightSprite;
                            else
                                slot3.sprite = diabeticKnightSprite;
                        }
                        break;
                    case Enums.JobType.Archer:
                        if (PlayerData.playerdata.pathogenType == Enums.CharacterType.Cancer)
                        {
                            if (i == 0)
                                slot1.sprite = cancerArcherSprite;
                            else if (i == 1)
                                slot2.sprite = cancerArcherSprite;
                            else
                                slot3.sprite = cancerArcherSprite;
                        }
                        else
                        {
                            if (i == 0)
                                slot1.sprite = diabeticArcherSprite;
                            else if (i == 1)
                                slot2.sprite = diabeticArcherSprite;
                            else
                                slot3.sprite = diabeticArcherSprite;

                        }
                        break;
                    case Enums.JobType.Priest:
                        if (PlayerData.playerdata.pathogenType == Enums.CharacterType.Cancer)
                        {
                            if (i == 0)
                                slot1.sprite = cancerPriestSprite;
                            else if (i == 1)
                                slot2.sprite = cancerPriestSprite;
                            else
                                slot3.sprite = cancerPriestSprite;

                        }
                        else
                        {
                            if (i == 0)
                                slot1.sprite = diabeticPriestSprite;
                            else if (i == 1)
                                slot2.sprite = diabeticPriestSprite;
                            else
                                slot3.sprite = diabeticPriestSprite;
                        }
                        break;
                }
                if (i == 0)
                    countText1.text = PlayerData.playerdata.troopSelected[i].count.ToString();
                else if (i == 1)
                    countText2.text = PlayerData.playerdata.troopSelected[i].count.ToString();
                else if (i == 2)
                    countText3.text = PlayerData.playerdata.troopSelected[i].count.ToString();
            }
        }

        int charCount = 0;
        for(int i = 0; i < PlayerData.playerdata.troopSelected.Count; i++)
        {
            if (PlayerData.playerdata.troopSelected[i].troop.job != Enums.JobType.None)
                charCount++;
        }

        if (charCount == 0)
            playButton.interactable = false;
        else
            playButton.interactable = true;
    }

    public void Main_PlayPress()
    {
        for(int i = 0; i < PlayerData.playerdata.mapProgress.Count; i++)
        {
            switch (PlayerData.playerdata.mapProgress[i].mapName)
            {
                case Enums.MainMap.Mouth:

                    for(int j = 0; j < PlayerData.playerdata.mapProgress[i].stages.Count; j++)
                    {
                        if(!PlayerData.playerdata.mapProgress[i].stages[j].isComplete && PlayerData.playerdata.mapProgress[i].stages[j].isCurrent)
                        {
                            switch (PlayerData.playerdata.mapProgress[i].stages[j].mapId)
                            {
                                case 0:
                                    Application.LoadLevel("TutorialScene");
                                    break;

                                case 1:
                                    Application.LoadLevel("Tutorial2Scene");
                                    break;

                                case 2:
                                    Application.LoadLevel("Tutorial3Scene");
                                    break;
                            }
                        }
                    }
                    break;
            }
        }
    }

    public void Slot1_Click()
    {
        chooseCanvas.enabled = true;
        slotClicked = 1;
    }

    public void Slot2_Click()
    {
        chooseCanvas.enabled = true;
        slotClicked = 2;
    }

    public void Slot3_Click()
    {
        chooseCanvas.enabled = true;
        slotClicked = 3;
    }

    public void Slot1_Plus()
    {
        if (PlayerData.playerdata.troopSelected[0].troop.job != Enums.JobType.None)
        {
            if (PlayerData.playerdata.totalResource >= PlayerData.playerdata.troopSelected[0].troop.resourceNeeded && PlayerData.playerdata.totalResource != 0)
            {
                //allow summoning
                PlayerData.playerdata.totalResource -= PlayerData.playerdata.troopSelected[0].troop.resourceNeeded;
                PlayerData.playerdata.troopSelected[0].count++;
            }

            UpdateSelectedSlot(1);
        }
    }

    public void Slot2_Plus()
    {
        if (PlayerData.playerdata.troopSelected[1].troop.job != Enums.JobType.None)
        {
            if (PlayerData.playerdata.totalResource >= PlayerData.playerdata.troopSelected[1].troop.resourceNeeded && PlayerData.playerdata.totalResource != 0)
            {
                //allow summoning
                PlayerData.playerdata.totalResource -= PlayerData.playerdata.troopSelected[1].troop.resourceNeeded;
                PlayerData.playerdata.troopSelected[1].count++;
            }

            UpdateSelectedSlot(2);
        }
    }

    public void Slot3_Plus()
    {
        if(PlayerData.playerdata.troopSelected[2].troop.job != Enums.JobType.None)
        {
            if (PlayerData.playerdata.totalResource >= PlayerData.playerdata.troopSelected[2].troop.resourceNeeded && PlayerData.playerdata.totalResource != 0)
            {
                //allow summoning
                PlayerData.playerdata.totalResource -= PlayerData.playerdata.troopSelected[2].troop.resourceNeeded;
                PlayerData.playerdata.troopSelected[2].count++;
            }

            UpdateSelectedSlot(3);
        }
    }

    public void Slot1_Minus()
    {
        if(PlayerData.playerdata.troopSelected[0].troop.job != Enums.JobType.None && PlayerData.playerdata.troopSelected[0].count > 0)
        {
            PlayerData.playerdata.totalResource += PlayerData.playerdata.troopSelected[0].troop.resourceNeeded;
            PlayerData.playerdata.troopSelected[0].count--;

            if(PlayerData.playerdata.troopSelected[0].count == 0)
            {
                PlayerData.playerdata.troopSelected[0].troop = new PlayerData.Troop();
                slot1.sprite = originalSprite1;
                noneText1.enabled = true;
            }
            UpdateSelectedSlot(1);
        }
    }

    public void Slot2_Minus()
    {
        if (PlayerData.playerdata.troopSelected[1].troop.job != Enums.JobType.None && PlayerData.playerdata.troopSelected[1].count > 0)
        {
            PlayerData.playerdata.totalResource += PlayerData.playerdata.troopSelected[1].troop.resourceNeeded;
            PlayerData.playerdata.troopSelected[1].count--;

            if (PlayerData.playerdata.troopSelected[1].count == 0)
            {
                PlayerData.playerdata.troopSelected[1].troop = new PlayerData.Troop();
                slot2.sprite = originalSprite2;
                noneText2.enabled = true;
            }
            UpdateSelectedSlot(2);
        }
    }

    public void Slot3_Minus()
    {
        if (PlayerData.playerdata.troopSelected[2].troop.job != Enums.JobType.None && PlayerData.playerdata.troopSelected[2].count > 0)
        {
            PlayerData.playerdata.totalResource += PlayerData.playerdata.troopSelected[2].troop.resourceNeeded;
            PlayerData.playerdata.troopSelected[2].count--;

            if (PlayerData.playerdata.troopSelected[2].count == 0)
            {
                PlayerData.playerdata.troopSelected[2].troop = new PlayerData.Troop();
                slot3.sprite = originalSprite3;
                noneText3.enabled = true;
            }
            UpdateSelectedSlot(3);
        }
    }

    public void Management_BackPress()
    {
        Application.LoadLevel("MouthStage");
    }

    public void UpdateSelectedSlot(int SlotNumber)
    {
        if(SlotNumber == 1)
        {
            if (PlayerData.playerdata.pathogenType == Enums.CharacterType.Cancer)
            {
                switch (PlayerData.playerdata.troopSelected[0].troop.job)
                {
                    case Enums.JobType.Knight:
                        slot1.sprite = cancerKnightSprite;
                        break;
                    case Enums.JobType.Archer:
                        slot1.sprite = cancerArcherSprite;
                        break;
                    case Enums.JobType.Priest:
                        slot1.sprite = cancerPriestSprite;
                        break;
                }
            }
            else
            {
                switch (PlayerData.playerdata.troopSelected[0].troop.job)
                {
                    case Enums.JobType.Knight:
                        slot1.sprite = diabeticKnightSprite;
                        break;
                    case Enums.JobType.Archer:
                        slot1.sprite = diabeticArcherSprite;
                        break;
                    case Enums.JobType.Priest:
                        slot1.sprite = diabeticPriestSprite;
                        break;
                }
            }
            countText1.text = PlayerData.playerdata.troopSelected[0].count.ToString();
        }
        else if(SlotNumber == 2)
        {
            if (PlayerData.playerdata.pathogenType == Enums.CharacterType.Cancer)
            {
                switch (PlayerData.playerdata.troopSelected[1].troop.job)
                {
                    case Enums.JobType.Knight:
                        slot2.sprite = cancerKnightSprite;
                        break;
                    case Enums.JobType.Archer:
                        slot2.sprite = cancerArcherSprite;
                        break;
                    case Enums.JobType.Priest:
                        slot2.sprite = cancerPriestSprite;
                        break;
                }
            }
            else
            {
                switch (PlayerData.playerdata.troopSelected[1].troop.job)
                {
                    case Enums.JobType.Knight:
                        slot2.sprite = diabeticKnightSprite;
                        break;
                    case Enums.JobType.Archer:
                        slot2.sprite = diabeticArcherSprite;
                        break;
                    case Enums.JobType.Priest:
                        slot2.sprite = diabeticPriestSprite;
                        break;
                }
            }
            countText2.text = PlayerData.playerdata.troopSelected[1].count.ToString();
        }
        else
        {
            if (PlayerData.playerdata.pathogenType == Enums.CharacterType.Cancer)
            {
                switch (PlayerData.playerdata.troopSelected[2].troop.job)
                {
                    case Enums.JobType.Knight:
                        slot3.sprite = cancerKnightSprite;
                        break;
                    case Enums.JobType.Archer:
                        slot3.sprite = cancerArcherSprite;
                        break;
                    case Enums.JobType.Priest:
                        slot3.sprite = cancerPriestSprite;
                        break;
                }
            }
            else
            {
                switch (PlayerData.playerdata.troopSelected[2].troop.job)
                {
                    case Enums.JobType.Knight:
                        slot3.sprite = diabeticKnightSprite;
                        break;
                    case Enums.JobType.Archer:
                        slot3.sprite = diabeticArcherSprite;
                        break;
                    case Enums.JobType.Priest:
                        slot3.sprite = diabeticPriestSprite;
                        break;
                }
            }
            countText3.text = PlayerData.playerdata.troopSelected[2].count.ToString();
        }
    }

    /*********************************Choose Canvas**************************************/
    
    public void StartChoose()
    {
        knightLevel = knightLevel.GetComponent<Text>();
        archerLevel = archerLevel.GetComponent<Text>();
        priestLevel = priestLevel.GetComponent<Text>();

        knightAttack = knightAttack.GetComponent<Text>();
        archerAttack = archerAttack.GetComponent<Text>();
        priestAttack = priestAttack.GetComponent<Text>();

        knightDefense = knightDefense.GetComponent<Text>();
        archerDefense = archerDefense.GetComponent<Text>();
        priestDefense = priestDefense.GetComponent<Text>();

        knightResource = knightResource.GetComponent<Text>();
        archerResource = archerResource.GetComponent<Text>();
        priestResource = priestResource.GetComponent<Text>();

        chooseKnightSprite = chooseKnightSprite.GetComponent<Image>();
        chooseArcherSprite = chooseArcherSprite.GetComponent<Image>();
        choosePriestSprite = choosePriestSprite.GetComponent<Image>();

        knightLeader = knightLeader.GetComponent<UnityEngine.UI.Button>();
        archerLeader = archerLeader.GetComponent<UnityEngine.UI.Button>();
        priestLeader = priestLeader.GetComponent<UnityEngine.UI.Button>();

    }

    public void InitChoose()
    {
        UpdateLeaderButton();

        //reflect all troop data
        if (PlayerData.playerdata.pathogenType == Enums.CharacterType.Cancer)
        {
            //set first slot
            chooseKnightSprite.sprite = cancerKnightSprite;
            if (PlayerData.playerdata.troopData[0].level > 0)
                chooseKnightSprite.GetComponentInChildren<Text>().enabled = false;
            else
                chooseKnightSprite.GetComponentInChildren<Text>().enabled = true;

            knightLevel.text = PlayerData.playerdata.troopData[0].level.ToString();
            knightAttack.text = PlayerData.playerdata.troopData[0].attack.ToString();
            knightDefense.text = PlayerData.playerdata.troopData[0].defenseRating.ToString();
            knightResource.text = PlayerData.playerdata.troopData[0].resourceNeeded.ToString();

            //set second slot
            chooseArcherSprite.sprite = cancerArcherSprite;
            if (PlayerData.playerdata.troopData[1].level > 0)
                chooseArcherSprite.GetComponentInChildren<Text>().enabled = false;
            else
                chooseArcherSprite.GetComponentInChildren<Text>().enabled = true;

            archerLevel.text = PlayerData.playerdata.troopData[1].level.ToString();
            archerAttack.text = PlayerData.playerdata.troopData[1].attack.ToString();
            archerDefense.text = PlayerData.playerdata.troopData[1].defenseRating.ToString();
            archerResource.text = PlayerData.playerdata.troopData[1].resourceNeeded.ToString();

            //set third slot
            choosePriestSprite.sprite = cancerPriestSprite;
            if (PlayerData.playerdata.troopData[2].level > 0)
                choosePriestSprite.GetComponentInChildren<Text>().enabled = false;
            else
                choosePriestSprite.GetComponentInChildren<Text>().enabled = true;

            priestLevel.text = PlayerData.playerdata.troopData[2].level.ToString();
            priestAttack.text = PlayerData.playerdata.troopData[2].attack.ToString();
            priestDefense.text = PlayerData.playerdata.troopData[2].defenseRating.ToString();
            priestResource.text = PlayerData.playerdata.troopData[2].resourceNeeded.ToString();
        }
        else
        {
            //set first slot
            chooseKnightSprite.sprite = diabeticKnightSprite;
            if (PlayerData.playerdata.troopData[0].level > 0)
                chooseKnightSprite.GetComponentInChildren<Text>().enabled = false;
            else
                chooseKnightSprite.GetComponentInChildren<Text>().enabled = true;

            knightLevel.text = PlayerData.playerdata.troopData[0].level.ToString();
            knightAttack.text = PlayerData.playerdata.troopData[0].attack.ToString();
            knightDefense.text = PlayerData.playerdata.troopData[0].defenseRating.ToString();
            knightResource.text = PlayerData.playerdata.troopData[0].resourceNeeded.ToString();

            //set second slot
            chooseArcherSprite.sprite = diabeticArcherSprite;
            if (PlayerData.playerdata.troopData[1].level > 0)
                chooseArcherSprite.GetComponentInChildren<Text>().enabled = false;
            else
                chooseArcherSprite.GetComponentInChildren<Text>().enabled = true;

            archerLevel.text = PlayerData.playerdata.troopData[1].level.ToString();
            archerAttack.text = PlayerData.playerdata.troopData[1].attack.ToString();
            archerDefense.text = PlayerData.playerdata.troopData[1].defenseRating.ToString();
            archerResource.text = PlayerData.playerdata.troopData[1].resourceNeeded.ToString();

            //set third slot
            choosePriestSprite.sprite = diabeticPriestSprite;
            if (PlayerData.playerdata.troopData[2].level > 0)
                choosePriestSprite.GetComponentInChildren<Text>().enabled = false;
            else
                choosePriestSprite.GetComponentInChildren<Text>().enabled = true;

            priestLevel.text = PlayerData.playerdata.troopData[2].level.ToString();
            priestAttack.text = PlayerData.playerdata.troopData[2].attack.ToString();
            priestDefense.text = PlayerData.playerdata.troopData[2].defenseRating.ToString();
            priestResource.text = PlayerData.playerdata.troopData[2].resourceNeeded.ToString();
        }
    }

    public void ChooseCanvas_BackPress()
    {
        slotClicked = 0;
        InitMain();
        chooseCanvas.enabled = false;
    }

    public void Choose_KnightLeader()
    {
        PlayerData.playerdata.leaderType = Enums.JobType.Knight;
        UpdateLeaderButton();
    }
    public void Choose_ArcherLeader()
    {
        PlayerData.playerdata.leaderType = Enums.JobType.Archer;
        UpdateLeaderButton();
    }
    public void Choose_PriestLeader()
    {
        PlayerData.playerdata.leaderType = Enums.JobType.Priest;
        UpdateLeaderButton();
    }

    public void UpdateLeaderButton()
    {
        switch (PlayerData.playerdata.leaderType)
        {
            case Enums.JobType.Knight:
                knightLeader.interactable = false;
                archerLeader.interactable = true;
                priestLeader.interactable = true;
                break;

            case Enums.JobType.Archer:
                knightLeader.interactable = true;
                archerLeader.interactable = false;
                priestLeader.interactable = true;
                break;

            case Enums.JobType.Priest:
                knightLeader.interactable = true;
                archerLeader.interactable = true;
                priestLeader.interactable = false;
                break;
            default:
                knightLeader.interactable = true;
                archerLeader.interactable = true;
                priestLeader.interactable = true;
                break;
        }
    }
    public void Choose_KnightPress()
    {
        bool allowed = true;
        for(int i = 0; i < PlayerData.playerdata.troopSelected.Count; i++)
        {
            if(PlayerData.playerdata.troopSelected[i].troop.job == Enums.JobType.Knight)
            {
                allowed = false;
                break;
            }
        }
        if (PlayerData.playerdata.troopSelected[slotClicked - 1].troop.job != Enums.JobType.Knight && PlayerData.playerdata.troopData[0].level > 0 && allowed)
        {
            PlayerData.playerdata.totalResource += PlayerData.playerdata.troopSelected[slotClicked - 1].troop.resourceNeeded * PlayerData.playerdata.troopSelected.Count;
            PlayerData.playerdata.troopSelected[slotClicked - 1].count = 0;
            PlayerData.playerdata.troopSelected[slotClicked - 1].troop = PlayerData.playerdata.troopData[0];


            slotClicked = 0;
            InitMain();
            chooseCanvas.enabled = false;
        }
    }

    public void Choose_ArcherPress()
    {
        bool allowed = true;

        for (int i = 0; i < PlayerData.playerdata.troopSelected.Count; i++)
        {
            if (PlayerData.playerdata.troopSelected[i].troop.job == Enums.JobType.Archer)
            {
                allowed = false;
                break;
            }
        }

        if (PlayerData.playerdata.troopSelected[slotClicked - 1].troop.job != Enums.JobType.Archer && PlayerData.playerdata.troopData[1].level > 0 && allowed)
        {
            PlayerData.playerdata.totalResource += PlayerData.playerdata.troopSelected[slotClicked - 1].troop.resourceNeeded * PlayerData.playerdata.troopSelected.Count;
            PlayerData.playerdata.troopSelected[slotClicked - 1].count = 0;
            PlayerData.playerdata.troopSelected[slotClicked - 1].troop = PlayerData.playerdata.troopData[1];

            slotClicked = 0;
            InitMain();
            chooseCanvas.enabled = false;
        }
    }

    public void Choose_PriestPress()
    {
        bool allowed = true;

        for (int i = 0; i < PlayerData.playerdata.troopSelected.Count; i++)
        {
            if (PlayerData.playerdata.troopSelected[i].troop.job == Enums.JobType.Priest)
            {
                allowed = false;
                break;
            }
        }

        if (PlayerData.playerdata.troopSelected[slotClicked - 1].troop.job != Enums.JobType.Priest && PlayerData.playerdata.troopData[2].level > 0 && allowed)
        {
            PlayerData.playerdata.totalResource += PlayerData.playerdata.troopSelected[slotClicked - 1].troop.resourceNeeded * PlayerData.playerdata.troopSelected.Count;
            PlayerData.playerdata.troopSelected[slotClicked - 1].count = 0;
            PlayerData.playerdata.troopSelected[slotClicked - 1].troop = PlayerData.playerdata.troopData[2];


            slotClicked = 0;
            InitMain();
            chooseCanvas.enabled = false;
        }
    }
}
