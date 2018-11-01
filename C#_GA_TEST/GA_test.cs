using System;
using System.Collections.Generic;

public class GeneticAlgorithm<T>
{
    //Population이라는 염색체 배열을 생성, Population을 100으로 할 예정, 즉 100개의 던전
    public List<DNA<T>> Population { get; private set; }

    //세대 값
    public int Generation { get; private set; }

    //best염색체의 적합성 수치
    public double BestFitness { get; private set; }

    //적합성 함수 중에 가장 높은 적합도를 갖는 염색체의 유전자 배열 
    public T[] BestGenes { get; private set; }

    //
    public int Elitism;
    //돌연변이확률
    public float MutationRate;

    //새로운 세대의 population
    private List<DNA<T>> newPopulation;
    private Random random;
    private double fitnessSum;
    private int dnaSize;
    private Func<T> getRandomGene;
    private Func<int, double> fitnessFunction;

    //생성자 초기화
    public GeneticAlgorithm(int populationSize, int dnaSize, Random random, Func<T> getRandomGene, Func<int, double> fitnessFunction,
        int elitism, float mutationRate = 0.01f)
    {
        Generation = 1;
        Elitism = elitism;
        MutationRate = mutationRate;
        //population크기에 맞는 염색체 리스트를 생성
        Population = new List<DNA<T>>(populationSize);
        newPopulation = new List<DNA<T>>(populationSize);
        this.random = random;
        this.dnaSize = dnaSize;
        this.getRandomGene = getRandomGene;
        this.fitnessFunction = fitnessFunction;

        //BestGenes  염색체크기에 맞게 생성
        BestGenes = new T[dnaSize];

        for (int i = 0; i < populationSize; i++)
        {
            //한 세대에 리스트에 염색체를 생성하여 삽입한다.
            Population.Add(new DNA<T>(dnaSize, random, getRandomGene, fitnessFunction, shouldInitGenes: true));
        }
    }


    //새로운 세대를 생성한다.
    public void NewGeneration(int numNewDNA = 0, bool crossoverNewDNA = false)
    {
        // .Count : 현재 사용중인 리스트 내부의 요소 개수
        int finalCount = Population.Count + numNewDNA;

        if (finalCount <= 0)
        {
            return;
        }


        if (Population.Count > 0)
        {
            //Fitness 계산하여 가장 높은 적합도를 갖는 염색체를 얻어 BestGenes, BestFitness 설정한다.
            CalculateFitness();
            //Population에 있는 DNA 오름차순 정렬
            Population.Sort(CompareDNA);
        }
        // newPopulation 리스트 내부의 요소를 모두 지운다.
        newPopulation.Clear();

        for (int i = 0; i < Population.Count; i++)
        {
            //Elitism 값보다 작은 DNA는 새로운 세대에 삽입한다. 즉, 상위 Elitism만 살린다.
            if (i < Elitism && i < Population.Count)
            {
                newPopulation.Add(Population[i]);
            }
            //DNA를 교배한다.
            else if (i < Population.Count || crossoverNewDNA)
            {
                DNA<T> parent1 = ChooseParent();
                DNA<T> parent2 = ChooseParent();
                //선택된 두 부모로 부터 교배하여 자식 DNA생성
                DNA<T> child = parent1.Crossover(parent2);

                //자식 DNA 변이
                child.Mutate(MutationRate);

                //새로운 세대에 자식 DNA 삽입
                newPopulation.Add(child);
            }
            ////대치 : 새로운 DNA를 새로운 세대에 삽입 (가장 품질이 낮은 해를 대치)
            //else
            //{
            //    newPopulation.Add(new DNA<T>(dnaSize, random, getRandomGene, fitnessFunction, shouldInitGenes: true));
            //}
        }

        //swap 한다.
        List<DNA<T>> tmpList = Population;
        Population = newPopulation;
        newPopulation = tmpList;

        //세대 증가
        Generation++;
    }

    //DNA 오름차순 정렬
    private int CompareDNA(DNA<T> a, DNA<T> b)
    {
        //정렬을 Fitness 기준으로 오름차순 
        if (a.Fitness > b.Fitness)
        {
            return 1;
        }
        else if (a.Fitness < b.Fitness)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }

    //Fitness 계산하여 가장 높은 적합도를 갖는 염색체를 얻는다.
    private void CalculateFitness()
    {
        //모든 적합도 값을 더한다.
        fitnessSum = 0;
        DNA<T> best = Population[0];

        //리스트중에 적합성이 가장 높은 염색체는 best로 설정
        for (int i = 0; i < Population.Count; i++)
        {
            fitnessSum += Population[i].CalculateFitness(i);

            if (Population[i].Fitness < best.Fitness)
            {
                best = Population[i];
            }
        }

        // best염색체의 적합성 수치를 BestFitness에 저장.
        BestFitness = best.Fitness;
        //인덱스 0 부터 시작하여 best 염색체의 유전자 배열을 BestGenes으로 복사한다.
        best.Genes.CopyTo(BestGenes, 0);
    }

    //교배위한 Parent DNA를 고른다.
    private DNA<T> ChooseParent()
    {
        //랜덤 숫자 생성
        double randomNumber = random.NextDouble() * fitnessSum;

        for (int i = 0; i < Population.Count; i++)
        {
            if (randomNumber < Population[i].Fitness)
            {
                return Population[i];
            }

            randomNumber -= Population[i].Fitness;
        }

        return null;
    }
}