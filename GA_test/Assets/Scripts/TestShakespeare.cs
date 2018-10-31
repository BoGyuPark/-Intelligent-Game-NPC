using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class TestShakespeare : MonoBehaviour
{
    [Header("Genetic Algorithm")]
    [SerializeField] string targetString = "To be, or not to be, that is the question.";
    [SerializeField] string validCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ,.|!#$%&/()=? ";
    [SerializeField] int populationSize = 200;
    [SerializeField] float mutationRate = 0.01f;
    [SerializeField] int elitism = 5;

    [Header("Other")]
    [SerializeField] int numCharsPerText = 15000;

    [SerializeField] Text targetText;
    [SerializeField] Text bestText;
    [SerializeField] Text bestFitnessText;
    [SerializeField] Text numGenerationsText;

    [SerializeField] Transform populationTextParent;
    [SerializeField] Text textPrefab;

    private GeneticAlgorithm<char> ga;
    private System.Random random;

    void Start()
    {
        targetText.text = targetString;

        if (string.IsNullOrEmpty(targetString))
        {
            Debug.LogError("Target string is null or empty");
            this.enabled = false;
        }

        //초기화
        random = new System.Random();
        //GA는 char형으로, dnasize는 targetString.Length로, Func<T> getRandomGene은 GetRandomCharacter함수로
        //FitnessFunction 함수로 적합도 계산
        ga = new GeneticAlgorithm<char>(populationSize, targetString.Length, random, GetRandomCharacter, FitnessFunction, elitism, mutationRate);
    }

    //스크립트가 켜져 있을 때(enabled 상태일 때) 매 프레임마다 호출
    void Update()
    {
        ga.NewGeneration();

        UpdateText(ga.BestGenes, ga.BestFitness, ga.Generation, ga.Population.Count, (j) => ga.Population[j].Genes);

        //모든 문자를 맞췄을 경우
        if (ga.BestFitness == 1)
        {
            this.enabled = false;
        }
    }

    //랜덤 문자(유전자) 반환
    private char GetRandomCharacter()
    {
        //유효 문자 길이의 숫자 중 랜덤 값 (0 ~ len -1 값까지)
        int i = random.Next(validCharacters.Length);
        //인덱스에 해당하는 값의 문자 반환
        return validCharacters[i];
    }

    //적합도 함수 계산
    private float FitnessFunction(int index)
    {
        float score = 0;
        DNA<char> dna = ga.Population[index];

        for (int i = 0; i < dna.Genes.Length; i++)
        {
            //문자를 맞춘 갯수만큼 score 1증가.
            if (dna.Genes[i] == targetString[i])
            {
                score += 1;
            }
        }
        //퍼센트 비율로 환산
        score /= targetString.Length;

        //????????????????????이 값은 잘모르겠음
        score = (Mathf.Pow(2, score) - 1) / (2 - 1);

        return score;
    }


    private int numCharsPerTextObj;
    private List<Text> textList = new List<Text>();


    //Awake 함수는 언제나 Start 함수 전에 호출됩니다
    //Awake 함수는 스크립트 객체가 로딩될 때 호출됩니다.
    //Awake 함수는 게임이 시작하기 전에 변수나 게임 상태를 초기화하기 위해 사용합니다.
    //Awake 함수는 스크립트 객체의 라이프타임 동안 단 한번만 호출됩니다.
    void Awake()
    {
        numCharsPerTextObj = numCharsPerText / validCharacters.Length;
        if (numCharsPerTextObj > populationSize) numCharsPerTextObj = populationSize;

        int numTextObjects = Mathf.CeilToInt((float)populationSize / numCharsPerTextObj);

        for (int i = 0; i < numTextObjects; i++)
        {
            textList.Add(Instantiate(textPrefab, populationTextParent));
        }
    }

    private void UpdateText(char[] bestGenes, float bestFitness, int generation, int populationSize, Func<int, char[]> getGenes)
    {
        bestText.text = CharArrayToString(bestGenes);
        bestFitnessText.text = bestFitness.ToString();

        numGenerationsText.text = generation.ToString();


        // 오른쪽화면에 bestGene을 제외한 나머지 DNA들의 값을 보여준다.
        for (int i = 0; i < textList.Count; i++)
        {
            var sb = new StringBuilder();
            int endIndex = i == textList.Count - 1 ? populationSize : (i + 1) * numCharsPerTextObj;
            for (int j = i * numCharsPerTextObj; j < endIndex; j++)
            {
                foreach (var c in getGenes(j))
                {
                    sb.Append(c);
                }
                if (j < endIndex - 1) sb.AppendLine();
            }

            textList[i].text = sb.ToString();
        }
    }

    private string CharArrayToString(char[] charArray)
    {
        var sb = new StringBuilder();
        foreach (var c in charArray)
        {
            sb.Append(c);
        }

        return sb.ToString();
    }
}