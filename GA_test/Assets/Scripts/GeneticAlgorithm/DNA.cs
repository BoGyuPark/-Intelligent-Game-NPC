using System;

public class DNA<T>
{
    public T[] Genes { get; private set; }
    public float Fitness { get; private set; }

    private Random random;

    //Func<T>의 T는 리턴값의 타입을 가리키며, 이 경우 입력파라미터는 없다 (일반적으로 T를 입력과 구분하기 위해 TResult로 표현한다)
    private Func<T> getRandomGene;

    //입력이 1개인 경우 Func<T, TResult>, 입력이 2개인 경우 Func<T1, T2, TResult> 를 사용한다.
    private Func<int, float> fitnessFunction;

    public DNA(int size, Random random, Func<T> getRandomGene, Func<int, float> fitnessFunction, bool shouldInitGenes = true)
    { 
        Genes = new T[size];
        this.random = random;
        this.getRandomGene = getRandomGene;
        this.fitnessFunction = fitnessFunction;

        if (shouldInitGenes)
        {
            for (int i = 0; i < Genes.Length; i++)
            {
                Genes[i] = getRandomGene();
            }
        }
    }

    //적합도 계산, 해당 index값에 위치한 유전자를 fitnessFuction을 통하여 적합도를 계산한다.
    public float CalculateFitness(int index)
    {
        Fitness = fitnessFunction(index);
        return Fitness;
    }


    //염색체와 염색체를 교배하여 자식 염색체를 생성한다. 균일교배
    public DNA<T> Crossover(DNA<T> otherParent)
    {
        //자식 염색체 생성
        DNA<T> child = new DNA<T>(Genes.Length, random, getRandomGene, fitnessFunction, shouldInitGenes: false);

        //임계확률 0.5로 설정
        for (int i = 0; i < Genes.Length; i++)
        {
            //random.NextDouble()은 0.0 ~ 1.0 사이 생성
            //조건 ? 참 : 거짓

            child.Genes[i] = random.NextDouble() < 0.5 ? Genes[i] : otherParent.Genes[i];

        }
        // 두 부모의 교배를 통하여 한 명의 자식만 생성하고 반환한다.
        return child;
    }


    //변이
    public void Mutate(float mutationRate)
    {
        for (int i = 0; i < Genes.Length; i++)
        {
            // 0.0 ~ 1.0의 난수를 생성하여 돌연변이확률 보다 작은 경우 랜덤유전자로 변경한다.
            // mutationRate는 전형적으로 0.015 or 0.05 로 설정하자.
            if (random.NextDouble() < mutationRate)
            {
                Genes[i] = getRandomGene();
            }
        }
    }
}