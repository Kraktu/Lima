using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigInt
{
    int rank=0,tempNbr;
    float[] nbr= new float[25];
    string[] suffixes = new string[]{"K","M","B","T","q","Q","s","S","O","N","d","U","D","!","@","#","$","%","^","&","*","Aa","Ab","Ac","Ad"};

    public void TransformIntToBigInt(double nbrToTransform)
    {
        nbr[0]=(int)nbrToTransform;
        for (int i = 0; i < nbr.Length-1; i++)
        {
            if (nbr[i] > 1000)
            {
                rank++;
                nbr[i+1] = Mathf.Floor(nbr[i] / 1000);
                nbr[i] = nbr[i] % 1000;
            }
        }
    }
    public void Add(BigInt nbrToAdd)
    {
        for (int i = 0; i < nbrToAdd.rank; i++)
        {
            if (nbr[i]==0)
            {
                rank++;
            }
            nbr[i] += nbrToAdd.nbr[i];
            if (nbr[i]>1000)
            {
                if (nbr[i+1]==0)
                {
                    rank++;
                }
                nbr[i + 1] += Mathf.Floor(nbr[i] / 1000);
                nbr[i] = nbr[i] % 1000;
            }
        }
    }
    public void Minus(BigInt nbrToSous)
    {
        if (CheckIfLower(nbrToSous))
        {
            for (int i = rank; i >= 0 ; i--)
            {
                if (nbr[rank]>=nbrToSous.nbr[rank])
                {
                    nbr[rank] = nbr[rank] - nbrToSous.nbr[rank];
                }
                else
                {
                    nbr[rank + 1] -= 1;
                    nbr[rank] += 1000;
                    nbr[rank] = nbr[rank] - nbrToSous.nbr[rank];
                }
            }
        }
       // else
       // {
       //     //nombre is negatif
       // }
    }
    public void Multiply(float nbrThatMultiply)
    {
        for (int i = rank; i >=0 ; i--)
        {
            nbr[i] = nbr[i] * nbrThatMultiply;
            if (nbr[i]>1000)
            {
                if (nbr[i+1]==0)
                {
                    rank++;
                }
                nbr[i + 1] += Mathf.Floor(nbr[i] / 1000);
                nbr[i] = nbr[i] % 1000;
            }
            nbr[i - 1] += (nbr[i] % 1)*1000;
        }
    }
    public bool CheckIfLower(BigInt nbrToCompare)
    {
        if (nbrToCompare.rank>rank)
        {
            return false;
        }
        else if (nbrToCompare.rank<rank)
        {
            return true;
        }
        else
        {
            for (int i = rank; i > 0; i--)
            {
                if (nbr[rank] > nbrToCompare.nbr[rank])
                {
                    return true;
                }
                else if (nbr[rank] < nbrToCompare.nbr[rank])
                {
                    return false;
                }
            }
            return false;
        }
    }
    public bool CheckIfHigher(BigInt nbrToCompare)
    {
        if (nbrToCompare.rank > rank)
        {
            return true;
        }
        else if (nbrToCompare.rank < rank)
        {
            return false;
        }
        else
        {
            for (int i = rank; i > 0; i--)
            {
                if (nbr[rank] > nbrToCompare.nbr[rank])
                {
                    return false;
                }
                else if (nbr[rank] < nbrToCompare.nbr[rank])
                {
                    return true;
                }
            }
            return false;
        }
    }
    public bool CheckIfEqual(BigInt nbrToCompare)
    {
        if (rank==nbrToCompare.rank)
        {
            for (int i = rank; i > 0; i--)
            {
                if (nbr[rank] > nbrToCompare.nbr[rank])
                {
                    return false;
                }
                else if (nbr[rank] < nbrToCompare.nbr[rank])
                {
                    return false;
                }
            }
            return true;
        }
        else
        {
            return false;
        }
    }
    public override string ToString()
    {
        //return base.ToString();
        if (rank>0)
        {
            return (nbr[rank].ToString() + "," + (nbr[rank - 1] / 10).ToString("0")) + suffixes;
        }
        else
        {
            return nbr[0].ToString();
        }
       //BigInt A;
       //BigInt B;
       //BigInt c = A + B;
    }

   // public static BigInt operator+ (BigInt Terme1, BigInt Term2)
   // {
   //
   //     for (int i = 0; i < nbrToAdd.rank; i++)
   //     {
   //         if (nbr[i] == 0)
   //         {
   //             rank++;
   //         }
   //         nbr[i] += nbrToAdd.nbr[i];
   //         if (nbr[i] > 1000)
   //         {
   //             if (nbr[i + 1] == 0)
   //             {
   //                 rank++;
   //             }
   //             nbr[i + 1] += Mathf.Floor(nbr[i] / 1000);
   //             nbr[i] = nbr[i] % 1000;
   //         }
   //     }
   // }
}
