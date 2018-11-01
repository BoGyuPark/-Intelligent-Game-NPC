using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class TestShakespeare_changing : MonoBehaviour
{
    //Monster 구조체 선언
    public struct MonsterInfo
    {
        public int Mnum;
        public string Mname;
        public double Mhp;
        public double Marmor;
        public double Mdps;

        public MonsterInfo(int Mnum, string Mname, double Mhp, double Marmor, double Mdps)
        {
            this.Mnum = Mnum;
            this.Mname = Mname;
            this.Mhp = Mhp;
            this.Marmor = Marmor;
            this.Mdps = Mdps;  
        }
    }

    //Player 구조체 선언
    public struct PlayerInfo
    {
        public double Php;
        public double Parmor;
        public double Pdps;

    }

    int populationSize = 100;
    float mutationRate = 0.015f;
    int elitism = 50;

    //[Header("Genetic Algorithm")]
    //[SerializeField] string targetString = "To be, or not to be, that is the question.";
    //[SerializeField] string validCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ,.|!#$%&/()=? ";

    ////변경<----------------------------------------------->
    //[SerializeField] int populationSize = 100;
    //[SerializeField] float mutationRate = 0.015f;
    //[SerializeField] int elitism = 50;
    ////<----------------------------------------------->

    //[Header("Other")]
    //[SerializeField] int numCharsPerText = 15000;

    //[SerializeField] Text targetText;
    //[SerializeField] Text bestText;
    //[SerializeField] Text bestFitnessText;
    //[SerializeField] Text numGenerationsText;

    //[SerializeField] Transform populationTextParent;
    //[SerializeField] Text textPrefab;

    private GeneticAlgorithm<int> ga;
    private System.Random random;
    public List<MonsterInfo> Mlist;

    void Start()
    {
        //targetText.text = targetString;

        //if (string.IsNullOrEmpty(targetString))
        //{
        //    Debug.LogError("Target string is null or empty");
        //    this.enabled = false;
        //}

        

        //<------------------------------몬스터 정보---------------------------------------->
        {
            //Monster list 생성
            Mlist = new List<MonsterInfo>();
            //0 ~ 9
            Mlist.Add(new MonsterInfo(0, "Peasant", 220, 0, 11.00));
            Mlist.Add(new MonsterInfo(1, "Peon", 250, 0, 22.50));
            Mlist.Add(new MonsterInfo(2, "Acolyte", 220, 0, 23.75));
            Mlist.Add(new MonsterInfo(3, "Flying Machine", 200, 0.08, 24.00));
            Mlist.Add(new MonsterInfo(4, "Priest", 370, 0, 17.00));
            Mlist.Add(new MonsterInfo(5, "Shaman", 415, 0, 17.85));
            Mlist.Add(new MonsterInfo(6, "Necromancer", 385, 0, 15.30));
            Mlist.Add(new MonsterInfo(7, "Skeleton Warrior", 180, 0.07, 38.00));
            Mlist.Add(new MonsterInfo(8, "Obsidian Statue", 550, 0.04, 15.75));
            Mlist.Add(new MonsterInfo(9, "Spirit Wolf", 200, 0, 11.50));
            //10 ~ 19
            Mlist.Add(new MonsterInfo(10, "Sorceress", 405, 0, 19.25));
            Mlist.Add(new MonsterInfo(11, "Skeletal Mage", 230, 0.06, 24.00));
            Mlist.Add(new MonsterInfo(12, "Banshee", 365, 0, 16.50));
            Mlist.Add(new MonsterInfo(13, "Witch Doctor", 395, 0.08, 21.00));
            Mlist.Add(new MonsterInfo(14, "Dot Druid Form", 380, 0, 19.20));
            Mlist.Add(new MonsterInfo(15, "Treant", 300, 0.05, 28.00));
            Mlist.Add(new MonsterInfo(16, "Militia", 220, 0.1, 20.40));
            Mlist.Add(new MonsterInfo(17, "Troll Batrider", 325, 0.06, 36.00));
            Mlist.Add(new MonsterInfo(18, "Serpent Ward", 135, 0, 64.50));
            Mlist.Add(new MonsterInfo(19, "Archer", 245, 0.06, 39.00));
            //20 ~ 29
            Mlist.Add(new MonsterInfo(20, "Dire Wolf", 300, 0, 16.50));
            Mlist.Add(new MonsterInfo(21, "T. Headhunter", 350, 0.06, 78.54));
            Mlist.Add(new MonsterInfo(22, "Dryad", 435, 0.06, 48.00));
            Mlist.Add(new MonsterInfo(23, "Footman", 420, 0.08, 22.95));
            Mlist.Add(new MonsterInfo(24, "Faerie Dragon", 450, 0.06, 36.75));
            Mlist.Add(new MonsterInfo(25, "Ghoul", 340, 0.06, 18.20));
            Mlist.Add(new MonsterInfo(26, "Glaive Thrower", 300, 0.02, 255.50));
            Mlist.Add(new MonsterInfo(27, "Carrion Beetle", 410, 0.02, 36.75));
            Mlist.Add(new MonsterInfo(28, "Spell Breaker", 600, 0.09, 38.00));
            Mlist.Add(new MonsterInfo(29, "Spirit Walker", 620, 0, 34.13));

            //30 ~ 39
            Mlist.Add(new MonsterInfo(30, "T. Berserker", 450, 0.06, 78.54));
            Mlist.Add(new MonsterInfo(31, "Huntress", 600, 0.08, 41.40));
            Mlist.Add(new MonsterInfo(32, "Mortar Team", 360, 0.06, 276.50));
            Mlist.Add(new MonsterInfo(33, "Gargoyle", 410, 0.09, 37.80));
            Mlist.Add(new MonsterInfo(34, "DoC Druid Form", 580, 0.01, 38.25));
            Mlist.Add(new MonsterInfo(35, "Meat Wagon", 380, 0.02, 432.00));
            Mlist.Add(new MonsterInfo(36, "Shadow Wolf", 500, 0, 21.50));
            Mlist.Add(new MonsterInfo(37, "Rifleman", 535, 0.06, 42.75));
            Mlist.Add(new MonsterInfo(38, "DragonHawk Rider", 725, 0.07, 43.75));
            Mlist.Add(new MonsterInfo(39, "Demolisher", 425, 0.08, 490.50));
            //40 ~ 49
            Mlist.Add(new MonsterInfo(40, "Crypt Fiend", 550, 0.06, 78.00));
            Mlist.Add(new MonsterInfo(41, "Raider", 610, 0.07, 62.90));
            Mlist.Add(new MonsterInfo(42, "Dodo Beast", 1000, 0.01, 25.92));
            Mlist.Add(new MonsterInfo(43, "Wind Rider", 570, 0.06, 98.00));
            Mlist.Add(new MonsterInfo(44, "Grunt", 800, 0.07, 48.00));
            Mlist.Add(new MonsterInfo(45, "Destroyer", 900, 0.09, 35.10));
            Mlist.Add(new MonsterInfo(46, "Hippogryph Rider", 765, 0.07, 28.60));
            Mlist.Add(new MonsterInfo(47, "Water Elemental", 900, 0.02, 67.50));
            Mlist.Add(new MonsterInfo(48, "Avatar of Vengeance", 1200, 0.02, 41.18));
            Mlist.Add(new MonsterInfo(49, "Siege Engine", 700, 0.08, 184.80));
            //50 ~ 59
            Mlist.Add(new MonsterInfo(50, "Abomination", 1175, 0.08, 91.20));
            Mlist.Add(new MonsterInfo(51, "DocC Bear Form", 960, 0.09, 70.50));
            Mlist.Add(new MonsterInfo(52, "Tauren", 1300, 0.09, 85.50));
            Mlist.Add(new MonsterInfo(53, "Knight", 985, 0.11, 60.20));
            Mlist.Add(new MonsterInfo(54, "Mountain giant", 1600, 0.10, 136.25));
            Mlist.Add(new MonsterInfo(55, "Gryphon Rider", 975, 0.06, 193.60));
            Mlist.Add(new MonsterInfo(56, "Chimaera", 1000, 0.08, 255.00));
            Mlist.Add(new MonsterInfo(57, "Frost Wyrm", 1350, 0.07, 370.50));
            Mlist.Add(new MonsterInfo(58, "Phoenix", 1250, 0.01, 95.20));
            Mlist.Add(new MonsterInfo(59, "Infernal", 1500, 0.06, 73.58));
        }


        //초기화
        random = new System.Random();
        //GA는 char형으로, dnasize는 targetString.Length로, Func<T> getRandomGene은 GetRandomCharacter함수로
        //FitnessFunction 함수로 적합도 계산
        ga = new GeneticAlgorithm<int>(populationSize, 8, random, GetRandomMonster, FitnessFunction, elitism, mutationRate);
    }

    //스크립트가 켜져 있을 때(enabled 상태일 때) 매 프레임마다 호출
    void Update()
    {
        ga.NewGeneration();

        //UpdateText(ga.BestGenes, ga.BestFitness, ga.Generation, ga.Population.Count, (j) => ga.Population[j].Genes);

        //모든 문자를 맞췄을 경우
        if (ga.BestFitness == 1)
        {
            this.enabled = false;
        }
    }

    //랜덤 문자(유전자) 반환
    private int GetRandomMonster()
    {
        //랜덤 값 (0 ~ 59 값까지)
        int i = random.Next(0, 60);
        //인덱스에 해당하는 값의 문자 반환
        return i;
    }

    //적합도 함수 계산
    private double FitnessFunction(int index)
    {
        double score = 0;
        DNA<int> dna = ga.Population[index];

        //<----------------------플레이어 정보------------------------------------>
        PlayerInfo player = new PlayerInfo();
        player.Php = 4000;
        player.Parmor = 0.5;
        player.Pdps = 200;

        //여기부터 수정
        for (int i = 0; i < dna.Genes.Length; i++)
        {
            //dna에서 i번째에 있는 몬스터 정보(n)를 가져온다.
            int n = dna.Genes[i];
            MonsterInfo mon = Mlist[n];

            while (player.Php > 0)
            {
                mon.Mhp -= player.Pdps * (1 - mon.Marmor);
                if (mon.Mhp <= 0)
                    break;
                player.Php -= mon.Mdps * (1 - player.Parmor);
            }
        }

        //플레이어의 남은 체력
        score = player.Php;

        return score;
    }


    //private int numCharsPerTextObj;
    //private List<Text> textList = new List<Text>();


    //Awake 함수는 언제나 Start 함수 전에 호출됩니다
    //Awake 함수는 스크립트 객체가 로딩될 때 호출됩니다.
    //Awake 함수는 게임이 시작하기 전에 변수나 게임 상태를 초기화하기 위해 사용합니다.
    //Awake 함수는 스크립트 객체의 라이프타임 동안 단 한번만 호출됩니다.
    //void Awake()
    //{
    //    numCharsPerTextObj = numCharsPerText / validCharacters.Length;
    //    if (numCharsPerTextObj > populationSize) numCharsPerTextObj = populationSize;

    //    int numTextObjects = Mathf.CeilToInt((float)populationSize / numCharsPerTextObj);

    //    for (int i = 0; i < numTextObjects; i++)
    //    {
    //        textList.Add(Instantiate(textPrefab, populationTextParent));
    //    }
    //}

    //private void UpdateText(char[] bestGenes, float bestFitness, int generation, int populationSize, Func<int, char[]> getGenes)
    //{
    //    bestText.text = CharArrayToString(bestGenes);
    //    bestFitnessText.text = bestFitness.ToString();

    //    numGenerationsText.text = generation.ToString();


    //    // 오른쪽화면에 bestGene을 제외한 나머지 DNA들의 값을 보여준다.
    //    for (int i = 0; i < textList.Count; i++)
    //    {
    //        var sb = new StringBuilder();
    //        int endIndex = i == textList.Count - 1 ? populationSize : (i + 1) * numCharsPerTextObj;
    //        for (int j = i * numCharsPerTextObj; j < endIndex; j++)
    //        {
    //            foreach (var c in getGenes(j))
    //            {
    //                sb.Append(c);
    //            }
    //            if (j < endIndex - 1) sb.AppendLine();
    //        }

    //        textList[i].text = sb.ToString();
    //    }
    //}

    //private string CharArrayToString(char[] charArray)
    //{
    //    var sb = new StringBuilder();
    //    foreach (var c in charArray)
    //    {
    //        sb.Append(c);
    //    }

    //    return sb.ToString();
    //}
}