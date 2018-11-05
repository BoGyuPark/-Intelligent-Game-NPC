﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerControl : MonoBehaviour {

    public GameObject monster;
    int randomNum;
    public string InputMonName;
    public double InputMonHp;
    public double InputMonArmor;
    public double InputMonDps;

    public static int NumOfMon;
    public static bool numCheck = false;

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
    public List<MonsterInfo> Mlist;

    int populationSize = 4;
    float mutationRate = 0.015f;
    int elitism = 2;
    public static int dnaSize = 4;

    private GeneticAlgorithm<int> ga;
    private System.Random random;

    public SpawnerControl()
    {
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
        ga = new GeneticAlgorithm<int>(populationSize, dnaSize, random, GetRandomMonster, FitnessFunction, elitism, mutationRate);
        

    }

    // Use this for initialization
    void Start () {

        Vector3 pos = new Vector3(transform.position.x + Random.Range(-15.0f, 15.0f), transform.position.y, +transform.position.z + Random.Range(-15.0f, 15.0f));
        // 첫 세대의 첫번째 던전의 첫번째 몬스터의 번호
        randomNum = ga.Population[0].Genes[0];
        //Instantiate(monster[randomNum], pos, transform.rotation);
        //monster[0].GetComponent<MonstersControl>().SetParameter("Acolyte", 220, 0, 23.75);

        //MonsterInfo mon = Mlist[randomNum];
        MonsterInfo mon = Mlist[randomNum];
        InputMonName = mon.Mname;
        InputMonHp = mon.Mhp;
        InputMonArmor = mon.Marmor;
        InputMonDps = mon.Mdps;
        monster.GetComponent<MonstersControl>().SetParameter(InputMonName, InputMonHp, InputMonArmor, InputMonDps);

        //Debug.Log("start monster num : " + randomNum);
        Instantiate(monster, pos, transform.rotation);

        //for (int i = 0; i < 4; i++)
        //{
        //    for (int j = 0; j < 4; j++)
        //    {
        //        Debug.Log(i + "번째 던전" + ga.Population[i].Genes[j]);
        //    }
        //}

    }

    // Update is called once per frame
    void Update() {


        //남은 hp가 원래 hp체력의 1 / 10이하 인 경우 목표에 만족하게 된다.
        if (ga.BestFitness < PlayerControl.HP * 0.1)
        {
            //종료
            Application.Quit();
            //Console.WriteLine();
            //Console.WriteLine("DNA size : " + dnaSize);
            //Console.WriteLine(ga.Generation - 1 + " Generation The End");
            //Console.WriteLine("HP After Battle : " + ga.BestFitness);
            //Console.Write("Genes of Best Dungeon : ");
            //for (int i = 0; i < dnaSize; i++)
            //{
            //    //Console.Write(ga.BestGenes[i] + " ");
            //    //Console.Write(Mlist[(ga.BestGenes[i])].Mname);
            //}
            //Console.WriteLine();
            //Console.WriteLine("The number of Monster ");
            //for (int j = 0; j < 60; j++)
            //{
            //    Console.WriteLine(j + "= " + ga.monsterCount[j] + "  ");
            //}
            //Console.WriteLine();

            //break;
        }

        //하나의 DNA에 있는 모든 몬스터와 전투를 함
        //이 때 fitness 계산을 해야한다.
        if (NumOfMon != 0 && NumOfMon % dnaSize == 0 && numCheck == true)
        {
            if(MonstersControl.deadHp < 0)
            {
                ga.Population[(NumOfMon / dnaSize) - 1].Fitness = MonstersControl.deadHp + 3000 * 10;
            }
            else
            {
                ga.Population[(NumOfMon / dnaSize) - 1].Fitness = PlayerControl.HP;
            }
            ga.fitnessSum += PlayerControl.HP;
            numCheck = false;

            Debug.Log(ga.Generation + "세대의 " + ((NumOfMon / dnaSize) - 1) + "번째 던전의 적합도수치" + ga.Population[(NumOfMon / dnaSize) - 1].Fitness);
            //한 던전이 끝났으므로 HP 리셋
            PlayerControl.HP = 3000;

            //모든 던전이 전투 종료시
            if (NumOfMon / dnaSize == ga.Population.Count)
            {
                ga.NewGeneration();
                Debug.Log(ga.Generation + "generation");
                //1. 남은 체력순으로 sort
                //2. newPopulation.Clear()
                //3. Elitism 기준으로 새로운 세대에 삽입 나머진 DNA 교배 및 변이로 삽입
                //4. swap

                //초기화
                NumOfMon = 0;
                ga.fitnessSum = 0;

                //for (int i = 0; i < 4; i++)
                //{
                //    for (int j = 0; j < 4; j++)
                //    {
                //        Debug.Log(i + "번째 던전" + ga.Population[i].Genes[j]);
                //    }
                //}


            }

        }

        
        //몬스터 생성
        if (MonstersControl.flagnum == true)
        {
            Vector3 pos = new Vector3(transform.position.x + Random.Range(-15.0f, 15.0f), transform.position.y, +transform.position.z + Random.Range(-15.0f, 15.0f));
            //각 던전의 몬스터

            randomNum = ga.Population[NumOfMon / dnaSize].Genes[NumOfMon % dnaSize];
            //Instantiate(monster[randomNum], pos, transform.rotation);
            //monster[0].GetComponent<MonstersControl>().SetParameter("Acolyte", 220, 0, 23.75);

            //MonsterInfo mon = Mlist[randomNum];
            MonsterInfo mon = Mlist[randomNum];
            InputMonName = mon.Mname;
            InputMonHp = mon.Mhp;
            InputMonArmor = mon.Marmor;
            InputMonDps = mon.Mdps;
            monster.GetComponent<MonstersControl>().SetParameter(InputMonName, InputMonHp, InputMonArmor, InputMonDps);

            //Debug.Log("new monster num : " + randomNum);
            Instantiate(monster, pos, transform.rotation);

            MonstersControl.flagnum = false;
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
        PlayerControl.HP = 3000;
        PlayerControl.ARMOR = 0.8;
        PlayerControl.DPS = 205;

        double originalPlayerHP = PlayerControl.HP;

        //Console.Write(index + " dungeon  : ");

        for (int i = 0; i < dna.Genes.Length; i++)
        {
            //dna에서 i번째에 있는 몬스터 정보(n)를 가져온다.
            int n = dna.Genes[i];

            //해당몬스터 갯수 증가
            ga.monsterCount[n]++;

            MonsterInfo mon = Mlist[n];
            //Console.Write(mon.Mnum + " ");

            

            
            //몬스터를 생성했으니까 Player와 전투를 해야한다.


            //while (PlayerControl.HP > 0)
            //{
            //    mon.Mhp -= PlayerControl.DPS * (1 - mon.Marmor);
            //    if (mon.Mhp <= 0)
            //        break;
            //    PlayerControl.HP -= mon.Mdps * (1 - PlayerControl.ARMOR);

            //    //플레이어가 죽은 경우, 남은 hp가 음수 이므로 원래 player의 HP의 2배 값과 더하여 우선순위를 뒤로 미룬다.
            //    if (PlayerControl.HP <= 0)
            //    {
            //       // Console.WriteLine("player die");
            //        return (originalPlayerHP * 2) + PlayerControl.HP;
            //    }
            //}



        }
        //Console.WriteLine();
        //플레이어의 남은 체력
        score = PlayerControl.HP;
        return score;
    }


}
